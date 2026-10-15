using UnityEngine;

namespace Commons
{
    /// <summary>
    /// Reusable sizing for a <see cref="GenericObjectPool{T}"/>, authored as an asset so several pools can share one
    /// configuration instead of repeating the numbers.
    /// </summary>
    [CreateAssetMenu(menuName = "Scriptable Objects/Pooling/Pool Settings", fileName = "Pool Settings")]
    public sealed class PoolSettings : ScriptableObject
    {
        [SerializeField] private int timeToLive = 30;
        [SerializeField] private int initialSize;
        [SerializeField] private int maxSize = 20;

        public int TimeToLive => timeToLive;
        public int InitialSize => initialSize;
        public int MaxSize => maxSize;
    }
}
