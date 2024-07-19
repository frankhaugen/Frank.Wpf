using System.Windows.Controls;

namespace Frank.Wpf.Controls.SimpleInputs;

public class TextInput : GroupBox
{
    private readonly TextBox _textBox;

    public TextInput(string header, string text, Action<object, TextChangedEventArgs> textChanged)
    {
        Header = header;

        _textBox = new TextBox();
        _textBox.Text = text;
        _textBox.TextChanged += textChanged.Invoke;
        base.Content = _textBox;
    }

    public new string Content => _textBox.Text;
}