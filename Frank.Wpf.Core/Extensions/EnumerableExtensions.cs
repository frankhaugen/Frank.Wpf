namespace Frank.Wpf.Core;

public static class EnumerableExtensions
{
    public static void Do<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
        {
            action(item);
        }
    }
    
    public static void Do<T>(this IEnumerable<T> source, Action<T, int> action)
    {
        var index = 0;
        foreach (var item in source)
        {
            action(item, index);
            index++;
        }
    }
    
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
        {
            action(item);
        }
        return source;
    }
    
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
    {
        var index = 0;
        foreach (var item in source)
        {
            action(item, index);
            index++;
        }
        return source;
    }
    
    public static IEnumerable<T> ForEachIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, Action<T> action)
    {
        foreach (var item in source)
        {
            if (predicate(item))
            {
                action(item);
            }
        }
        return source;
    }
    
    public static IEnumerable<T> ForEachIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, Action<T, int> action)
    {
        var index = 0;
        foreach (var item in source)
        {
            if (predicate(item))
            {
                action(item, index);
            }
            index++;
        }
        return source;
    }
}