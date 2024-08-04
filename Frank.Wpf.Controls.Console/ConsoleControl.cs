using System.Text;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Frank.Wpf.Controls.Console;

    public class ConsoleControl : ContentControl
    {
        private readonly Stack<string> _history = new();
        private readonly Channel<string> _channel = Channel.CreateUnbounded<string>();
        private readonly ChannelWriter<string> _writer;

        private readonly TextBox _inputTextBox;
        private readonly TextBlock _outputTextBlock;
        private readonly ScrollViewer _scrollViewer;
        
        private readonly IEnumerable<IConsoleCommand> _commands;

        public ConsoleControl(IEnumerable<IConsoleCommand>? commands = null)
        {
            _commands = commands ?? [];
            
            // Create the layout
            var panel = new StackPanel();

            _outputTextBlock = new TextBlock
            {
                VerticalAlignment = VerticalAlignment.Top,
                TextWrapping = TextWrapping.Wrap,
                DataContext = _history,
                Background = Brushes.Black,
                Foreground = Brushes.White,
                FontFamily = new FontFamily("Consolas"),
                FontSize = 14,
                Padding = new Thickness(5)
            };
            
            _scrollViewer = new ScrollViewer
            {
                MaxHeight = 300,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Content = _outputTextBlock
            };

            _inputTextBox = new TextBox
            {
                VerticalAlignment = VerticalAlignment.Bottom
            };

            _inputTextBox.KeyDown += OnInputKeyDown;

            panel.Children.Add(_scrollViewer);
            panel.Children.Add(_inputTextBox);

            Content = panel;
            StartListening();
            
            _writer = _channel.Writer;
        }

        private void OnInputKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !string.IsNullOrWhiteSpace(_inputTextBox.Text))
            {
                WriteToHistory(_inputTextBox.Text);
                ExecuteCommand(_inputTextBox.Text);
                _inputTextBox.Clear();
            }
        }

        private void ExecuteCommand(string command)
        {
            foreach (var consoleCommand in _commands)
            {
                if (consoleCommand.CanExecute(command))
                {
                    var stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine($"Running command: {consoleCommand.CommandName}");
                    try
                    {
                        var result = consoleCommand.Execute(command);
                        stringBuilder.AppendLine(result);
                    }
                    catch (Exception e)
                    {
                        stringBuilder.AppendLine($"An error occurred: {e.Message}");
                    }
                    WriteToHistory(stringBuilder.ToString());
                }
            }
        }

        private void WriteToHistory(string message)
        {
            _outputTextBlock.Text += $"\n> {message}";
            _scrollViewer.ScrollToBottom();
        }

        private async void StartListening()
        {
            await foreach (var message in _channel.Reader.ReadAllAsync())
            {
                WriteToHistory(message);
            }
        }

        private bool _hasInput;

        public bool HasInput
        {
            get => _hasInput;
            set
            {
                _inputTextBox.Visibility = value == false ? Visibility.Collapsed : Visibility.Visible;
                _hasInput = value;
            }
        }

        public ConsoleControlWriter Writer => new(_writer);
    }