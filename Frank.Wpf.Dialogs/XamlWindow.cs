using System.Windows;
using Frank.Wpf.Controls.SimpleInputs;
using Frank.Wpf.Core;

namespace Frank.Wpf.Dialogs;

public class XamlWindow<T> : Window where T : UIElement
{
    public XamlWindow(T uiElement)
    {
        Title = "XAML of " + uiElement.GetType().Name;
        SizeToContent = SizeToContent.WidthAndHeight;
        MinWidth = 300;
        MinHeight = 250;

        MaxWidth = 800;
        MaxHeight = 600;
        
        var textBoxWithLineNumbers = new TextBoxWithLineNumbers
        {
            Text = uiElement.ToXaml()
        };
        
        Content = textBoxWithLineNumbers;
    }
}