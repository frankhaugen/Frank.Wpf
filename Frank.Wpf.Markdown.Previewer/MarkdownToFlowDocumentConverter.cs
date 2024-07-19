using System.Windows.Documents;
using Frank.Markdown;

namespace Frank.Wpf.Markdown.Previewer;

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