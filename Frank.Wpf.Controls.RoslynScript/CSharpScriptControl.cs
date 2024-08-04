using System.Windows;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.RoslynScript;

public class CSharpScriptControl : ContentControl
{
    private StackPanel _stackPanel = new();
    private CheckBox _autorunCheckBox;
    private TextBox _inputTextBox;
    private Button _runButton;
    private TextBlock _outputTextBlock;
    
    private ScriptRunner _scriptRunner = new();
    private string _code;
    
    public CSharpScriptControl()
    {
        _autorunCheckBox = CreateAutorunCheckBox();
        _outputTextBlock = CreateOutputTextBlock();
        _runButton = CreateRunButton();
        _inputTextBox = CreateInputTextBox();
        
        _stackPanel.Children.Add(_autorunCheckBox);
        _stackPanel.Children.Add(_inputTextBox);
        _stackPanel.Children.Add(_runButton);
        _stackPanel.Children.Add(_outputTextBlock);
        
        Content = _stackPanel;
    }
    
    private CheckBox CreateAutorunCheckBox()
    {
        var checkBox = new CheckBox();
        checkBox.Content = "Autorun";
        checkBox.IsChecked = false;
        checkBox.Checked += (sender, args) => _runButton.IsEnabled = false;
        checkBox.Unchecked += (sender, args) => _runButton.IsEnabled = true;
        
        return checkBox;
    }
    
    private TextBlock CreateOutputTextBlock()
    {
        var textBlock = new TextBlock();
        textBlock.Text = "Output";
        return textBlock;
    }
    
    private Button CreateRunButton()
    {
        var button = new Button();
        button.Content = "Run";
        button.Click += Button_Click;
        return button;
    }
    
    private TextBox CreateInputTextBox()
    {
        var textBox = new TextBox();
        textBox.Text = "return \"Hello, \nWorld!\";";
        textBox.AcceptsReturn = true;
        textBox.AcceptsTab = true;
        textBox.TextWrapping = TextWrapping.Wrap;
        textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        
        textBox.TextChanged += (sender, args) =>
        {
            if (_autorunCheckBox.IsChecked == true)
            {
                _code = textBox.Text;
                try
                {
                    var result = _scriptRunner.RoslynScriptingAsync<string>(_code).Result;
                    _outputTextBlock.Text = result;
                }
                catch (Exception exception)
                {
                    // Do nothing
                }
            }
        };
        return textBox;
    }
    
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        _code = _inputTextBox.Text;
        try
        {
            var result = _scriptRunner.RoslynScriptingAsync<string>(_code).Result;
            _outputTextBlock.Text = result;
        }
        catch (Exception exception)
        {
            _outputTextBlock.Text = exception.Message;
        }
    }
}