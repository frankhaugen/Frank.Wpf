using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Frank.Wpf.Controls.SimpleInputs;

/// <summary>
/// A big text input. This is a multiline text input with a header.
/// </summary>
public class BigTextInput : UserControl
{
    private readonly TextBox _textBox;
    private readonly GroupBox _groupBox = new();

    /// <summary>
    /// Creates a new instance of <see cref="BigTextInput"/>.
    /// </summary>
    public BigTextInput()
    {
        _textBox = new TextBox
        {
            AcceptsReturn = true,
            AcceptsTab = true
        };

        Content = _textBox;
    }
    
    public event Action<string?>? SaveKeyCombination
    {
        add => _textBox.KeyDown += (_, e) =>
        {
            if (e.KeyboardDevice.Modifiers == (ModifierKeys.Control) && e.Key == Key.S)
            {
                // Handle the Ctrl+S key combination
                e.Handled = true; // Optional: prevents the default save action if any
                value?.Invoke(_textBox.Text);
            }
        };
        remove => _textBox.KeyDown -= (_, e) =>
        {
            if (e.KeyboardDevice.Modifiers == (ModifierKeys.Control) && e.Key == Key.S)
            {
                // Handle the Ctrl+S key combination
                e.Handled = true; // Optional: prevents the default save action if any
                value?.Invoke(_textBox.Text);
            }
        };
    }
    
    public string Header
    {
        get => _groupBox.Header as string ?? string.Empty;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                _groupBox.Header = null;
                Content = _textBox;
                return;
            }
            
            _groupBox.Header = value;
            _groupBox.Content = _textBox;
            Content = _groupBox;
        }
    }

    public event Action<string?>? TextChanged
    {
        add => _textBox.TextChanged += (_, _) => value?.Invoke(_textBox.Text);
        remove => _textBox.TextChanged -= (_, _) => value?.Invoke(_textBox.Text);
    }

    public TextWrapping TextWrapping
    {
        get => _textBox.TextWrapping;
        set => _textBox.TextWrapping = value;
    }

    /// <summary>
    /// The text of the input.
    /// </summary>
    public string Text
    {
        get => _textBox.Text;
        set => _textBox.Text = value;
    }

    /// <summary>
    /// Clears the text of the input.
    /// </summary>
    public void Clear() => _textBox.Clear();
}