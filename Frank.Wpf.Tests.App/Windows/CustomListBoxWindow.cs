using System.Windows;
using System.Windows.Controls;
using Frank.Wpf.Controls.SimpleInputs;

namespace Frank.Wpf.Tests.App.Windows;

public class CustomListBoxWindow : Window
{
    private readonly StackPanel _stackPanel = new();
    private readonly GroupBox _groupBox = new() { Header = "Selected" };
    private readonly ScrollViewer _scrollViewer = new();
    
    private readonly SearchBox _searchBox;
    private readonly CustomListBox<MyClass> _listBox;
    
    public CustomListBoxWindow()
    {
        Title = "Custom ListBox Window";
        Width = 800;
        Height = 600;
        
        _listBox = new CustomListBox<MyClass>
        {
            DisplayFunc = x => x.Name,
            FilterFunc = x => true,
            Items = new List<MyClass>
            {
                new MyClass { Name = "Frank", Age = 30 },
                new MyClass { Name = "John", Age = 40 },
                new MyClass { Name = "Jane", Age = 50 }
            }
        };

        _searchBox = new SearchBox
        {
            Header = "Search"
        };
        
        _searchBox.SearchTextChanged += x =>
        {
            _listBox.FilterFunc = y => x == null || y.Name.Contains(x, StringComparison.OrdinalIgnoreCase);
            _listBox.InvalidateVisual();
        };
        
        _listBox.SelectionChanged += x =>
        {
            _groupBox.Content = _listBox.DisplayFunc(x ?? new MyClass());
            _listBox.InvalidateVisual();
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