namespace Frank.Wpf.Core;

public static class GenericExtensions
{
    public static List<KeyValuePair<string, string>> GetPropertiesAndValues<T>(this T source)
    {
        var publicProperties = typeof(T).GetProperties().Where(x => x.IsPublic()).ToList();
        var primitiveProperties = publicProperties.Where(x => x.IsPrimitive()).ToList();
        var stringProperties = publicProperties.Where(x => x.IsString()).ToList();
        var guidProperties = publicProperties.Where(x => x.IsGuid()).ToList();

        var combinedList = stringProperties.Concat(primitiveProperties).Concat(guidProperties).ToList();
        var properties = new List<KeyValuePair<string, string>>(combinedList.Select(x => new KeyValuePair<string, string>(x.Name, x.GetValue(source)?.ToString() ?? string.Empty)));
        return properties;
    }
}