namespace Frank.Wpf.Controls.CompletionPopup;

public interface ICompletionTriggerRule
{
    bool ShouldTriggerCompletion(string text, int position);
}