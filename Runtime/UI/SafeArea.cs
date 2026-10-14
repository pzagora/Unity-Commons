using UnityEngine;

namespace Commons
{
    /// <summary>
    /// Insets the RectTransform it is on to the device's <see cref="Screen.safeArea"/> (notch, rounded corners, home
    /// indicator) by driving its anchors, re-applying when the safe area or resolution changes (e.g. on rotation).
    /// <para>
    /// Place it on a content container, not the screen root: keep backgrounds full-bleed as siblings outside it so art
    /// extends edge-to-edge while interactive content stays inside the safe area — no letterbox bars. Each edge can opt
    /// out via the conform flags (e.g. respect the notch at the top but let content bleed under the home indicator).
    /// </para>
    /// </summary>
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public sealed class SafeArea : MonoBehaviour
    {
        [SerializeField] private RectEdge conform = RectEdge.All;

        private RectTransform _rect;
        private Rect _lastSafeArea;
        private int _lastWidth;
        private int _lastHeight;

        private void Awake() 
            => _rect = (RectTransform)transform;

        private void OnEnable() 
            => Apply();

        private void Update()
        {
            if (Screen.safeArea != _lastSafeArea || Screen.width != _lastWidth || Screen.height != _lastHeight)
                Apply();
        }

        private void Apply()
        {
            _lastSafeArea = Screen.safeArea;
            _lastWidth = Screen.width;
            _lastHeight = Screen.height;

            if (_lastWidth <= 0 || _lastHeight <= 0)
                return;

            var area = Screen.safeArea;
            var min = area.position;
            var max = area.position + area.size;

            min.x /= _lastWidth;
            min.y /= _lastHeight;
            max.x /= _lastWidth;
            max.y /= _lastHeight;

            if ((conform & RectEdge.Left) == 0) min.x = 0f;
            if ((conform & RectEdge.Bottom) == 0) min.y = 0f;
            if ((conform & RectEdge.Right) == 0) max.x = 1f;
            if ((conform & RectEdge.Top) == 0) max.y = 1f;

            _rect.anchorMin = min;
            _rect.anchorMax = max;
            _rect.offsetMin = Vector2.zero;
            _rect.offsetMax = Vector2.zero;
        }

#if UNITY_EDITOR
        public static string NameOfConform()
            => nameof(conform);
#endif
    }
}
