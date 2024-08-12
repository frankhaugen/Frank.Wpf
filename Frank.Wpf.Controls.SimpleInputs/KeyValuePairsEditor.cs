using System.Windows.Controls;

namespace Frank.Wpf.Controls.SimpleInputs;

public class KeyValuePairsEditor : UserControl
{
    private readonly StackPanel _stackPanel;

    public KeyValuePairsEditor(string header, IEnumerable<KeyValuePair<string, string?>> keyValuePairs)
    {
        GroupBox groupBox = new()
        {
            Header = header,
            Content = _stackPanel
        };
        
        _stackPanel = new StackPanel();

        foreach (var keyValuePair in keyValuePairs)
        {
            var keyValuePairEditor = new KeyValuePairEditor(keyValuePair);
            _stackPanel.Children.Add(keyValuePairEditor);
        }
        
        Content = groupBox;
    }

    public IEnumerable<KeyValuePair<string, string?>> KeyValuePairs
    {
        get
        {
            foreach (KeyValuePairEditor keyValuePairEditor in _stackPanel.Children)
            {
                yield return keyValuePairEditor.KeyValuePair;
            }
        }
    }
}