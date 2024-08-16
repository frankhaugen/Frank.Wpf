using System.Windows;
using System.Windows.Controls;
using Frank.Wpf.Controls.SimpleInputs;
using Frank.Wpf.Hosting;
using Frank.Wpf.Tests.App.Windows;

public class DefaultWindow : MainWindow
{
    private readonly ButtonStack _windowButtons = new(Orientation.Vertical);
    private const int DefaultWidth = 400;
    private const int DefaultHeight = 300;
    private readonly WindowStartupLocation _defaultPosition = WindowStartupLocation.CenterScreen;
    
    private readonly Dictionary<string, Func<Window>> _windows = new()
    {
        { "Show Big Text Input Window", () => new BigTextInputWindow() },
        { "Show Code Window", () => new CodeWindow() },
        { "Show Console Window", () => new ConsoleWindow() },
        { "Show CSharp Scripting Window", () => new CSharpScriptingWindow() },
        { "Show Custom List Box Window", () => new CustomListBoxWindow() },
        { "Show Dropdown Window", () => new DropdownWindow() },
        { "Show Json Window", () => new JsonWindow() },
        { "Show Key-Value Pair Editor Window", () => new KvpEditorWindow() },
        { "Show Paged Tab Control Window", () => new PagedTabControlWindow() },
        { "Show Searchable Selection List Window", () => new SearchableSelectionListWindow() },
        { "Show SQL Runner", () => new SqlLiteWindow() },
        { "Show Text Box With Line Numbers Window", () => new TextBoxWithLineNumbersWindow() },
        { "Show Text Completion Window", () => new TextCompletionWindow() },
        { "Show Text Label Experimentation Window", () => new TextLabelExperimentationWindow() },
        { "Show Xml Window", () => new XmlWindow() }
    };

    public DefaultWindow()
    {
        Title = "Frank.Wpf.Tests.App";
        Content = _windowButtons;
        SizeToContent = SizeToContent.WidthAndHeight;
        WindowStartupLocation = _defaultPosition;

        foreach (var (title, createWindow) in _windows)
        {
            _windowButtons.AddButton(title, (_, _) =>
            {
                var window = createWindow();
                window.MinWidth = DefaultWidth;
                window.MinHeight = DefaultHeight;
                window.WindowStartupLocation = _defaultPosition;
                window.Show();
            });
        }
    }
}
