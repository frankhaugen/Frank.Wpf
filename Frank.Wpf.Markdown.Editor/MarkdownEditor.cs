using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Frank.Wpf.Controls.Code;
using ICSharpCode.AvalonEdit.Highlighting;

namespace Frank.Wpf.Markdown.Editor;

/// <summary>
/// Represents a Markdown editor control derived from the CodeArea class.
/// </summary>
public class MarkdownEditor : UserControl
{
    private readonly CodeArea _codeArea;
    
    /// <inheritdoc />
    public MarkdownEditor()
    {
        _codeArea = new CodeArea(HighlightingManager.Instance.GetDefinition("MarkDown"))
        {
            FontFamily = new("Consolas"),
            FontSize = 12,
            ShowLineNumbers = true,
            WordWrap = true,
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto
        };
        
        Content = _codeArea;
    }
    
    public string Markdown
    {
        get => _codeArea.Text;
        set => _codeArea.Text = value;
    }
    
    public event EventHandler? MarkdownChanged
    {
        add => _codeArea.TextChanged += value;
        remove => _codeArea.TextChanged -= value;
    }
    
}