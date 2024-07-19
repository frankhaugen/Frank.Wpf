using System.Windows.Controls;

namespace Frank.Wpf.Markdown.Previewer;

public class MarkdownPreviewer : UserControl
{
    // private readonly WebBrowser _viewer = new();
    private readonly RichTextBox _viewer = new();
    
    public MarkdownPreviewer()
    {
        Content = _viewer;
        Markdown.MarkdownUpdated += OnMarkdownUpdated;
    }
    
    public MarkdownContainer Markdown { get; } = new();
    
    private void OnMarkdownUpdated(object? sender, MarkdownUpdatedEventArgs e)
    {
        _viewer.Document = MarkdownToFlowDocumentConverter.Convert(e.Markdown);
        // _viewer.NavigateToString(Markdown.GetHtml());
    }
}