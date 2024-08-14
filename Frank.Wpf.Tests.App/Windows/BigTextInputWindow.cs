using System.Windows;
using Frank.Wpf.Controls.SimpleInputs;

namespace Frank.Wpf.Tests.App.Windows;

public class BigTextInputWindow : Window
{
    private readonly BigTextInput _bigTextInput;
    
    public BigTextInputWindow()
    {
        _bigTextInput = new BigTextInput()
        {
            Header = "Big Text Input",
            Text = "Hello, World!"
        };
        
        Content = _bigTextInput;
        
        _bigTextInput.TextChanged += text => Title = text;
        
        _bigTextInput.SaveKeyCombination += text => MessageBox.Show($"Saved:\n\n{text}");
    }
}