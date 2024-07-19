using System.Windows;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.SimpleInputs;

public class ButtonInput : Button
{
    public ButtonInput(string text, RoutedEventHandler buttonClicked)
    {
        Content = text;
        Click += buttonClicked;
    }

    public new string Content
    {
        get => base.Content as string ?? string.Empty;
        set => base.Content = value;
    }
}