using System.Windows;
using System.Windows.Controls;
using Label = System.Windows.Controls.Label;
using ToolTip = System.Windows.Controls.ToolTip;
using UserControl = System.Windows.Controls.UserControl;

namespace Frank.Wpf.Core;

/// <summary>
/// A helper class for creating DataTemplates.
/// </summary>
public static class DataTemplateHelper
{
    /// <summary>
    /// Creates a reusable DataTemplate for displaying items of type <typeparamref name="T"/> in a TextBlock.
    /// The TextBlock will bind its text to the string result of the provided display function.
    /// </summary>
    /// <typeparam name="T">The type of the items to display.</typeparam>
    /// <param name="displayFunc">A function to convert items of type <typeparamref name="T"/> to their string representations.</param>
    /// <returns>A DataTemplate for displaying the items as TextBlocks.</returns>
    public static DataTemplate CreateTextBlockTemplate<T>(Func<T, string> displayFunc)
    {
        var factory = new FrameworkElementFactory(typeof(TextBlock));
        factory.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding
        {
            Converter = new FuncValueConverter<T, string>(displayFunc),
            Mode = System.Windows.Data.BindingMode.OneWay
        });

        var template = new DataTemplate
        {
            VisualTree = factory
        };
        return template;
    }
    
    public static DataTemplate CreateLabelTemplate<T>(Func<T, string> displayFunc)
    {
        var factory = new FrameworkElementFactory(typeof(Label));
        factory.SetBinding(ContentControl.ContentProperty, new System.Windows.Data.Binding
        {
            Converter = new FuncValueConverter<T, string>(displayFunc),
            Mode = System.Windows.Data.BindingMode.OneWay
        });

        var template = new DataTemplate
        {
            VisualTree = factory
        };
        return template;
    }
    
    public static DataTemplate CreateUserControlTemplate<T>(Func<T, UserControl> displayFunc)
    {
        var factory = new FrameworkElementFactory(typeof(ContentControl));
        factory.SetBinding(ContentControl.ContentProperty, new System.Windows.Data.Binding
        {
            Converter = new FuncValueConverter<T, UserControl>(displayFunc),
            Mode = System.Windows.Data.BindingMode.OneWay
        });

        var template = new DataTemplate
        {
            VisualTree = factory
        };
        return template;
    }
    
    public static DataTemplate CreateToolTipTemplate<T, TToolTip>(Func<T, TToolTip> displayFunc) where TToolTip : ToolTip
    {
        var factory = new FrameworkElementFactory(typeof(TToolTip));
        factory.SetBinding(ContentControl.ContentProperty, new System.Windows.Data.Binding
        {
            Converter = new FuncValueConverter<T, TToolTip>(displayFunc),
            Mode = System.Windows.Data.BindingMode.OneWay
        });

        var template = new DataTemplate
        {
            VisualTree = factory
        };
        return template;
    }
    
    private class FuncValueConverter<TInput, TOutput>(Func<TInput, TOutput> func) : System.Windows.Data.IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
            => value is TInput input ? func(input)! : DependencyProperty.UnsetValue;

        public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
            => throw new NotSupportedException();
    }
}
