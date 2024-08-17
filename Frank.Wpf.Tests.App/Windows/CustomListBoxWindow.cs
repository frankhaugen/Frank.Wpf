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
    private readonly CustomListBox<MyClass> _listBox;
    
    private Func<MyClass, bool> _filterFunc;
    
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
        
        _listBox = new CustomListBox<MyClass>()
        {
            DisplayFunc = x => x.Name,
            FilterFunc = x => _filterFunc != null && _filterFunc.Invoke(x)
        };
        
        _listBox.Items = items;
        
        _searchBox = new SearchBox("Search", x =>
        {
            if (x == null)
            {
                _listBox.Items = items;
                return;
            }
            _listBox.Items = items.Where(y => y.Name.Contains(x));
        });
        
        _listBox.SelectionChanged += x =>
        {
            _groupBox.Content = x;
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