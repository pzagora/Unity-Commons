using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
#endif

namespace Commons.Injection
{
    /// <summary>
    /// Handles class fields marked by <see cref="Inject"/> or <see cref="Update"/> and classes by <see cref="Install"/> dependency binding
    /// </summary>
    public static class Binder
    {
        private sealed class Listener
        {
            public object Target;
            public FieldInfo Field;
            public MethodInfo Callback;

            public void Call(object dependency)
            {
                if (Callback != null && dependency != null)
                {
#if ENABLE_DI_LOGS
                    DebugLog("Invoking", target, callback, dependency);
#endif
                    Callback.Invoke(Target, new object[] { dependency });
                }

#if ENABLE_DI_LOGS
                DebugLog("Injecting", target, field, dependency);
#endif
                Field.SetValue(Target, dependency);
            }
        }

        private class ListenerList : List<Listener>
        {
            public void Call(object dependency)
                => ForEach(listener => listener.Call(dependency));

            public void Remove(object target)
                => RemoveAll(listener => listener.Target == target);
        }

        private class DependencyList : List<object> { }

        private class InstallData
        {
            public Type Type;
        }

        private class InjectData
        {
            public Type Type;
            public FieldInfo Field;
            public MethodInfo Callback;
            public bool Updater;
        }

        private class ReflectionData
        {
            public List<InstallData> Installs;
            public List<InjectData> Injects;
        }

        private static Dictionary<Type, DependencyList> _dependencyLists = new();
        private static Dictionary<Type, ListenerList> _injectorLists = new();
        private static Dictionary<Type, ListenerList> _updaterLists = new();
        private static Dictionary<Type, ReflectionData> _reflections = new();

        public static void Bind(object target)
        {
            var type = target.GetType();
            var list = EnsureReflection(type);

            if (list.Installs != null)
                foreach (var install in list.Installs)
                    Install(target, install);
            
            if (list.Injects != null)
                foreach (var inject in list.Injects)
                    Inject(target, inject);
        }

        public static void Unbind(object target)
        {
            var type = target.GetType();
            var list = EnsureReflection(type);

            if (list.Installs != null)
                foreach (var install in list.Installs)
                    Uninstall(target, install);
            
            if (list.Injects != null)
                foreach (var inject in list.Injects)
                    Uninject(target, inject);
        }

        public static void Install(object target, Type type)
        {
#if ENABLE_DI_LOGS
            DebugLog("Installing", target);
#endif

            AddDependency(type, target);
            RefreshDependency(type);
        }

        public static void Install<T>(T target)
            => Install(target, typeof(T));

        public static void Uninstall(object target, Type type)
        {
#if ENABLE_DI_LOGS
            DebugLog("Uninstalling", target);
#endif

            if (RemoveDependency(type, target))
            {
                RefreshDependency(type);
            }
        }

        public static void Uninstall<T>(T target)
            => Uninstall(target, typeof(T));

        private static void Install(object target, InstallData install)
            => Install(target, install.Type);

        private static void Uninstall(object target, InstallData install)
            => Uninstall(target, install.Type);

        private static void Inject(object target, InjectData inject)
        {
            var listener = new Listener
            {
                Target = target,
                Field = inject.Field,
                Callback = inject.Callback
            };

            var injected = RefreshListener(inject.Type, listener);

            if (inject.Updater)
            {
                AddUpdater(inject.Type, listener);
            }
            else if (!injected)
            {
                AddInjector(inject.Type, listener);
            }
        }

        private static void Uninject(object target, InjectData inject)
        {
#if ENABLE_DI_LOGS
            DebugLog("Uninjecting", target, inject.field);
#endif

            RemoveUpdater(inject.Type, target);
            inject.Field.SetValue(target, null);
        }

        private static bool RefreshListener(Type type, Listener listener)
        {
            if (TryGetDependencies(type, out var dependencies) &&
                dependencies.TryGetLast(out var dependency))
            {
                listener.Call(dependency);
                return true;
            }
            return false;
        }

        private static void RefreshDependency(Type type)
        {
            if (!TryGetDependencies(type, out var dependencies) ||
                !dependencies.TryGetLast(out var dependency))
            {
                dependency = null;
            }

            if (TryGetUpdaters(type, out var updaters))
            {
                updaters.Call(dependency);
            }

            if (TryGetInjectors(type, out var injectors))
            {
                injectors.Call(dependency);
                injectors.Clear();

                RemoveInjectors(type);
            }
        }

        #region Dependencies
        private static void AddDependency(Type type, object dependency)
        {
            EnsureDependencies(type).Add(dependency);
        }

        private static bool RemoveDependency(Type type, object dependency)
        {
            if (TryGetDependencies(type, out var dependencies))
            {
                var changed = dependencies.Remove(dependency);

                if (dependencies.Count == 0)
                {
                    RemoveDependencies(type);
                }

                return changed;
            }
            return false;
        }

        private static bool TryGetDependencies(Type type, out DependencyList dependencies)
        {
            return _dependencyLists.TryGetValue(type, out dependencies);
        }

        private static DependencyList EnsureDependencies(Type type)
        {
            if (!_dependencyLists.TryGetValue(type, out var dependencies))
                _dependencyLists[type] = dependencies = new DependencyList();
            return dependencies;
        }

        private static void RemoveDependencies(Type type)
        {
            _dependencyLists.Remove(type);
        }
        #endregion

        #region Updaters
        private static void AddUpdater(Type type, Listener updater)
        {
            EnsureUpdaters(type).Add(updater);
        }

