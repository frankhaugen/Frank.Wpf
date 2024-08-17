using System.Windows;
using System.Windows.Controls;
using Frank.Wpf.Controls.SimpleInputs;

namespace Frank.Wpf.Controls.SearchableList;

/// <summary>
/// A list that allows searching and selecting items.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class SearchableSelectionList<T> : UserControl
{
    private readonly StackPanel _stackPanel = new();
    private readonly SearchBox _searchBox;
    private readonly GroupBox _groupBox = new() { Header = "Selected" };
    private readonly ScrollViewer _scrollViewer = new();
    private readonly CustomListBox<T> _listBox;
    private Func<T,string> _displayFunc;

    /// <summary>
    /// Creates a new instance of <see cref="SearchableSelectionList{T}"/>.
    /// </summary>
    public SearchableSelectionList()
    {
        _listBox = new CustomListBox<T>()
        {
            DisplayFunc = x => _displayFunc(x),
            FilterFunc = x => FilterFunc(x)
        };
        
        _listBox.SelectionChanged += x =>
        {
            _groupBox.Content = _displayFunc?.Invoke(x);
            
            _listBox.SetSelectedItem(x);
        };
        
        _searchBox = new SearchBox("Search", x =>
        {
            Items = _listBox.Items.Where(FilterFunc);
        });

        _scrollViewer.Content = _listBox;
        _stackPanel.Children.Add(_searchBox);
        _stackPanel.Children.Add(_scrollViewer);
        _stackPanel.Children.Add(_groupBox);
        
        Items = Array.Empty<T>();
        
        MinWidth = 128;
        Content = _stackPanel;
        
        _listBox.SetSelectedItem(default);
    }

    private bool FilterFunc(T arg)
    {
        return true;
    }

    public void SetSelectedItem(T item)
    {
        _listBox.SetSelectedItem(item);
    }

    /// <summary>
    /// The items in the list.
    /// </summary>
    public IEnumerable<T> Items
    {
        get => _listBox.Items;
        set => _listBox.Items = value;
    }

    /// <summary>
    /// The function that determines how to display an item.
    /// </summary>
    /// <remarks> Search functionality is based on this function. </remarks>
    public required Func<T, string> DisplayFunc
    {
        get => _displayFunc;
        init => _displayFunc = value;
    }

    /// <summary>
    /// This action is called when an item is selected.
    /// </summary>
    public event Action<T>? SelectionChangedAction
    {
        add => _listBox.SelectionChanged += value;
        remove => _listBox.SelectionChanged -= value;
    }
    
    /// <summary>
    /// Determines if the search box is visible. Default is true.
    /// </summary>
    public bool IsSearchBoxVisible
    {
        get => _searchBox.Visibility == Visibility.Visible;
        set => _searchBox.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
    }
    
    /// <summary>
    /// Determines if the selected item box is visible. Default is true.
    /// </summary>
    public bool IsSelectedBoxVisible
    {
        get => _groupBox.Visibility == Visibility.Visible;
        set => _groupBox.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
    }
}