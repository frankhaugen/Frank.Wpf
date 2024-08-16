namespace Frank.Wpf.Controls.XmlRenderer.Internals;

public static class FunctionalExtensions
{
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
            action(item);
    }

    public static void Let<T>(this T value, Action<T> action)
    {
        action(value);
    }
}