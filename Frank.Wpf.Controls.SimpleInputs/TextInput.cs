using System.Windows.Controls;
using Frank.Wpf.Core;

namespace Frank.Wpf.Controls.SimpleInputs;

public class TextInput : GroupBox
{
    private readonly TextBox _textBox;
    
    public TextInput(string header, Action<TextChangedEvent> textChanged) : this(header, null, textChanged) 
    {
    }

    public TextInput(string header, string? text, Action<TextChangedEvent> textChanged)
    {
        Header = header;

        _textBox = new TextBox();
        _textBox.Text = text;
        _textBox.TextChanged += (sender, args) =>
        {
            var oldText = args.OriginalSource.As<TextBox>()!.Text;
            var newText = _textBox.Text;
            textChanged(new TextChangedEvent(oldText, newText));
        };
        base.Content = _textBox;
    }

    public new string Content => _textBox.Text;

    public void Clear() => _textBox.Clear();
}