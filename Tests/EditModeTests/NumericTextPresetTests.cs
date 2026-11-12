using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Commons.Tests
{
    public sealed class NumericTextPresetTests
    {
        private NumericTextPreset _preset;

        [SetUp]
        public void SetUp()
            => _preset = ScriptableObject.CreateInstance<NumericTextPreset>();

        [TearDown]
        public void TearDown()
            => Object.DestroyImmediate(_preset);

        [Test]
        public void Format_PercentageUsesPrecisionAndAffixes()
        {
            Configure(
                NumericTextMode.Percentage,
                NumericTextPrecision.OneDecimal,
                "Progress: ",
                "%"
            );

            Assert.AreEqual(
                string.Concat("Progress: ", 12.5f.ToString("F1"), "%"),
                _preset.Format(0.125f)
            );
        }

        [Test]
        public void FormatRange_FractionFormatsBothValues()
        {
            Configure(
                NumericTextMode.Fraction,
                NumericTextPrecision.WholeNumber,
                "",
                " items"
            );

            Assert.AreEqual(
                string.Concat(2f.ToString("F0"), " / ", 5f.ToString("F0"), " items"),
                _preset.Format(2f, 5f)
            );
        }

        [Test]
        public void FormatRange_PercentageWithZeroMaximumReturnsZero()
        {
            Configure(
                NumericTextMode.Percentage,
                NumericTextPrecision.TwoDecimals,
                "",
                "%"
            );

            Assert.AreEqual(string.Concat(0f.ToString("F2"), "%"), _preset.Format(4f, 0f));
        }

        private void Configure(
            NumericTextMode mode,
            NumericTextPrecision precision,
            string prefix,
            string suffix)
        {
            var serializedPreset = new SerializedObject(_preset);
            serializedPreset.FindProperty("mode").enumValueIndex = (int)mode;
            serializedPreset.FindProperty("precision").enumValueIndex = (int)precision;
            serializedPreset.FindProperty("prefix").stringValue = prefix;
            serializedPreset.FindProperty("suffix").stringValue = suffix;
            serializedPreset.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
