using System.Dynamic;

namespace Frank.Wpf.Controls.ExpandoControl;

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