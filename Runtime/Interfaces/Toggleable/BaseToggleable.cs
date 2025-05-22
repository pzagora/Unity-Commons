namespace Commons
{
    public abstract class BaseToggleable : IToggleable
    {
        public bool Enabled { get; private set; }

        public virtual void Enable()
        {
            if (Enabled) 
                return;
            
            Enabled = true;
            OnEnable();
        }

        public virtual void Disable()
        {
            if (!Enabled) 
                return;
            
            Enabled = false;
            OnDisable();
        }

        protected virtual void OnEnable() {}
        protected virtual void OnDisable() {}
    }
}