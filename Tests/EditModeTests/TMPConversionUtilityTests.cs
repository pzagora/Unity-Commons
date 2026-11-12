using Commons.Editor;
using NUnit.Framework;
using TMPro;
using UnityEngine;

namespace Commons.Tests
{
    public sealed class TMPConversionUtilityTests
    {
        private GameObject _gameObject;

        [SetUp]
        public void SetUp()
            => _gameObject = new GameObject("Text", typeof(RectTransform));

        [TearDown]
        public void TearDown()
            => Object.DestroyImmediate(_gameObject);

        [Test]
        public void ConvertToStyleLinkedTMPPreservesCompleteTMPState()
        {
            var source = _gameObject.AddComponent<TextMeshProUGUI>();
            source.text = "Styled text";
            source.fontSize = 27f;
            source.color = new Color(0.2f, 0.4f, 0.6f, 0.8f);
            source.alignment = TextAlignmentOptions.BottomRight;
            source.textWrappingMode = TextWrappingModes.NoWrap;
            source.richText = false;
            source.raycastTarget = false;
            source.margin = new Vector4(1f, 2f, 3f, 4f);

            var converted = TMPConversionUtility.Convert<StyleLinkedTMP>(
                source,
                "Convert test text"
            );

            Assert.AreEqual("Styled text", converted.text);
            Assert.AreEqual(27f, converted.fontSize);
            Assert.AreEqual(new Color(0.2f, 0.4f, 0.6f, 0.8f), converted.color);
            Assert.AreEqual(TextAlignmentOptions.BottomRight, converted.alignment);
            Assert.AreEqual(TextWrappingModes.NoWrap, converted.textWrappingMode);
            Assert.IsFalse(converted.richText);
            Assert.IsFalse(converted.raycastTarget);
            Assert.AreEqual(new Vector4(1f, 2f, 3f, 4f), converted.margin);
            Assert.AreEqual(typeof(StyleLinkedTMP), converted.GetType());
        }
    }
}
