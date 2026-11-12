using System;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Commons.Editor
{
    [CustomEditor(typeof(TextMeshProUGUI))]
    internal sealed class TextMeshProUGUIEditor : UnityEditor.Editor
    {
        private UnityEditor.Editor _internalEditor;

        private void OnEnable()
        {
            var tmpEditorType = Type.GetType(
                "TMPro.EditorUtilities.TMP_EditorPanelUI, Unity.TextMeshPro.Editor"
            );

            if (tmpEditorType != null)
                _internalEditor = CreateEditor(target, tmpEditorType);
        }

        public override void OnInspectorGUI()
        {
            var text = (TextMeshProUGUI)target;

            if (DrawStyleLinkedTools(text) || TMPInspectorToolRegistry.Draw(text))
            {
                GUIUtility.ExitGUI();
                return;
            }

            EditorGUILayout.Space();

            if (_internalEditor != null)
            {
                _internalEditor.OnInspectorGUI();
            }
            else
            {
                EditorGUILayout.HelpBox(
                    "Could not find TMP_EditorPanelUI. Showing the default inspector.",
                    MessageType.Warning
                );
                DrawDefaultInspector();
            }
        }

        private static bool DrawStyleLinkedTools(TextMeshProUGUI text)
        {
            var buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 14,
                fixedHeight = 40,
                fontStyle = FontStyle.Bold
            };

            var originalColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.green;

            var converted = false;

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label("Style-Linked Text", EditorStyles.boldLabel);

            if (GUILayout.Button("Convert to StyleLinkedTMP", buttonStyle))
            {
                TMPConversionUtility.Convert<StyleLinkedTMP>(
                    text,
                    "Convert to StyleLinkedTMP"
                );
                converted = true;
            }

            EditorGUILayout.EndVertical();
            GUI.backgroundColor = originalColor;
            EditorGUILayout.Space();
            return converted;
        }

        private void OnDisable()
        {
            if (_internalEditor != null)
                DestroyImmediate(_internalEditor);
        }
    }
}
