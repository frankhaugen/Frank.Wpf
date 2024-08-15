using System.Windows;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.SimpleInputs;

public class Dropdown<T> : UserControl
{
    private readonly ComboBox _comboBox = new();

    public Dropdown()
    {
        _comboBox.SelectionChanged += ComboBox_SelectionChanged;
        Content = _comboBox;
    }

    public required IEnumerable<T> Items
    {
        get => (IEnumerable<T>)_comboBox.ItemsSource;
        set => _comboBox.ItemsSource = value;
    }

    public required Func<T, string> DisplayFunc
    {
        init => _comboBox.ItemTemplate = CreateDataTemplate(value);
    }

    public required Action<T> SelectionChangedAction { get; init; }
    
    public void SetSelectedItem(T item)
    {
        // Ensure the item is in the list
        if (!Items.Contains(item))
            throw new ArgumentException("Item is not in the list of items");
        
        _comboBox.SelectedItem = item;
    }

    public T? SelectedItem => _comboBox.SelectedItem is T item ? item : default;

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_comboBox.SelectedItem is T selectedItem)
        {
            SelectionChangedAction(selectedItem);
        }
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

    // Converter for converting the Func<T, string> to a binding-friendly format
    private class FuncValueConverter<TInput, TOutput> : System.Windows.Data.IValueConverter
    {
        private readonly Func<TInput, TOutput> _func;

        public FuncValueConverter(Func<TInput, TOutput> func)
        {
            _func = func;
        }

        public object? Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            return value is TInput input ? _func(input) : default;
        }
        
        public object? ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            return value is TOutput output ? output : default;
        }
    }
}