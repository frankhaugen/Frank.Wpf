using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using Frank.Wpf.Controls.CompletionPopup;

public class CompletionPopup : Popup
{
    private readonly ListBox _completionListBox;
    private ICompletionSource _completionSource;
    private ICompletionTriggerRule _triggerRule;

    public CompletionPopup()
    {
        StaysOpen = false;
        _completionListBox = new ListBox();
        _completionListBox.PreviewMouseLeftButtonUp += CompletionListBox_MouseUp;
        _completionListBox.KeyDown += CompletionListBox_KeyDown;
        Child = _completionListBox;
    }

    public void Initialize(TextBox textBox, ICompletionSource completionSource, ICompletionTriggerRule triggerRule)
    {
        _completionSource = completionSource;
        _triggerRule = triggerRule;
        
        _completionListBox.ItemTemplate = CreateVisualTemplate();
        
        // Set PlacementTarget and PlacementMode
        PlacementTarget = textBox;
        Placement = PlacementMode.RelativePoint;

        textBox.PreviewKeyDown += (s, e) => HandleKeyDown(e);
        textBox.TextChanged += (s, e) => HandleTextChanged(textBox);
    }

    /// <inheritdoc />
    protected override void OnOpened(EventArgs e)
    {
        _completionListBox.Focus();
        base.OnOpened(e);
    }

    private void HandleTextChanged(TextBox textBox)
    {
        int position = textBox.CaretIndex;
        string text = textBox.Text;

        if (_triggerRule.ShouldTriggerCompletion(text, position))
        {
            var completions = _completionSource.GetCompletions(text, position);
            SetCompletionData(completions);

            var caretPosition = textBox.GetRectFromCharacterIndex(position);
            
            // Position the popup relative to the caret
            HorizontalOffset = caretPosition.Left;
            VerticalOffset = caretPosition.Bottom;
            
            IsOpen = true;
        }
        else
        {
            IsOpen = false;
        }
    }

    private void HandleKeyDown(KeyEventArgs e)
    {
        if (IsOpen && (e.Key == Key.Up || e.Key == Key.Down))
        {
            e.Handled = true;
        }
    }

    private void SetCompletionData(IEnumerable<ICompletionData> completions)
    {
        _completionListBox.Items.Clear();
        foreach (var completion in completions)
        {
            _completionListBox.Items.Add(completion);
        }

        if (_completionListBox.Items.Count > 0)
        {
            _completionListBox.SelectedIndex = 0;
            IsOpen = true;
        }
        else
        {
            IsOpen = false;
        }
    }

    public string SelectedCompletion => (_completionListBox.SelectedItem as ICompletionData)?.Text;

    // public event EventHandler<string> CompletionSelected;

    private void CompletionListBox_MouseUp(object sender, MouseButtonEventArgs e)
    {
        OnCompletionSelected();
    }
    
    public DataTemplate CreateVisualTemplate()
    {
        var type = typeof(ICompletionData);
        
        // Create a generic DataTemplate that relies on the interface's properties
        var template = new DataTemplate(type);
        
        // Create a TextBlock that binds to the Text property of the ICompletionData
        var textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
        textBlockFactory.SetBinding(TextBlock.TextProperty, new Binding(nameof(ICompletionData.Text)));

        // Optionally add a tooltip or other visual elements based on the interface properties
        if (type.GetProperty(nameof(ICompletionData.Description)) is not null)
        {
            var toolTipBinding = new Binding(nameof(ICompletionData.Description));
            textBlockFactory.SetValue(FrameworkElement.ToolTipProperty, toolTipBinding);
        }

        template.VisualTree = textBlockFactory;

        return template;
    }

    private void CompletionListBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter || e.Key == Key.Tab)
        {
            OnCompletionSelected();
            e.Handled = true;
        }
        else if (e.Key == Key.Escape)
        {
            IsOpen = false;
        }
        else if (e.Key == Key.Up || e.Key == Key.Down)
        {
            e.Handled = false; // Let the ListBox handle up/down navigation
        }
        else
        {
            IsOpen = false;
        }
    }

    private void OnCompletionSelected()
    {
        if (_completionListBox.SelectedItem != null)
        {
            CompletionPopup_CompletionSelected(this, SelectedCompletion);
            IsOpen = false;
        }
    }
    
    
    private void CompletionPopup_CompletionSelected(object? sender, string completion)
    {
        var popup = sender as CompletionPopup;
        if (popup?.PlacementTarget is not TextBox textBox)
            return;
        
        int position = textBox.CaretIndex;

        // Find the start of the word being completed
        int wordStart = textBox.Text.LastIndexOf('.', position - 1) + 1;

        textBox.Text = textBox.Text.Remove(wordStart, position - wordStart);
        textBox.Text = textBox.Text.Insert(wordStart, completion);
        textBox.CaretIndex = wordStart + completion.Length;
        
        textBox.Focus();
    }
}