        private static void RemoveUpdater(Type type, object updater)
        {
            if (TryGetUpdaters(type, out var updaters))
            {
                updaters.Remove(updater);
                if (updaters.Count == 0)
                {
                    RemoveUpdaters(type);
                }
            }
        }

        private static bool TryGetUpdaters(Type type, out ListenerList updaters)
        {
            return _updaterLists.TryGetValue(type, out updaters);
        }

        private static ListenerList EnsureUpdaters(Type type)
        {
            if (!_updaterLists.TryGetValue(type, out var updaters))
                _updaterLists[type] = updaters = new ListenerList();
            return updaters;
        }

        private static void RemoveUpdaters(Type type)
        {
            _updaterLists.Remove(type);
        }
        #endregion

        #region Injectors
        private static void AddInjector(Type type, Listener injector)
        {
            EnsureInjectors(type).Add(injector);
        }

        private static void RemoveInjector(Type type, object injector)
        {
            if (TryGetInjectors(type, out var injectors))
            {
                injectors.Remove(injector);
                if (injectors.Count == 0)
                {
                    RemoveInjectors(type);
                }
            }
        }

        private static bool TryGetInjectors(Type type, out ListenerList injectors)
        {
            return _injectorLists.TryGetValue(type, out injectors);
        }

        private static ListenerList EnsureInjectors(Type type)
        {
            if (!_injectorLists.TryGetValue(type, out var injectors))
                _injectorLists[type] = injectors = new ListenerList();
            return injectors;
        }

        private static void RemoveInjectors(Type type)
        {
            _injectorLists.Remove(type);
        }
        #endregion

        private static ReflectionData EnsureReflection(Type type)
        {
            if (!_reflections.TryGetValue(type, out var result))
                _reflections[type] = result = CreateReflection(type);
            return result;
        }

        private static ReflectionData CreateReflection(Type type)
        {
            var injects = CreateInjectData(type);
            var installs = CreateInstallDatas(type, injects);

            return new ReflectionData()
            {
                Installs = installs,
                Injects = injects
            };
        }

        private static List<InstallData> CreateInstallDatas(Type type, List<InjectData> injects)
        {
            var result = new List<InstallData>();

            foreach (var attribute in type.GetCustomAttributes<Install>())
            {
                var data = new InstallData
                {
                    Type = attribute.Type ?? type
                };
                result.Add(data);
            }
            
            if (result.Count > 0)
            {
                return result;
            }

            if (injects == null)
            {
                var data = new InstallData
                {
                    Type = type
                };
                result.Add(data);
            }
            return result;
        }
        
        private static List<InjectData> CreateInjectData(Type type)
        {
            const BindingFlags fieldBindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            var fields = type.GetAllFields(fieldBindings);

            var result = fields
                .Select(field => CreateInjectData(type, field))
                .Where(inject => inject != null)
                .ToList();

            return result.Count > 0 
                ? result 
                : null;
        }

        private static InjectData CreateInjectData(Type type, FieldInfo field)
        {
            const BindingFlags methodBindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            if (!field.TryGetCustomAttribute<Inject>(out var injectAttribute)) 
                return null;
            
            var injectType = injectAttribute.Type ?? field.FieldType;
            var callback = injectAttribute.Callback ?? $"On{injectType.Name}Inject";
            var method = type.GetMethod(callback, methodBindings);

            return new InjectData
            {
                Type = injectType,
                Field = field,
                Callback = method,
                Updater = injectAttribute is Update
            };
        }

        private static void Clear()
        {
            _dependencyLists = new Dictionary<Type, DependencyList>();
            _injectorLists = new Dictionary<Type, ListenerList>();
            _updaterLists = new Dictionary<Type, ListenerList>();
            _reflections = new Dictionary<Type, ReflectionData>();
        }

#if UNITY_EDITOR
        [DidReloadScripts]
        private static void OnReloadScripts()
        {
            Clear();

            EditorApplication.playModeStateChanged += onPlayModeStateChanged;
            return;

            void onPlayModeStateChanged(PlayModeStateChange change)
            {
                if (change == PlayModeStateChange.ExitingPlayMode)
                    Clear();
            }
        }
#endif

#if ENABLE_DI_LOGS
        private const string TARGET_COLOR = "FF8000";
        private const string FIELD_COLOR = "00FFFF";
        private const string VALUE_COLOR = "FFFFFF";

        private static void DebugLog(string message, object target, object field, object value)
        {
            UnityEngine.Debug.Log($"[{nameof(DI_Binder)}] {message} <color=#{FIELD_COLOR}>[{field}]</color> of <color=#{TARGET_COLOR}>[{target}]</color> with value <color=#{VALUE_COLOR}>[{value}]</color>");
        }

        private static void DebugLog(string message, object target, object field)
        {
            UnityEngine.Debug.Log($"[{nameof(DI_Binder)}] {message} <color=#{FIELD_COLOR}>[{field}]</color> of <color=#{TARGET_COLOR}>[{target}]</color>");
        }

        private static void DebugLog(string message, object target)
        {
            UnityEngine.Debug.Log($"[{nameof(DI_Binder)}] {message} <color=#{TARGET_COLOR}>[{target}]</color>");
        }

        private static void DebugWarning(string message, object target)
        {
            UnityEngine.Debug.LogWarning($"[{nameof(DI_Binder)}] {message} <color=#{TARGET_COLOR}>[{target}]</color>");
        }
#endif
    }
}
