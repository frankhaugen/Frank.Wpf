namespace Frank.Wpf.Controls.CompletionPopup;

public interface ICompletionSource
{
    IEnumerable<ICompletionData> GetCompletions(string text, int position);
}