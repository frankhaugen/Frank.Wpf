namespace Frank.Wpf.Markdown.Previewer;

public class MarkdownUpdatedEventArgs(string markdown) : EventArgs
{
    public string Markdown { get; } = markdown;
}