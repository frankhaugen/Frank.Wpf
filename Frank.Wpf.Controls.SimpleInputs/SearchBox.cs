using System.Windows.Controls;

namespace Frank.Wpf.Controls.SimpleInputs;

/// <summary>
/// A custom search box control that wraps a TextBox inside a GroupBox.
/// The GroupBox header defaults to "Search".
/// </summary>
public class SearchBox : UserControl
{
    private readonly TextBox _textBox;
    private readonly GroupBox _groupBox;

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchBox"/> class.
    /// The header defaults to "Search".
    /// </summary>
    public SearchBox()
    {
        // Create the layout
        _groupBox = new GroupBox
        {
            Header = "Search" // Default header value
        };

        // Search text box
        _textBox = new TextBox();
        _textBox.TextChanged += (_, _) => SearchTextChanged?.Invoke(_textBox.Text);

        // Set the content of the GroupBox to the TextBox
        _groupBox.Content = _textBox;

        // Set the content of the UserControl to the GroupBox
        Content = _groupBox;
    }

    /// <summary>
    /// Gets or sets the search text in the search box.
    /// </summary>
    public string? SearchText
    {
        get => _textBox.Text;
        set => _textBox.Text = value ?? string.Empty;
    }

    /// <summary>
    /// Gets or sets the header for the GroupBox.
    /// The default value is "Search".
    /// </summary>
    public string? Header
    {
        get => _groupBox.Header as string;
        set => _groupBox.Header = value;
    }

    /// <summary>
    /// Occurs when the text in the search box changes.
    /// </summary>
    public event Action<string?>? SearchTextChanged;
}