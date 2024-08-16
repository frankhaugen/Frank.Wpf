using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Frank.Wpf.Controls.SimpleInputs
{
    public class TextBoxWithLineNumbers : UserControl
    {
        private readonly TextBox _textBox;
        private readonly TextBlock _lineNumbers;

        public TextBoxWithLineNumbers()
        {
            // Initialize TextBlock for line numbers
            _lineNumbers = new TextBlock
            {
                Background = Brushes.LightGray,
                VerticalAlignment = VerticalAlignment.Top,
                TextAlignment = TextAlignment.Right,
                Padding = new Thickness(5, 0, 10, 0),
                FontFamily = new FontFamily("Consolas"),
                FontSize = 14
            };

            // Initialize TextBox for text input
            _textBox = new TextBox
            {
                AcceptsReturn = true,
                AcceptsTab = true,
                FontFamily = new FontFamily("Consolas"),
                FontSize = 14,
                Padding = new Thickness(5, 0, 5, 0),
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden
            };

            // Set up event handlers
            _textBox.TextChanged += (s, e) => UpdateLineNumbers();
            _textBox.SizeChanged += (s, e) => UpdateLineNumbers();
            _textBox.LayoutUpdated += (s, e) => UpdateLineNumbers();

            // Create a Grid to hold the line numbers and the TextBox
            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            grid.Children.Add(_lineNumbers);
            grid.Children.Add(_textBox);
            Grid.SetColumn(_textBox, 1);

            // Wrap the grid in a ScrollViewer
            var scrollViewer = new ScrollViewer
            {
                Content = grid,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto
            };

            Content = scrollViewer;

            UpdateLineNumbers();
            
            // Scroll to the left when the control is loaded
            Loaded += (s, e) =>
            {
                scrollViewer.ScrollToHorizontalOffset(0);
            };
        }
        
        public void Clear() => _textBox.Clear();
        public int LineCount => _textBox.LineCount;
        public string GetLineText(int lineIndex) => _textBox.GetLineText(lineIndex);
        
        public TextWrapping TextWrapping
        {
            get => _textBox.TextWrapping;
            set => _textBox.TextWrapping = value;
        }

        public string Text
        {
            get => _textBox.Text;
            set => _textBox.Text = value;
        }

        public new FontFamily FontFamily
        {
            get => _textBox.FontFamily;
            set
            {
                _textBox.FontFamily = value;
                _lineNumbers.FontFamily = value;
            }
        }
        
        public new double FontSize
        {
            get => _textBox.FontSize;
            set
            {
                _textBox.FontSize = value;
                _lineNumbers.FontSize = value;
            }
        }
        
        public event Action<string?>? SaveKeyCombination
        {
            add => _textBox.KeyDown += (_, e) =>
            {
                if (e.KeyboardDevice.Modifiers == (ModifierKeys.Control) && e.Key == Key.S)
                {
                    // Handle the Ctrl+S key combination
                    e.Handled = true; // Optional: prevents the default save action if any
                    value?.Invoke(_textBox.Text);
                }
            };
            remove => _textBox.KeyDown -= (_, e) =>
            {
                if (e.KeyboardDevice.Modifiers == (ModifierKeys.Control) && e.Key == Key.S)
                {
                    // Handle the Ctrl+S key combination
                    e.Handled = true; // Optional: prevents the default save action if any
                    value?.Invoke(_textBox.Text);
                }
            };
        }

        private void UpdateLineNumbers()
        {
            var lineCount = _textBox.LineCount;
            if (lineCount < 1)
            {
                lineCount = 1;
            }
            var lines = Enumerable.Range(1, lineCount).Select(i => i.ToString()).ToArray();
            _lineNumbers.Text = string.Join(Environment.NewLine, lines);
        }

        public event Action TextChanged;
    }
}
