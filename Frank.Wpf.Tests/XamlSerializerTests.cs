using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Frank.Wpf.Core;
using Xunit.Abstractions;

namespace Frank.Wpf.Tests;

public class XamlSerializerTests
{
    private readonly ITestOutputHelper _outputHelper;

    public XamlSerializerTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }
    
    [WpfFact]
    public void Test1()
    {
        var uiElement = new System.Windows.Controls.StackPanel
        {
            Orientation = System.Windows.Controls.Orientation.Horizontal,
            Children =
            {
                new System.Windows.Controls.Button
                {
                    Content = "Click me"
                }
            }
        };
        
        var result = XamlWriter.Save(uiElement);
        _outputHelper.WriteLine(result);
    }
    
    // [WpfFact]
    public void Test2()
    {
        var uiElement = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Children =
            {
                new MyDropDown<string>()
                {
                    Items = new[] { "One", "Two", "Three" },
                    DisplayFunc = x => x,
                    SelectionChangedAction = x => { }
                }
            }
        };
        
        var result = XamlWriter.Save(uiElement);
        _outputHelper.WriteLine(result);
        
        Assert.Contains("One", result);
    }
    
}
public class MyDropDown<T> : UserControl
{
    private readonly ComboBox _comboBox = new();

    public MyDropDown()
    {
        _comboBox.SelectionChanged += ComboBox_SelectionChanged;
        Content = _comboBox;
    }

    public IEnumerable<T> Items
    {
        get => (IEnumerable<T>)_comboBox.ItemsSource;
        set => _comboBox.ItemsSource = value;
    }

    public Func<T, string> DisplayFunc
    {
        init => _comboBox.ItemTemplate = CreateDataTemplate(value);
    }

    public Action<T> SelectionChangedAction { get; init; }

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
