using System.Collections.Generic;
using UnityEngine;

namespace Commons
{
    public class NestedToggleable : BaseToggleable
    {
        public IReadOnlyList<IToggleable> Children => _children;
        private readonly List<IToggleable> _children = new();
        
        protected override void OnEnable()
        {
            base.OnEnable();
            _children.ForEach(child => child.Enable());
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _children.ForEach(child => child.Disable());
        }
        
        public void Add(IToggleable child)
        {
            if (_children.Contains(child))
            {
                Debug.Log($"[{nameof(NestedToggleable)}] Duplicate {child}");
                return;
            }
            
            _children.Add(child);
            
            if (Enabled)
                child.Enable();
        }

        public void Remove(IToggleable child)
        {
            _children.Remove(child);
            
            if (Enabled)
                child.Disable();
        }

        public void Clear()
        {
            if (Enabled)
                _children.ForEach(child => child.Disable());
            
            _children.Clear();
        }
    }
}