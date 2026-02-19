namespace Commons
{
    public interface ILocalizedText
    {
        TextModel Model { get; }
        void UpdateText();
    }
}