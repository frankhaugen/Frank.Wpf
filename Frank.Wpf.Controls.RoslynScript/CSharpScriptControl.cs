using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using Frank.Wpf.Controls.SimpleInputs;

namespace Frank.Wpf.Controls.RoslynScript;

public class CSharpScriptControl : UserControl
{
    private readonly TextBoxWithLineNumbers _outputTextBlock;

    private readonly ScriptRunner _scriptRunner;
    private string _code;
    private bool _autorun;
    private readonly MenuItem _runMenuItem;

    public CSharpScriptControl()
    {
        var scriptRunnerBuilder = new ScriptRunnerBuilder()
            .WithReference(typeof(object).Assembly)
            .WithReference(typeof(Enumerable).Assembly)
            .WithReference(typeof(MessageBox).Assembly)
            .WithReference(typeof(Enumerable).Assembly)
            .WithReference(typeof(IQueryable).Assembly);
        _scriptRunner = scriptRunnerBuilder.Build();
        var menu = new Menu();
        _runMenuItem = new MenuItem { Header = "Run" };
        _runMenuItem.Click += async (sender, args) => await ExecuteScriptAsync();
        menu.Items.Add(_runMenuItem);
        
        var autorunCheckBox = CreateAutorunCheckBox();
        var autorunMenuItem = new MenuItem { Header = autorunCheckBox };
        menu.Items.Add(autorunMenuItem);
        
        _outputTextBlock = CreateOutputTextBlock();
        var inputTextBox = CreateInputTextBox();
        
        var inputGroupBox = new GroupBox
        {
            Header = "Input",
            Content = inputTextBox
        };
        
        var outputGroupBox = new GroupBox
        {
            Header = "Output",
            Content = _outputTextBlock
        };
        
        var stackPanel = new StackPanel()
        {
            Orientation = Orientation.Horizontal
        };
        stackPanel.Children.Add(inputGroupBox);
        stackPanel.Children.Add(outputGroupBox);

        var outerStackPanel = new StackPanel();
        outerStackPanel.Children.Add(menu);
        outerStackPanel.Children.Add(stackPanel);
        
        Content = outerStackPanel;
    }

    private CheckBox CreateAutorunCheckBox()
    {
        var checkBox = new CheckBox
        {
            Content = "Autorun",
            IsChecked = false
        };

        checkBox.Checked += (sender, args) =>
        {
            _autorun = true;
            _runMenuItem.IsEnabled = false;
        };

        checkBox.Unchecked += (sender, args) =>
        {
            _autorun = false;
            _runMenuItem.IsEnabled = true;
        };

        return checkBox;
    }

    private TextBoxWithLineNumbers CreateOutputTextBlock()
    {
        return new TextBoxWithLineNumbers() { Text = "Output" };
    }

    private TextBoxWithLineNumbers CreateInputTextBox()
    {
        var textBox = new TextBoxWithLineNumbers
        {
            TextWrapping = TextWrapping.Wrap,
        };

        textBox.TextChanged += async () =>
        {
            _code = textBox.Text;
            if (_autorun)
            {
                await ExecuteScriptAsync();
            }
        };

        return textBox;
    }

    private async Task ExecuteScriptAsync()
    {
        try
        {
            var result = await _scriptRunner.RunAsync(_code);
            
            var resultType = result?.GetType();
            if (resultType == null)
            {
                _outputTextBlock.Text = "null";
                return;
            }
            
            if (resultType == typeof(string))
            {
                _outputTextBlock.Text = result?.ToString() ?? "null";
                return;
            }
            
            var jsonDocument = JsonSerializer.SerializeToDocument(result);
            var jsonElement = jsonDocument.RootElement;
            var prettyPrintedJson = JsonSerializer.Serialize(jsonElement, new JsonSerializerOptions { WriteIndented = true, Converters = { new JsonStringEnumConverter()}});
            
            _outputTextBlock.Text = prettyPrintedJson;
        }
        catch (Exception ex)
        {
            _outputTextBlock.Text = ex.Message;
        }
    }
}
