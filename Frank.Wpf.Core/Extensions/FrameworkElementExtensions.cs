using System.Windows;
using System.Windows.Data;
using Binding = System.Windows.Data.Binding;

namespace Frank.Wpf.Core;

public static class FrameworkElementExtensions
{
    public static void BindWidth(this FrameworkElement bindMe, DependencyProperty toMe)
    {
        var b = new Binding();
        b.Mode = BindingMode.OneWay;
        b.Source = toMe;
        bindMe.SetBinding(FrameworkElement.WidthProperty, b);
    }
}