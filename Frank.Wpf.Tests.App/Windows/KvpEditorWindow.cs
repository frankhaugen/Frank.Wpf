using System.Windows;
using System.Windows.Controls;
using Frank.Wpf.Controls.SimpleInputs;

namespace Frank.Wpf.Tests.App.Windows;

public class KvpEditorWindow : Window
{
    public KvpEditorWindow()
    {
        Title = "Key-Value Pair Editor";
        Width = 400;
        Height = 300;
        var stackPanel = new StackPanel();
        Content = stackPanel;

        var keyValuePair = new Dictionary<string, string>
        {
            {"Key1", "Value1"},
            {"Key2", "Value2"},
            {"Key3", "Value3"},
            {"Key4", "Value4"},
            {"Key5", "Value5"}
        };
        
        foreach (var kvp in keyValuePair)
        {
            var kvpEditor = new KeyValuePairEditor(kvp);
            stackPanel.Children.Add(kvpEditor);
        }
    }
    
}