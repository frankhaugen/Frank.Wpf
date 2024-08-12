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
        Width = defaultWidth;
        Height = defaultHeight;
        WindowStartupLocation = defaultPosition;
        
        _windowButtons.AddButton("Show SQL Runner", (sender, args) =>
        {
            var window = new SqlLiteWindow
            {
                Width = defaultWidth,
                Height = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
        
        _windowButtons.AddButton("Show Json Window", (sender, args) =>
        {
            var window = new JsonWindow
            {
                Width = defaultWidth,
                Height = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
        
        _windowButtons.AddButton("Show Console Window", (sender, args) =>
        {
            var window = new ConsoleWindow
            {
                Width = defaultWidth,
                Height = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
        
        _windowButtons.AddButton("Show Custom List Box Window", (sender, args) =>
        {
            var window = new CustomListBoxWindow
            {
                Width = defaultWidth,
                Height = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
        
        _windowButtons.AddButton("Show Searchable Selection List Window", (sender, args) =>
        {
            var window = new SearchableSelectionListWindow
            {
                Width = defaultWidth,
                Height = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
        
        _windowButtons.AddButton("Show Key-Value Pair Editor Window", (sender, args) =>
        {
            var window = new KvpEditorWindow
            {
                Width = defaultWidth,
                Height = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
        
        _windowButtons.AddButton("Show Dropdown Window", (sender, args) =>
        {
            var window = new DropdownWindow
            {
                Width = defaultWidth,
                Height = defaultHeight,
                WindowStartupLocation = defaultPosition
            };
            window.Show();
        });
    }
}