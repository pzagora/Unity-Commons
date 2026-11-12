using System;
using TMPro;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Commons.Editor
{
    public static class TMPConversionUtility
    {
        private const string CONVERT_TO_STYLE_LINKED = "Convert to StyleLinkedTMP";
        private const string CONTEXT_MENU_PATH = "CONTEXT/TextMeshProUGUI/Convert to StyleLinkedTMP";

        public static T Convert<T>(TextMeshProUGUI source, string undoName)
            where T : TextMeshProUGUI
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var gameObject = source.gameObject;
            var sourceIndex = Array.IndexOf(gameObject.GetComponents<Component>(), source);
            var serializedState = EditorJsonUtility.ToJson(source);
            var undoGroup = Undo.GetCurrentGroup();

            Undo.SetCurrentGroupName(undoName);

            Undo.DestroyObjectImmediate(source);
            var converted = Undo.AddComponent<T>(gameObject);
            EditorJsonUtility.FromJsonOverwrite(serializedState, converted);

            MoveToComponentIndex(converted, sourceIndex);
            Undo.CollapseUndoOperations(undoGroup);

            EditorUtility.SetDirty(gameObject);
            Selection.activeGameObject = gameObject;
            return converted;
        }

        [MenuItem(CONTEXT_MENU_PATH)]
        private static void ConvertToStyleLinked(MenuCommand command)
            => Convert<StyleLinkedTMP>((TextMeshProUGUI)command.context, CONVERT_TO_STYLE_LINKED);

        [MenuItem(CONTEXT_MENU_PATH, true)]
        private static bool CanConvertToStyleLinked(MenuCommand command)
            => command.context != null && command.context.GetType() == typeof(TextMeshProUGUI);

        private static void MoveToComponentIndex(Component component, int targetIndex)
        {
            var components = component.gameObject.GetComponents<Component>();
            var currentIndex = Array.IndexOf(components, component);

            while (currentIndex > targetIndex && ComponentUtility.MoveComponentUp(component))
                currentIndex--;
        }
    }
}
