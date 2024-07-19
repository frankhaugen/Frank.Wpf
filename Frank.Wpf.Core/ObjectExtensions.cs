namespace Frank.Wpf.Core;

public static class ObjectExtensions
{
    public static T? As<T>(this object? source) where T : class => source as T;
}