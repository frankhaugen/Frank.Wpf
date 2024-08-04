using System.Windows;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.SimpleInputs;

public class ButtonStack : StackPanel
{
    public ButtonStack(Orientation orientation = Orientation.Horizontal)
    {
        Orientation = orientation;
    }

    public void AddButton(string content, RoutedEventHandler clickHandler)
    {
        var button = new Button
        {
            Margin = new Thickness(5),
            Content = content
        };
        button.Click += clickHandler;
        Children.Add(button);
    }
}