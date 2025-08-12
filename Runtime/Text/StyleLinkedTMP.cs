using Commons.Injection;
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
        [SerializeField] private FontSetting fontSetting;
        
        public FontSetting FontSetting 
            => fontSetting;

        protected override void Awake()
        {
            base.Awake();
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
            ApplyFontIfNeeded();
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            ApplyFontIfNeeded();
        }

        public void ApplyFont()
        {
            if (fontSetting != null && fontSetting.fontAsset != null)
            {
                font = fontSetting.fontAsset;
            }
        }

        private void ApplyFontIfNeeded()
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying && fontSetting != null && font != fontSetting.fontAsset)
            {
                ApplyFont();
            }
#endif
        }
    }
}