namespace Commons
{
    public interface ILocalizedText : ISetupable<string>
    {
        string Key { get; }
        void UpdateText();
    }
}