using UnityEngine;

namespace Commons
{
    public enum NumericTextPrecision
    {
        [InspectorName("Whole number (0)")]
        WholeNumber,

        [InspectorName("One decimal (0.0)")]
        OneDecimal,

        [InspectorName("Two decimals (0.00)")]
        TwoDecimals
    }
}
