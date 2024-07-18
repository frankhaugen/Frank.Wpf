using System.Collections.Concurrent;

namespace Frank.Wpf.Hosting;

public class ApplicationCache : IApplicationCache
{
    private readonly ConcurrentDictionary<string, object> _cache = new();

    /// <inheritdoc />
    public T? Get<T>(string key)
    {
        if (_cache.TryGetValue(key, out var value))
            return (T)value;

        return default;
    }

    /// <inheritdoc />
    public void Set<T>(string key, T value) => _cache[key] = value ?? throw new InvalidOperationException("The value cannot be null.");

    /// <inheritdoc />
    public void Remove(string key) => _cache.TryRemove(key, out _);

    /// <inheritdoc />
    public bool ContainsKey(string key) => _cache.ContainsKey(key);

    /// <inheritdoc />
    public T GetOrSet<T>(string key, Func<T> valueFactory)
    {
        if (_cache.TryGetValue(key, out var value))
            return (T)value;

        var newValue = valueFactory();
        _cache[key] = newValue ?? throw new InvalidOperationException("The value factory returned a null value.");
        return newValue;
    }

    /// <inheritdoc />
    public T? GetAndRemove<T>(string key)
    {
        if (_cache.TryRemove(key, out var value))
            return (T)value;
        
        return default;
    }
}