using System.Windows.Controls;
using Frank.Wpf.Core;

namespace Frank.Wpf.Controls.SimpleInputs;

/// <summary>
/// A custom ListBox control that displays a collection of items of type <typeparamref name="T"/>.
/// Supports filtering, custom display functions, and selection handling.
/// </summary>
/// <typeparam name="T">The type of items to display in the ListBox.</typeparam>
public class CustomListBox<T> : UserControl
{
    private readonly ListBox _listBox = new();

    private IEnumerable<T>? _items;
    private Func<T, bool>? _filterFunc;
    private Func<T, string>? _displayFunc;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomListBox{T}"/> class.
    /// Sets up event handling for selection changes and initializes the ListBox control.
    /// </summary>
    public CustomListBox()
    {
        Content = _listBox;
        _filterFunc = _ => true;
        _listBox.SelectionMode = SelectionMode.Single;
        _listBox.SelectionChanged += ListBox_SelectionChanged;
    }

    /// <summary>
    /// Gets the currently selected item in the ListBox.
    /// Returns null if no item is selected or the selection is invalid.
    /// </summary>
    public T? SelectedItem => _listBox.SelectedItem is T item ? item : default;

    /// <summary>
    /// Gets or sets the collection of items to be displayed in the ListBox.
    /// This property also triggers the application of filtering and display transformations.
    /// </summary>
    public IEnumerable<T> Items
    {
        get => _items ?? Enumerable.Empty<T>();
        set => SetItems(value);
    }
    
    /// <summary>
    /// Gets the collection of items currently displayed in the ListBox.
    /// This reflects the items after applying any filters and sorting.
    /// </summary>
    public IEnumerable<T> DisplayedItems => _listBox.ItemsSource.Cast<T>();

    /// <summary>
    /// Gets or sets the filtering function to determine which items should be displayed.
    /// The filter is applied automatically whenever this function is set or the <see cref="Items"/> property is updated.
    /// </summary>
    public Func<T, bool>? FilterFunc
    {
        get => _filterFunc;
        set
        {
            _filterFunc = value;
            ApplyFilterAndSort();  // Apply filtering when filter function changes
        }
    }

    /// <summary>
    /// Gets or sets the function to convert items of type <typeparamref name="T"/> to their string representations.
    /// This function is used to display items in the ListBox and is applied whenever this function or the <see cref="Items"/> property is updated.
    /// </summary>
    public Func<T, string>? DisplayFunc
    {
        get => _displayFunc;
        set
        {
            _displayFunc = value;
            ApplyFilterAndSort();  // Apply sorting when display function changes
        }
    }

    /// <summary>
    /// Occurs when the selection of the ListBox changes.
    /// The event handler provides the newly selected item or null if no item is selected.
    /// </summary>
    public event Action<T?>? SelectionChanged;

    /// <summary>
    /// Programmatically sets the selected item in the ListBox.
    /// </summary>
    /// <param name="item">The item to select. Can be null to clear the selection.</param>
    public void SetSelectedItem(T? item) => _listBox.SelectedItem = item;

    /// <summary>
    /// Sets the items for the ListBox and applies filtering and sorting as necessary.
    /// </summary>
    /// <param name="value">The collection of items to set.</param>
    private void SetItems(IEnumerable<T>? value)
    {
        _items = value;
        ApplyFilterAndSort();  // Apply filtering and sorting on item change
    }

    /// <summary>
    /// Applies the filtering and display logic to the ListBox.
    /// Filters items using the <see cref="FilterFunc"/> and orders them using <see cref="DisplayFunc"/>.
    /// Updates the ListBox's <see cref="ItemsSource"/> property accordingly.
    /// </summary>
    private void ApplyFilterAndSort()
    {
        if (_items == null)
        {
            _listBox.ItemsSource = null;
            return;
        }

        // Start with all items
        var filteredItems = _items;

        // Apply filter if available
        if (FilterFunc != null)
        {
            filteredItems = filteredItems.Where(FilterFunc).ToList();
        }

        // Apply sorting/display transformation if available
        if (DisplayFunc != null)
        {
            filteredItems = filteredItems.OrderBy(DisplayFunc).ToList();
            _listBox.ItemTemplate = DataTemplateHelper.CreateTextBlockTemplate(DisplayFunc);
        }

        // Set filtered and sorted items as the ItemsSource of the ListBox
        _listBox.ItemsSource = filteredItems;
    }

    /// <summary>
    /// Handles the selection change event of the ListBox and triggers the <see cref="SelectionChanged"/> event.
    /// </summary>
    private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Invoke the SelectionChanged event with the current SelectedItem
        SelectionChanged?.Invoke(SelectedItem);
    }
}
