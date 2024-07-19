using System.Windows.Controls;
using ICSharpCode.AvalonEdit.Highlighting;

namespace Frank.Wpf.Controls.Code;

/// <summary>
/// Represents a code area control derived from the TextEditor class.
/// </summary>
public class CodeArea : ICSharpCode.AvalonEdit.TextEditor
{
    public CodeArea(IHighlightingDefinition highlightingDefinition)
    {
        SyntaxHighlighting = highlightingDefinition;
        FontFamily = new("Consolas");
        FontSize = 12;
        ShowLineNumbers = true;
        WordWrap = true;
        VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
    }
}