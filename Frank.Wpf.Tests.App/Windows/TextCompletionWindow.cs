using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Frank.Wpf.Controls.CompletionPopup;

namespace Frank.Wpf.Tests.App.Windows;

public class TextCompletionWindow : Window
{
    private readonly CompletionPopup _completionPopup = new();
    private readonly TextBox _scriptTextBox = new()
    {
        AcceptsReturn = true,
        AcceptsTab = true,
        HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
        VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
        TextWrapping = TextWrapping.Wrap,
        FontFamily = new FontFamily("Consolas"),
        FontSize = 14,
        Text = "Console"
    };
    
    public TextCompletionWindow()
    {
        var completions = new List<ICompletionData>
        {
            new CompletionData("WriteLine"),
            new CompletionData("Write"),
            new CompletionData("ReadLine")
        };

        // Initialize the CompletionPopup with a basic completion source and trigger rule
        _completionPopup.Initialize(_scriptTextBox, new BasicCompletionSource(completions), new DotCompletionTriggerRule());

        // Handle the completion selection event
        // _completionPopup.CompletionSelected += CompletionPopup_CompletionSelected;
        
        Content = _scriptTextBox;
    }

    
    public class BasicCompletionSource : ICompletionSource
    {
        private readonly List<ICompletionData> _completions;

        public BasicCompletionSource(IEnumerable<ICompletionData> completions)
        {
            _completions = completions.ToList();
        }

        public IEnumerable<ICompletionData> GetCompletions(string text, int position)
        {
            return _completions;
        }
    }
    
    public class DotCompletionTriggerRule : ICompletionTriggerRule
    {
        public bool ShouldTriggerCompletion(string text, int position)
        {
            return position > 0 && text[position - 1] == '.';
        }
    }
}