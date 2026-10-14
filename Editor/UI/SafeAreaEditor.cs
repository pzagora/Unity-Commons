#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Commons.Editor
{
    [CustomEditor(typeof(SafeArea))]
    [CanEditMultipleObjects]
    public class SafeAreaEditor : UnityEditor.Editor
    {
        private const float CELL_HEIGHT = 20f;
        private const float ROW_SPACING = 2f;
        private const float MAX_BLOCK_WIDTH = 240f;

        public override void OnInspectorGUI()
        {
            target.DrawScript();

            serializedObject.Update();
            DrawEdgeSelector(serializedObject.FindProperty(SafeArea.NameOfConform()));
            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();
            DrawSafeAreaInfo();
        }

        private static void DrawEdgeSelector(SerializedProperty conform)
        {
            if (conform == null)
                return;

            EditorHelper.CenteredLabel(ObjectNames.NicifyVariableName(conform.name));

            var value = (RectEdge)conform.intValue;

            var height = CELL_HEIGHT * 3f + ROW_SPACING * 2f;
            var area = GUILayoutUtility.GetRect(0f, height, GUILayout.ExpandWidth(true));

            var blockWidth = Mathf.Min(area.width, MAX_BLOCK_WIDTH);
            var cellWidth = blockWidth / 3f;
            var originX = area.x + (area.width - blockWidth) * 0.5f;

            Rect Cell(int column, int row) => new Rect(
                originX + column * cellWidth,
                area.y + row * (CELL_HEIGHT + ROW_SPACING),
                cellWidth,
                CELL_HEIGHT);

            value = EdgeToggle(Cell(1, 0), value, RectEdge.Top);
            value = EdgeToggle(Cell(0, 1), value, RectEdge.Left);
            GUI.Label(Cell(1, 1), ObjectNames.NicifyVariableName(nameof(SafeArea)), EditorStyles.centeredGreyMiniLabel);
            value = EdgeToggle(Cell(2, 1), value, RectEdge.Right);
            value = EdgeToggle(Cell(1, 2), value, RectEdge.Bottom);

            conform.intValue = (int)value;
        }

        private static RectEdge EdgeToggle(Rect rect, RectEdge value, RectEdge edge)
        {
            var isOn = (value & edge) != 0;
            var nowOn = GUI.Toggle(rect, isOn, edge.ToString(), EditorStyles.miniButton);

            if (nowOn != isOn)
                value ^= edge;

            return value;
        }

        private static void DrawSafeAreaInfo()
        {
            var area = Screen.safeArea;
            var full = area.x == 0f && area.y == 0f
                && Mathf.Approximately(area.width, Screen.width)
                && Mathf.Approximately(area.height, Screen.height);

            EditorGUILayout.HelpBox(
                full
                    ? "No safe-area inset in the current view (full screen). Open the Device Simulator " +
                      "(Window ▸ General ▸ Device Simulator) and pick a device with a notch to preview the inset live."
                    : $"Current safe area: ({area.x:0}, {area.y:0}) {area.width:0}×{area.height:0} of {Screen.width}×{Screen.height}.",
                MessageType.Info);
        }
    }
}
#endif
