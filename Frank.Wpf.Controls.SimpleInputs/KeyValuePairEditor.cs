using System.Windows;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.SimpleInputs;

public class KeyValuePairEditor : UserControl
{
    private readonly SimpleTextInput _keyTextBox;

    public KeyValuePairEditor(KeyValuePair<string, string?> keyValuePair)
    {
        KeyValuePair = keyValuePair;
        
        _keyTextBox = new(keyValuePair.Key, text => keyValuePair = new(text, KeyValuePair.Value))
        {
            Margin = new Thickness(0, 0, 5, 0),
            MinWidth = 100
        };

        SimpleTextInput valueTextBox = new(keyValuePair.Value, text => keyValuePair = new(KeyValuePair.Key, text))
        {
            Margin = new Thickness(5, 0, 0, 0),
            MinWidth = 100
        };

        var grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        grid.Children.Add(_keyTextBox);
        grid.Children.Add(valueTextBox);
        Grid.SetColumn(valueTextBox, 1);
        Grid.SetColumn(_keyTextBox, 0);
        
        Content = grid;
    }

    public bool KeyIsReadOnly
    {
        get => _keyTextBox.IsReadOnly;
        set => _keyTextBox.IsReadOnly = value;
    }
    
    public KeyValuePair<string, string?> KeyValuePair { get; private set; }
}