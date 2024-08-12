using System.Windows;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.SimpleInputs;

public class CustomListBox<T> : UserControl
{
    private readonly ListBox _listBox;

    public CustomListBox()
    {
        _listBox = new ListBox();
        Content = _listBox;

        _listBox.SelectionChanged += ListBox_SelectionChanged;
    }

    public static readonly DependencyProperty ItemsProperty =
        DependencyProperty.Register(
            nameof(Items),
            typeof(IEnumerable<T>),
            typeof(CustomListBox<T>),
            new PropertyMetadata(new List<T>(), OnItemsChanged));

    public static readonly DependencyProperty DisplayFuncProperty =
        DependencyProperty.Register(
            nameof(DisplayFunc),
            typeof(Func<T, string>),
            typeof(CustomListBox<T>),
            new PropertyMetadata(null, OnDisplayFuncChanged));

    public static readonly DependencyProperty FilterFuncProperty =
        DependencyProperty.Register(
            nameof(FilterFunc),
            typeof(Func<T, bool>),
            typeof(CustomListBox<T>),
            new PropertyMetadata(null, OnFilterFuncChanged));

    public static readonly DependencyProperty SelectionChangedActionProperty =
        DependencyProperty.Register(
            nameof(SelectionChangedAction),
            typeof(Action<T>),
            typeof(CustomListBox<T>),
            new PropertyMetadata(null));

    public IEnumerable<T> Items
    {
        get => (IEnumerable<T>)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    public Func<T, string> DisplayFunc
    {
        get => (Func<T, string>)GetValue(DisplayFuncProperty);
        set => SetValue(DisplayFuncProperty, value);
    }

    public Func<T, bool> FilterFunc
    {
        get => (Func<T, bool>)GetValue(FilterFuncProperty);
        set => SetValue(FilterFuncProperty, value);
    }

    public Action<T> SelectionChangedAction
    {
        get => (Action<T>)GetValue(SelectionChangedActionProperty);
        set => SetValue(SelectionChangedActionProperty, value);
    }

    private static void OnItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (CustomListBox<T>)d;
        control.ApplyFilter();
    }

    private static void OnDisplayFuncChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (CustomListBox<T>)d;
        control.ApplyFilter();
    }

    private static void OnFilterFuncChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (CustomListBox<T>)d;
        control.ApplyFilter();
    }

    private void ApplyFilter()
    {
        _listBox.Items.Clear();

        if (Items == null || DisplayFunc == null)
        {
            return;
        }

        var filteredItems = FilterFunc != null ? Items.Where(FilterFunc) : Items;

        foreach (var item in filteredItems)
        {
            _listBox.Items.Add(new ListBoxItem { Content = DisplayFunc(item), Tag = item });
        }
    }

    private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_listBox.SelectedItem is ListBoxItem selectedItem && SelectionChangedAction != null)
        {
            SelectionChangedAction((T)selectedItem.Tag);
        }
    }
}