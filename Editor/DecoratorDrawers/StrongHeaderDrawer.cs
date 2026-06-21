#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Commons.Editor
{
    [CustomPropertyDrawer(typeof(StrongHeaderAttribute))]
    public class StrongHeaderDrawer : DecoratorDrawer
    {
        private const float TOP_PADDING = 6f;
        private const float LABEL_HEIGHT = 18f;
        private const float LINE_HEIGHT = 1f;
        private const float LINE_MARGIN = 4f;

        public override float GetHeight()
        {
            var header = (StrongHeaderAttribute)attribute;

            var height = TOP_PADDING + LABEL_HEIGHT;

            if (header.LineSeparator)
                height += LINE_MARGIN + LINE_HEIGHT + LINE_MARGIN;

            return height;
        }

        public override void OnGUI(Rect position)
        {
            var header = (StrongHeaderAttribute)attribute;

            position.y += TOP_PADDING;
            position.height = LABEL_HEIGHT;

            EditorGUI.LabelField(position, header.Text.ToUpper(), EditorHelper.GetCenteredStyle(true));

            if (!header.LineSeparator)
                return;

            var lineRect = new Rect(
                position.x,
                position.y + LABEL_HEIGHT + LINE_MARGIN,
                position.width,
                LINE_HEIGHT
            );

            EditorHelper.HorizontalLine(lineRect, Color.gray);
        }
    }
}
#endif