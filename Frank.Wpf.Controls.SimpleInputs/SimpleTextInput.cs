using System.Windows.Controls;

namespace Frank.Wpf.Controls.SimpleInputs;

public class SimpleTextInput : UserControl
{
    private readonly TextBox _textBox;

    public SimpleTextInput(string? text = "", Action<string>? textChanged = null)
    {
        _textBox = new TextBox
        {
            Text = text ?? string.Empty
        };
        
        if (textChanged != null)
        {
            _textBox.TextChanged += (_, _) =>
            {
                textChanged(_textBox.Text);
            };
        }
        
        Content = _textBox;
    }

    public string Text
    {
        get => _textBox.Text;
        set => _textBox.Text = value;
    }

    public bool IsReadOnly
    {
        get => _textBox.IsReadOnly;
        set => _textBox.IsReadOnly = value;
    }

    public void Clear() => _textBox.Clear();
}