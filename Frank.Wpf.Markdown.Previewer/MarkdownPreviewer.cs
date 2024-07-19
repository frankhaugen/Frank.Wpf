using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Frank.Markdown;

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

public class MarkdownContainer : DependencyObject
{
    public string Markdown
    {
        get => (string)GetValue(MarkdownProperty);
        set => SetValue(MarkdownProperty, value);
    }
    
    public string GetHtml()
    {
        var htmlBuilder = new StringBuilder();
        
        htmlBuilder.AppendLine("<!DOCTYPE html>");
        htmlBuilder.AppendLine("<html>");
        htmlBuilder.AppendLine("<head>");
        htmlBuilder.AppendLine("<style>");
        htmlBuilder.AppendLine("</style>");
        htmlBuilder.AppendLine("</head>");
        htmlBuilder.AppendLine("<body>");
        // htmlBuilder.AppendLine(htmlBody);
        htmlBuilder.AppendLine("</body>");
        
        return htmlBuilder.ToString();
    }
    
    public IMarkdownDocument GetDocument() => MarkdownDocument.Create(Markdown);

    public event EventHandler<MarkdownUpdatedEventArgs>? MarkdownUpdated;
    
    private static void OnMarkdownChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is MarkdownContainer container)
        {
            container.MarkdownUpdated?.Invoke(container, new MarkdownUpdatedEventArgs((string)e.NewValue));
        }
    }
    
    private static readonly DependencyProperty MarkdownProperty = DependencyProperty.Register(
        nameof(Markdown),
        typeof(string),
        typeof(MarkdownContainer),
        new PropertyMetadata(default(string), OnMarkdownChanged));
}

public class MarkdownUpdatedEventArgs(string markdown) : EventArgs
{
    public string Markdown { get; } = markdown;
}

public static class MarkdownToFlowDocumentConverter
{
    public static FlowDocument Convert(string markdown)
    {
        var document = MarkdownDocument.Create(markdown);
        
        // Convert MarkdownDocument to FlowDocument
        var flowDocument = new FlowDocument();
        
        foreach (var section in document)
        {
            var lines = section.ToString().Split("\n\n");
            var paragraph = new Paragraph();
            
            foreach (var line in lines)
            {
                paragraph.Inlines.Add(new Run(line));
            }
            
            flowDocument.Blocks.Add(paragraph);
        }

        return flowDocument;
    }
}