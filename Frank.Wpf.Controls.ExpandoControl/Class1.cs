using System.Dynamic;
using System.Windows;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.ExpandoControl;

public class ExpandoControl : ContentControl
{
    static ExpandoControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ExpandoControl), new FrameworkPropertyMetadata(typeof(ExpandoControl)));
    }
}

public static class ExpandoHelper
{
    public static ExpandoObject CreateExpando<T>(T source)
    {
        var expando = new ExpandoObject();
        
        foreach (var property in typeof(T).GetProperties())
        {
            ((IDictionary<string, object>)expando)[property.Name] = property.GetValue(source);
        }

        return expando;
    }
}