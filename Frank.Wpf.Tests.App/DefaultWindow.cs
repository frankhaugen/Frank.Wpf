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
        Title = "Frank.Wpf.Tests.App";
        Content = _windowButtons;
        
        _windowButtons.AddButton("Show SQL Runner", (sender, args) =>
        {
            var window = new SqlLiteWindow();
            window.Show();
        });
        
        _windowButtons.AddButton("Show Json Window", (sender, args) =>
        {
            var window = new JsonWindow();
            window.Show();
        });
        
        _windowButtons.AddButton("Show Console Window", (sender, args) =>
        {
            var window = new ConsoleWindow();
            window.Show();
        });
        
        _windowButtons.AddButton("Show Custom List Box Window", (sender, args) =>
        {
            var window = new CustomListBoxWindow();
            window.Show();
        });
        
        _windowButtons.AddButton("Show Searchable Selection List Window", (sender, args) =>
        {
            var window = new SearchableSelectionListWindow();
            window.Show();
        });
        
        _windowButtons.AddButton("Show Key-Value Pair Editor Window", (sender, args) =>
        {
            var window = new KvpEditorWindow();
            window.Show();
        });
    }
}