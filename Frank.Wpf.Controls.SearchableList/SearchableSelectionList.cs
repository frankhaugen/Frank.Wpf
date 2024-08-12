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
    private readonly CustomListBox<T> _listBox = new();

    /// <summary>
    /// Creates a new instance of <see cref="SearchableSelectionList{T}"/>.
    /// </summary>
    public SearchableSelectionList()
    {
        _searchBox = new SearchBox("Search", x =>
        {
            if (DisplayFunc != null)
            {
                _listBox.FilterFunc = item => DisplayFunc(item)
                    .Contains(x.SearchText ?? string.Empty, StringComparison.InvariantCultureIgnoreCase);
            }
        });

        _scrollViewer.Content = _listBox;
        _stackPanel.Children.Add(_searchBox);
        _stackPanel.Children.Add(_scrollViewer);
        _stackPanel.Children.Add(_groupBox);
        
        MinWidth = 128;
        Content = _stackPanel;
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

    /// <summary>
    /// The items in the list.
    /// </summary>
    public required IEnumerable<T> Items
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
        get => _listBox.DisplayFunc;
        init => _listBox.DisplayFunc = value;
    }

    /// <summary>
    /// This action is called when an item is selected.
    /// </summary>
    public required Action<T> SelectionChangedAction
    {
        init
        {
            _listBox.SelectionChangedAction = item =>
            {
                _groupBox.Content = new TextBlock { Text = DisplayFunc(item) };
                value?.Invoke(item);
            };
        }
    }
}