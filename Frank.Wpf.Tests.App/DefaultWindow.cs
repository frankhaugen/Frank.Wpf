using System.Windows;
using System.Windows.Controls;
using Frank.Wpf.Controls.SimpleInputs;
using Frank.Wpf.Hosting;
using Frank.Wpf.Tests.App.Windows;

namespace Frank.Wpf.Tests.App;

public class DefaultWindow : MainWindow
{
    private readonly ButtonStack _windowButtons = new(Orientation.Vertical);
    
    public DefaultWindow()
    {
        var defaultWidth = 400;
        var defaultHeight = 300;
        var defaultPosition = WindowStartupLocation.CenterScreen;
        
        Title = "Frank.Wpf.Tests.App";
        Content = _windowButtons;
        SizeToContent = SizeToContent.WidthAndHeight;
        WindowStartupLocation = defaultPosition;
        
        _windowButtons.AddButton("Show SQL Runner", (sender, args) =>
        {
            var window = new SqlLiteWindow
            {
                MinWidth = defaultWidth,
                MinHeight = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
        
        _windowButtons.AddButton("Show Json Window", (sender, args) =>
        {
            var window = new JsonWindow
            {
                MinWidth = defaultWidth,
                MinHeight = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
        
        _windowButtons.AddButton("Show Console Window", (sender, args) =>
        {
            var window = new ConsoleWindow
            {
                MinWidth = defaultWidth,
                MinHeight = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
        
        _windowButtons.AddButton("Show Custom List Box Window", (sender, args) =>
        {
            var window = new CustomListBoxWindow
            {
                MinWidth = defaultWidth,
                MinHeight = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
        
        _windowButtons.AddButton("Show Searchable Selection List Window", (sender, args) =>
        {
            var window = new SearchableSelectionListWindow
            {
                MinWidth = defaultWidth,
                MinHeight = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
        
        _windowButtons.AddButton("Show Key-Value Pair Editor Window", (sender, args) =>
        {
            var window = new KvpEditorWindow
            {
                MinWidth = defaultWidth,
                MinHeight = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
        
        _windowButtons.AddButton("Show Dropdown Window", (sender, args) =>
        {
            var window = new DropdownWindow
            {
                MinWidth = defaultWidth,
                MinHeight = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
        
        _windowButtons.AddButton("Show Paged Tab Control Window", (sender, args) =>
        {
            var window = new PagedTabControlWindow
            {
                MinWidth = defaultWidth,
                MinHeight = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
        
        _windowButtons.AddButton("Show Big Text Input Window", (sender, args) =>
        {
            var window = new BigTextInputWindow
            {
                MinWidth = defaultWidth,
                MinHeight = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
        
        _windowButtons.AddButton("Show Text Box With Line Numbers Window", (sender, args) =>
        {
            var window = new TextBoxWithLineNumbersWindow
            {
                MinWidth = defaultWidth,
                MinHeight = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
        
        _windowButtons.AddButton("Show Text Label Experimentation Window", (sender, args) =>
        {
            var window = new TextLabelExperimentationWindow
            {
                MinWidth = defaultWidth,
                MinHeight = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
        
        _windowButtons.AddButton("Show Text Completion Window", (sender, args) =>
        {
            var window = new TextCompletionWindow
            {
                MinWidth = defaultWidth,
                MinHeight = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
        
        _windowButtons.AddButton("Show Code Window", (sender, args) =>
        {
            var window = new CodeWindow
            {
                MinWidth = defaultWidth,
                MinHeight = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
        
        _windowButtons.AddButton("Show Xml Window", (sender, args) =>
        {
            var window = new XmlWindow
            {
                MinWidth = defaultWidth,
                MinHeight = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
    }
}