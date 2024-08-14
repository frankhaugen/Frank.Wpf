namespace Frank.Wpf.Controls.CompletionPopup;

public class CompletionData : ICompletionData
{
    public CompletionData(string text, string description = "")
    {
        Text = text;
        Description = description;
    }

    public string Text { get; }
    public string Description { get; }
}