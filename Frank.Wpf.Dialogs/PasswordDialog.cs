using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace Frank.Wpf.Dialogs;

public class PasswordDialog : Dialog<SecureString>
{
    private SecureString? _secureString;
    
    public PasswordDialog()
    {
        var label = new Label
        {
            Content = "Enter password:",
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
            _secureString = passwordBox.SecurePassword;
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
    }
    
    // public string Password { get; set; }
    /// <inheritdoc />
    protected override object? GetResultData()
    {
        return _secureString;
    }
}