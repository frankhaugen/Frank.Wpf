using System.Windows;
using System.Windows.Controls;

namespace Frank.Wpf.Dialogs;

public class PasswordDialog : Window
{
    public PasswordDialog()
    {
        Title = "Enter Password";
        Width = 250;
        Height = 100;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        ShowInTaskbar = false;
        ResizeMode = ResizeMode.NoResize;
        WindowStyle = WindowStyle.ToolWindow;
        Topmost = true;
        ShowActivated = true;
    }
    
    public string Password { get; set; }
    
    public bool ShowDialog(string message)
    {
        var label = new Label
        {
            Content = message,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        
        var passwordBox = new PasswordBox
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Width = 200
        };
        
        var button = new Button
        {
            Content = "OK",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        
        button.Click += (sender, args) =>
        {
            Password = passwordBox.Password;
            DialogResult = true;
        };
        
        var stackPanel = new StackPanel
        {
            Children =
            {
                label,
                passwordBox,
                button
            }
        };
        
        Content = stackPanel;
        
        return ShowDialog() == true;
    }
}