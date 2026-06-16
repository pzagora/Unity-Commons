using TMPro;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Commons
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    public class StyleLinkedTMP : TextMeshProUGUI
    {
        [SerializeField] private FontPreset fontSetting;

        public FontPreset FontSetting
            => fontSetting;

        protected override void Awake()
        {
            base.Awake();

            ApplyPreset();
            Binder.Bind(this);
        }

        protected override void OnDestroy()
        {
            Binder.Unbind(this);
            base.OnDestroy();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ApplyPreset();
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            ApplyPreset();
        }

        public void ApplyFont()
        {
            if (fontSetting == null || fontSetting.fontAsset == null)
                return;

            if (font == fontSetting.fontAsset)
                return;
           
            font = fontSetting.fontAsset;
            SetAllDirty();
        }

        private void ApplyPreset()
        {
            if (fontSetting == null || fontSetting.fontAsset == null)
                return;

#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
            {
                ApplyFont();
                return;
            }
#endif

            ApplyFont();
        }
    }
}