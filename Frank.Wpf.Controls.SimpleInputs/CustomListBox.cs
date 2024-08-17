using System.Windows;
using System.Windows.Controls;
using Frank.Wpf.Core;

namespace Frank.Wpf.Controls.SimpleInputs;

public class CustomListBox<T> : UserControl
{
    private readonly ListBox _listBox;
    
    private readonly Func<T, bool> _filterFunc;
    private readonly Func<T, string>? _displayFunc;
    
    private IEnumerable<T>? _items;

    public CustomListBox()
    {
        _listBox = new ListBox();
        Content = _listBox;

        _listBox.SelectionChanged += ListBox_SelectionChanged;
    }
    
    public T? SelectedItem => _listBox.SelectedItem is T item ? item : default;
    
    public IEnumerable<T> Items
    {
        get => _items ?? Array.Empty<T>();
        set
        {
            _items = value;
            SetListBoxItems();
        }
    }
    
    public required Func<T, bool> FilterFunc
    {
        get => _filterFunc;
        init
        {
            _filterFunc = value;
            SetListBoxItems();
        }
    }

    public event Action<T>? SelectionChanged;
    
    public required Func<T, string> DisplayFunc
    {
        get => _displayFunc ?? (x => x.ToString());
        init  
        {
            _displayFunc = value;
            _listBox.ItemTemplate = CreateDataTemplate(value);
        }
    }

    public void SetSelectedItem(T? item)
    {
        if (item == null)
        {
            _listBox.SelectedItem = null;
            return;
        }
        
        _listBox.SelectedItem = Items.FirstOrDefault(x => x.Equals(item));
    }
    
    private void SetListBoxItems()
    {
        _listBox.Items.Clear();
        Items.Where(FilterFunc).OrderBy(DisplayFunc).Do(item => _listBox.Items.Add(item));
    }

    private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_listBox.SelectedItem is ListBoxItem selectedItem && SelectionChanged != null) SelectionChanged((T)selectedItem.Tag);
    }
    
    private DataTemplate CreateDataTemplate(Func<T, string> displayFunc)
    {
        var dataTemplate = new DataTemplate(typeof(T));
        var factory = new FrameworkElementFactory(typeof(TextBlock));
        factory.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding
        {
            Converter = new FuncValueConverter<T, string>(displayFunc),
            Mode = System.Windows.Data.BindingMode.OneWay
        });
        dataTemplate.VisualTree = factory;
        return dataTemplate;
    }

    private class FuncValueConverter<TInput, TOutput>(Func<TInput, TOutput> func) : System.Windows.Data.IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture) 
            => value is TInput input ? func(input) : default;

        public object? ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture) 
            => value is TOutput output ? output : default;
    }
}