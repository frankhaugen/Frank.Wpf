using System.Windows.Controls;

namespace Frank.Wpf.Controls.SimpleInputs;

public class SearchBox : GroupBox
{
    private readonly TextBox _textBox = new();

    public SearchBox(string header, Action<string?> searchTextChanged)
    {
        Header = header;
        _textBox.TextChanged += (_, _) => searchTextChanged(_textBox.Text);
        Content = _textBox;
    }
}