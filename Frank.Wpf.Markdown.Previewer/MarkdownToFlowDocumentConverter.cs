using System.Windows.Documents;

namespace Frank.Wpf.Markdown.Previewer;

internal static class MarkdownToFlowDocumentConverter
{
    public static FlowDocument Convert(string markdown) => new MdXaml.Markdown().Transform(markdown);
}