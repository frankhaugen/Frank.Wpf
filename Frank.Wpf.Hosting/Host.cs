namespace Frank.Wpf.Hosting;

/// <summary>
/// A helper class to create a WPF host.
/// </summary>
public static class Host
{
    /// <summary>
    /// Creates a new instance of the <see cref="WpfHostBuilder"/> class.
    /// </summary>
    /// <returns> The new instance of the <see cref="WpfHostBuilder"/> class. </returns>
    public static WpfHostBuilder CreateWpfHostBuilder() => new();
}