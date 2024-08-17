using System.Windows;
using System.Windows.Controls;
using Frank.Wpf.Controls.SimpleInputs;

namespace Frank.Wpf.Controls.SearchableList;

/// <summary>
/// A list that allows searching and selecting items.
/// </summary>
/// <typeparam name="T">The type of the items in the list.</typeparam>
public sealed class SearchableSelectionList<T> : UserControl
{
    private readonly StackPanel _stackPanel = new();
    private readonly SearchBox _searchBox = new() { Header = "Search" };
    private readonly GroupBox _groupBox = new() { Header = "Selected" };
    private readonly ScrollViewer _scrollViewer = new();
    private readonly CustomListBox<T> _listBox;

    /// <summary>
    /// Initializes a new instance of <see cref="SearchableSelectionList{T}"/>.
    /// </summary>
    public SearchableSelectionList()
    {
        _listBox = new CustomListBox<T> ();

        _listBox.SelectionChanged += OnSelectionChanged;

        _searchBox.SearchTextChanged += OnSearchTextChanged;

        _scrollViewer.Content = _listBox;
        _stackPanel.Children.Add(_searchBox);
        _stackPanel.Children.Add(_scrollViewer);
        _stackPanel.Children.Add(_groupBox);

        MinWidth = 128;
        Content = _stackPanel;
    }

    /// <summary>
    /// Gets or sets the collection of items in the list.
    /// </summary>
    public IEnumerable<T> Items
    {
        get => _listBox.Items;
        set => _listBox.Items = value;
    }

    /// <summary>
    /// Gets or sets the selected item in the list.
    /// </summary>
    public T? SelectedItem
    {
        get => _listBox.SelectedItem;
        set => _listBox.SetSelectedItem(value);
    }

    /// <summary>
    /// Gets or sets the function used to display an item as a string.
    /// </summary>
    public Func<T, string>? Display
    {
        get => _listBox.DisplayFunc;
        set => _listBox.DisplayFunc = value;
    }

    /// <summary>
    /// Gets or sets the function used to filter items based on the search query.
    /// </summary>
    public Func<T, string?, bool>? Filter
    {
        get => (arg1, s) => _listBox.FilterFunc!.Invoke(arg1) || string.IsNullOrEmpty(s);
        set => _listBox.FilterFunc = arg => value?.Invoke(arg, _searchBox.SearchText) ?? true;
    }

    /// <summary>
    /// Occurs when an item is selected.
    /// </summary>
    public event Action<T>? SelectionChangedAction;

    /// <summary>
    /// Gets or sets a value indicating whether the search box is visible.
    /// </summary>
    public bool IsSearchBoxVisible
    {
        get => _searchBox.Visibility == Visibility.Visible;
        set => _searchBox.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the selected item box is visible.
    /// </summary>
    public bool IsSelectedBoxVisible
    {
        get => _groupBox.Visibility == Visibility.Visible;
        set => _groupBox.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
    }

    public IEnumerable<T> DisplayedItems => _listBox.DisplayedItems;

    /// <summary>
    /// Handles changes to the search text and filters the items in the list.
    /// </summary>
    private void OnSearchTextChanged(string? searchText)
    {
        _listBox.Items = Items.Where(item => Filter(item, searchText));
    }

    /// <summary>
    /// Handles item selection changes and updates the selected item box.
    /// </summary>
    private void OnSelectionChanged(T? selectedItem)
    {
        if (selectedItem != null)
        {
            _groupBox.Content = new TextBlock { Text = Display(selectedItem) };
        }
        else
        {
            _groupBox.Content = null;
        }

        SelectionChangedAction?.Invoke(selectedItem);
    }
}
