using System.Windows;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.SimpleInputs;

public class TextArea : GroupBox
{
    private readonly TextBlock _content = new();

    public TextArea(string header, string text = "")
    {
        Header = header;
        _content.Text = text;
        _content.TextWrapping = TextWrapping.Wrap;

        SizeChanged += (sender, args) => _content.Width = Width;

        var viewer = new ScrollViewer()
        {
            HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            Content = _content
        };

        base.Content = viewer;
    }

    public new string Content
    {
        get => _content.Text as string ?? string.Empty;
        set => _content.Text = value;
    }
}