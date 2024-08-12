using System.Windows;
using System.Windows.Controls;
using Frank.Wpf.Controls.SimpleInputs;

namespace Frank.Wpf.Tests.App.Windows;

public class CustomListBoxWindow : Window
{
    private readonly StackPanel _stackPanel = new();
    private readonly SearchBox _searchBox;
    private readonly GroupBox _groupBox = new() { Header = "Selected" };
    private readonly ScrollViewer _scrollViewer = new();
    private readonly CustomListBox<MyClass> _listBox = new();
    
    public CustomListBoxWindow()
    {
        Title = "Custom ListBox Window";
        Width = 800;
        Height = 600;
        
        var items = new List<MyClass>
        {
            new MyClass { Name = "Frank", Age = 30 },
            new MyClass { Name = "John", Age = 40 },
            new MyClass { Name = "Jane", Age = 50 }
        };
        
        _listBox.Items = items;
        
        _listBox.DisplayFunc = item => item.Name;
        
        _searchBox = new SearchBox("Search", x =>
        {
            _listBox.FilterFunc = item => item.Name.Contains(x.SearchText ?? string.Empty, StringComparison.InvariantCultureIgnoreCase);
        });
        
        _listBox.SelectionChangedAction = item =>
        {
            _groupBox.Content = new TextBlock { Text = _listBox.DisplayFunc(item) };
        };
        
        _scrollViewer.Content = _listBox;
        
        _stackPanel.Children.Add(_searchBox);
        _stackPanel.Children.Add(_scrollViewer);
        _stackPanel.Children.Add(_groupBox);
        
        Content = _stackPanel;
    }
    
    class MyClass
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}