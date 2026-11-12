using UnityEngine;

namespace Commons
{
    [CreateAssetMenu(
        fileName = "NumericTextPreset",
        menuName = "Scriptable Objects/Text/Numeric Text Preset"
    )]
    public sealed class NumericTextPreset : ScriptableObject
    {
        [SerializeField] private NumericTextMode mode;
        [SerializeField] private NumericTextPrecision precision = NumericTextPrecision.TwoDecimals;
        [SerializeField] private string prefix = DEFAULT_AFFIX;
        [SerializeField] private string suffix = DEFAULT_AFFIX;

        private const float PERCENT_MULTIPLIER = 100f;
        private const string DEFAULT_AFFIX = "";
        private const string FRACTION_SEPARATOR = " / ";
        private const string WHOLE_NUMBER_FORMAT = "F0";
        private const string ONE_DECIMAL_FORMAT = "F1";
        private const string TWO_DECIMALS_FORMAT = "F2";

        public NumericTextMode Mode
            => mode;

        public NumericTextPrecision Precision
            => precision;

        public string Prefix
            => prefix;

        public string Suffix
            => suffix;

        public string Format(float value)
        {
            var formattedValue = mode switch
            {
                NumericTextMode.Percentage => FormatNumber(value * PERCENT_MULTIPLIER),
                _ => FormatNumber(value)
            };

            return AddAffixes(formattedValue);
        }

        public string Format(float value, float maximum)
        {
            var formattedValue = mode switch
            {
                NumericTextMode.Percentage => FormatNumber(
                    Mathf.Approximately(maximum, 0f)
                        ? 0f
                        : value / maximum * PERCENT_MULTIPLIER
                ),
                NumericTextMode.Fraction => string.Concat(
                    FormatNumber(value),
                    FRACTION_SEPARATOR,
                    FormatNumber(maximum)
                ),
                _ => FormatNumber(value)
            };

            return AddAffixes(formattedValue);
        }

        private string FormatNumber(float value)
            => value.ToString(GetNumberFormat());

        private string AddAffixes(string value)
            => string.Concat(prefix ?? DEFAULT_AFFIX, value, suffix ?? DEFAULT_AFFIX);

        private string GetNumberFormat()
            => precision switch
            {
                NumericTextPrecision.WholeNumber => WHOLE_NUMBER_FORMAT,
                NumericTextPrecision.OneDecimal => ONE_DECIMAL_FORMAT,
                NumericTextPrecision.TwoDecimals => TWO_DECIMALS_FORMAT,
                _ => TWO_DECIMALS_FORMAT
            };
    }
}
