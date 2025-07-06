using Commons.Attributes;
using TMPro;
using UnityEngine;

namespace Commons
{
    [CreateAssetMenu(fileName = "FontSetting", menuName = "Scriptable Objects/Fonts/Font Setting")]
    public class FontSetting : ScriptableObject
    {
        [Required] public TMP_FontAsset fontAsset;
    }
}