namespace Frank.Wpf.Hosting;

/// <summary>
/// Provides a way to store and retrieve objects in a cache that has application-wide scope and is shared by all windows and has a lifetime that is the same as the application.
/// </summary>
public interface IApplicationCache
{
    /// <summary>
    /// Gets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the value to get.</param>
    /// <typeparam name="T">The type of the value to get.</typeparam>
    /// <returns>The value associated with the specified key.</returns>
    T? Get<T>(string key);

    /// <summary>
    /// Sets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the value to set.</param>
    /// <param name="value">The value to set.</param>
    /// <typeparam name="T">The type of the value to set.</typeparam>
    void Set<T>(string key, T value);

    /// <summary>
    /// Removes the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the value to remove.</param>
    void Remove(string key);
    
    /// <summary>
    /// Determines whether the cache contains the specified key.
    /// </summary>
    /// <param name="key">The key to locate in the cache.</param>
    /// <remarks>This method is an O(1) operation.</remarks>
    /// <returns><see langword="true"/> if the cache contains an element with the specified key; otherwise, <see langword="false"/>.</returns>
    bool ContainsKey(string key);
    
    /// <summary>
    /// Gets the value associated with the specified key or sets the value if the key does not exist.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="valueFactory"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T GetOrSet<T>(string key, Func<T> valueFactory);
    
    /// <summary>
    /// Gets the value associated with the specified key and removes it from the cache.
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T? GetAndRemove<T>(string key);
}