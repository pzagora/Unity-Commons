#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Commons.Extensions;
using Object = UnityEngine.Object;

namespace Commons.Editor.Helpers
{
    public static class EditorHelper
    {
        private const string SCRIPT_LABEL = "Script";
        
        public static void DrawScript(this object target)
        {
            using (new EditorGUI.DisabledScope(true))
                EditorGUILayout.ObjectField(
                    SCRIPT_LABEL, 
                    MonoScript.FromMonoBehaviour((MonoBehaviour)target), 
                    target.GetType(),
                    false);
            
            EditorGUILayout.Separator();
        }

        public static bool TryGetItem<T>(this object target, out T item) where T : class
        {
            item = target as T;
            return item != null;
        }

        public static string GetInspectorDisplayName(this string name)
        {
            return ObjectNames.NicifyVariableName(name);
        }
        
        public static GUIContent GetInspectorDisplayGUIContent(this string name)
        {
            return new GUIContent(ObjectNames.NicifyVariableName(name));
        }

        public static void MarkDirty(this Object target)
        {
            EditorUtility.SetDirty(target);
            PrefabUtility.RecordPrefabInstancePropertyModifications(target);
        }

        public static void CenteredLabel(string text, bool lineSeparator = true)
        {
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField(text.ToUpper(), GetCenteredStyle(true), GUILayout.ExpandWidth(true));

            if (lineSeparator)
            {
                HorizontalLine();
            }
        }
        
        public static GUIStyle GetCenteredStyle(bool isHeader = false)
        {
            return new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle = isHeader ? FontStyle.Bold : FontStyle.Italic,
                richText = true
            };
        }

        public static void HorizontalLine() => HorizontalLine(Color.gray);
        public static void HorizontalLine(Color color, int height = 1)
        {
            var c = GUI.color;
            GUI.color = color;

            var horizontalLine = new GUIStyle()
            {
                normal =
                {
                    background = EditorGUIUtility.whiteTexture
                },
                margin = new RectOffset(NumericExtensions.Zero, NumericExtensions.Zero, 4, 4),
                fixedHeight = height
            };
            
            GUILayout.Box(GUIContent.none, horizontalLine);

            GUI.color = c;
        }
        
        public static bool MatchIgnoreCase(this FieldInfo field, string fieldName, bool condition = true)
        {
            return string.Equals(field.Name, fieldName, StringComparison.OrdinalIgnoreCase) && condition;
        }
    }
}
#endif
