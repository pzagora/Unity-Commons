#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Commons.Editor
{
    [CustomEditor(typeof(StyleLinkedTMP))]
    [CanEditMultipleObjects]
    public class StyleLinkedTMPEditor : UnityEditor.Editor
    {
        private UnityEditor.Editor _internalTMPInspector;
        private StyleLinkedTMP _styleLinked;
        private SerializedProperty _fontSettingProp;
        private SerializedProperty _numericTextPresetProp;

        protected virtual bool ShowNumericFormatting
            => true;

        private void OnEnable()
        {
            _styleLinked = (StyleLinkedTMP)target;
            _fontSettingProp = serializedObject.FindProperty("fontSetting");
            _numericTextPresetProp = serializedObject.FindProperty("numericTextPreset");

            var tmpEditorType = Type.GetType("TMPro.EditorUtilities.TMP_EditorPanelUI, Unity.TextMeshPro.Editor");
            if (tmpEditorType != null)
            {
                _internalTMPInspector = CreateEditor(target, tmpEditorType);
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawFontSettingUI();

            if (ShowNumericFormatting)
                DrawNumericTextPresetUI();

            serializedObject.ApplyModifiedProperties();

            if (_internalTMPInspector != null)
            {
                _internalTMPInspector.OnInspectorGUI();
            }
            else
            {
                EditorGUILayout.HelpBox("TMP Editor not found. Falling back to default inspector.", MessageType.Warning);
                DrawDefaultInspector();
            }
        }

        private void DrawNumericTextPresetUI()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Numeric Formatting", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_numericTextPresetProp, GUIContent.none);

            if (_numericTextPresetProp.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox(
                    "Optional. Normal and localized string bindings ignore numeric formatting.",
                    MessageType.Info
                );
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        private void DrawFontSettingUI()
        {
            var styleLinked = (StyleLinkedTMP)target;

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Font Setting", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_fontSettingProp, GUIContent.none);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                styleLinked.ApplyFont();
                EditorUtility.SetDirty(styleLinked);
            }

            if (styleLinked.FontSetting != null && styleLinked.font != styleLinked.FontSetting.fontAsset)
            {
                if (GUILayout.Button("Apply", GUILayout.Width(60)))
                {
                    styleLinked.ApplyFont();
                    EditorUtility.SetDirty(styleLinked);
                }
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();
        }

        private void OnDisable()
        {
            if (_internalTMPInspector != null)
            {
                DestroyImmediate(_internalTMPInspector);
            }
        }
    }
}
#endif
