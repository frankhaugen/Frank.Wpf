using System.Windows.Controls;

namespace Frank.Wpf.Controls.SimpleInputs;

public class TextInput : UserControl
{
    private readonly SimpleTextInput _simpleTextInput;
    private readonly GroupBox _groupBox;
    
    public TextInput(string header, Action<string?> textChanged) : this(header, null, textChanged) 
    {
    }

    public TextInput(string header, string? text, Action<string?> textChanged)
    {
        _simpleTextInput = new SimpleTextInput(text, textChanged);
        _groupBox = new GroupBox
        {
            Header = header,
            Content = _simpleTextInput
        };

        Content = _groupBox;
    }

    public string Text
    {
        get => _simpleTextInput.Text;
        set => _simpleTextInput.Text = value;
    }

    public void Clear() => _simpleTextInput.Clear();
}