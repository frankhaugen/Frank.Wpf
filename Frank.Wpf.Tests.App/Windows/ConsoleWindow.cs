using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Frank.Wpf.Controls.Console;
using Frank.Wpf.Controls.SimpleInputs;

namespace Frank.Wpf.Tests.App.Windows;

public class ConsoleWindow : Window
{
    private readonly ConsoleControl _consoleControl;
    
    public ConsoleWindow()
    {
        var commands = new List<IConsoleCommand>
        {
            new ShowMessageCommand()
        };
        _consoleControl = new ConsoleControl(commands);
        Title = "Console Window";
        SizeToContent = SizeToContent.WidthAndHeight;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        
        var button = new Button
        {
            Content = "Show Console Input Dialog",
            Width = 200,
            Height = 50,
            Margin = new Thickness(100),
        };
        button.Click += ButtonOnClick;
        
        var stackPanel = new StackPanel
        {
            Children =
            {
                _consoleControl,
                button
            }
        };
        
        Content = stackPanel;
    }

    private void ButtonOnClick(object sender, RoutedEventArgs e)
    {
        var dialog = new ConsoleInputDialog(_consoleControl.Writer);
        dialog.Show();
    }

    private class ShowMessageCommand : IConsoleCommand
    {
        /// <inheritdoc />
        public string CommandName => "show-message";

        /// <inheritdoc />
        public bool CanExecute(string input) => input.Split().First().Equals(CommandName, StringComparison.OrdinalIgnoreCase);

        /// <inheritdoc />
        public string Execute(string command)
        {
            var messageParts = command.Split().Skip(1);
            var message = string.Join(" ", messageParts);
            MessageBox.Show(message);
            
            return "Message shown!";
        }
    }

    private class ConsoleInputDialog : Window
    {
        private readonly TextInput _textInput;
        private readonly ConsoleControlWriter _consoleControlWriter;
        private string _text = string.Empty;

        public ConsoleInputDialog(ConsoleControlWriter consoleControlWriter)
        {
            _consoleControlWriter = consoleControlWriter;
            Title = "Console Input";
            ResizeMode = ResizeMode.NoResize;
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
            _textInput = new TextInput("Enter text:", string.Empty, TextChanged);
            {
                Width = 200;
                Height = 50;
                Margin = new Thickness(100);
            }
            _textInput.KeyDown += OnEnterKeyDown;
            
            Content = _textInput;
        }

        private void OnEnterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _consoleControlWriter.WriteAsync(_text).GetAwaiter().GetResult();
                _textInput.Clear();
                // Close();
            }
        }

        private void TextChanged(TextChangedEvent textChangedEvent)
        {
            _text = _textInput.Content;
        }
    }
}