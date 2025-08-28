namespace Commons
{
    public interface ILocalizedText : ISetupable<TextModel>
    {
        TextModel Model { get; }
        void UpdateText();
    }
}