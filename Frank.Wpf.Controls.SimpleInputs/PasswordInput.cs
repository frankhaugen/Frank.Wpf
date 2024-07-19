using System.Windows;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.SimpleInputs;

public class PasswordInput : GroupBox
{
    private readonly PasswordBox _textBox;

    public PasswordInput(string header, string text, RoutedEventHandler passwordChanged)
    {
        Header = header;
        _textBox = new PasswordBox();
        _textBox.Password = text;
        _textBox.PasswordChanged += passwordChanged;
        base.Content = _textBox;
    }

    public new string Content => _textBox.Password;
}