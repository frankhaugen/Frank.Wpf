using System.Windows.Controls;
using MdXaml;

namespace Frank.Wpf.Markdown.Previewer;

public class MarkdownPreviewer : UserControl
{
    private readonly MarkdownScrollViewer _viewer = new();
    
    public MarkdownPreviewer()
    {
        Content = _viewer;
    }
    
    public string Markdown
    {
        get => _viewer.Markdown;
        set => _viewer.Markdown = value;
    }
}