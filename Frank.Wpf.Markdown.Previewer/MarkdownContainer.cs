using System.Text;
using System.Windows;
using Frank.Markdown;

namespace Frank.Wpf.Markdown.Previewer;

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