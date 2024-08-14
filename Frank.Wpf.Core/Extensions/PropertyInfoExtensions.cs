using System.Reflection;

namespace Frank.Wpf.Core;

public static class PropertyInfoExtensions
{
    public static T? GetAttribute<T>(this PropertyInfo source) where T : Attribute
    {
        return source.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T;
    }

    public static PropertyInfoValueManager<T, TSource> GetValueManager<T, TSource>(this TSource source, string propertyName)
    {
        var propertyInfo = source.GetType().GetProperty(propertyName);
        return new PropertyInfoValueManager<T, TSource>(propertyInfo, source);
    }
}