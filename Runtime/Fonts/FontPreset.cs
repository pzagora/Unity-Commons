using Commons.Attributes;
using TMPro;
using UnityEngine;

namespace Commons
{
    [CreateAssetMenu(fileName = "FontPreset", menuName = "Scriptable Objects/Fonts/Font Preset")]
    public class FontPreset : ScriptableObject
    {
        [Required] public TMP_FontAsset fontAsset;
    }
}