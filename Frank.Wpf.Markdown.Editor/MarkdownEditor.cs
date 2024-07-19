using Frank.Wpf.Controls.Code;
using ICSharpCode.AvalonEdit.Highlighting;

namespace Frank.Wpf.Markdown.Editor;

/// <summary>
/// Represents a Markdown editor control derived from the CodeArea class.
/// </summary>
public class MarkdownEditor : CodeArea
{
    /// <inheritdoc />
    public MarkdownEditor() : base(HighlightingManager.Instance.GetDefinition("Markdown"))
    {
    }
}