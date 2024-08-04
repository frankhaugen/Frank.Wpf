using System.Reflection;

namespace Frank.Wpf.Core;

public class PropertyInfoValueManager<T, TSource>
{
    private readonly TSource _source;

    public PropertyInfoValueManager(PropertyInfo propertyInfo, TSource source)
    {
        PropertyInfo = propertyInfo;
        _source = source;
    }
    
    public PropertyInfo PropertyInfo { get; }

    public T? GetValue() => (T?)PropertyInfo.GetValue(_source);
    public void SetValue(T? value) => PropertyInfo.SetValue(_source, value);
}