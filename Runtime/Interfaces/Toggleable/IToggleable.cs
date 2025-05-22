namespace Commons
{
    public interface IToggleable
    {
        bool Enabled { get; }
        
        void Enable();
        void Disable();
    }
}