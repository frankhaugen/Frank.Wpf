using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Frank.Wpf.Controls.SimpleInputs;
using Frank.Wpf.Core;
using Frank.Wpf.Dialogs;

namespace Frank.Wpf.Tests.App.Windows;

public class TextLabelExperimentationWindow : Window
{
    private readonly StackPanel _stackPanel = new();
    private readonly ComboBox _dropdown;
    private readonly TextLabel _textLabel;
    
    public TextLabelExperimentationWindow()
    {
        Title = "Text Label Experimentation";
        SizeToContent = SizeToContent.WidthAndHeight;
        
        _textLabel = new TextLabel
        {
            Text = "Text",
            Header = "Initial Header"
        };
        
        var items = new[] {"", "Item 2", "Item 3"};
        
        _dropdown = new ComboBox();
        
        foreach (var item in items)
        {
            _dropdown.Items.Add(item);
        }
        
        _dropdown.SelectionChanged += (sender, args) =>
        {
            var header = _dropdown.SelectedItem.As<ComboBoxItem>()?.Content.As<string>();
            
            if (header == null)
            {
                header = _dropdown.SelectedItem.As<string>();
            }
            
            _textLabel.Header = header ?? string.Empty;
        };
        
        _stackPanel.Children.Add(_dropdown);
        _stackPanel.Children.Add(_textLabel);
        
        Content = _stackPanel;
    }

    /// <inheritdoc />
    protected override void OnKeyDown(KeyEventArgs e)
    {
        // If Ctrl + Alt + D is pressed, open a new window with a readonly textbox and a button to close the window showing the XAML of the window's content
        if (e.Key == Key.D && Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Alt))
        {
            var window = new XamlWindow<StackPanel>(_stackPanel);
            window.Show();
        }
        
        if (e.Key == Key.Escape)
        {
            Close();
        }
        
        base.OnKeyDown(e);
    }
}