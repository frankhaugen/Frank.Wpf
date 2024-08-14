using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Frank.Wpf.Hosting;

/// <summary>
/// A helper class to create a WPF host with the specified main window through the builder pattern.
/// </summary>
public class WpfHostBuilder
{
    private readonly ServiceCollection _services = new ServiceCollection();

    // Prevent external instantiation.
    internal WpfHostBuilder() => Services.AddSingleton<Application>();

    /// <summary>
    /// Creates a new instance of the <see cref="WpfHostBuilder"/> class.
    /// </summary>
    /// <returns></returns>
    public static WpfHostBuilder CreateWpfHostBuilder() => new();

    /// <summary>
    /// Represents the configuration of the application. Add configuration sources to this object.
    /// </summary>
    public IConfigurationManager Configuration { get; } = new ConfigurationManager();

    /// <summary>
    /// Represents the services collection of the application. Add services to this object.
    /// </summary>
    public IServiceCollection Services => _services;

    /// <summary>
    /// Builds the WPF host with the specified main window.
    /// </summary>
    /// <typeparam name="T"> The type of the main window. </typeparam>
    /// <returns> The WPF host with the specified main window. </returns>
    public WpfHost<T> Build<T>() where T : MainWindow
    {
        // Add the main window to the services' collection.
        var existing = Services.Where(x => x.ServiceType == typeof(T)).ToArray();
        if (existing.Length != 0) foreach (var service in existing) Services.Remove(service);
        Services.AddWindow<T>();
        
        // Add the configuration and other services to the services' collection.
        Services.AddSingleton<IConfiguration>(Configuration.Build());
        Services.AddSingleton<IWindowFactory, WindowFactory>();
        Services.AddSingleton<IWpfHostLifetime, WpfHostLifetime>();
        Services.AddSingleton<IApplicationCache, ApplicationCache>();
        Services.AddSingleton<IPageFactory, PageFactory>();
        
        // Add the services' collection to the services' collection. This is used to resolve services from the serviceprovider after the host is built.
        // This is a workaround to resolve services from the serviceprovider after the host is built.
        Services.AddSingleton<IServiceCollection>(_services); // Must be the last service to be added to the services' collection.
        
        // Make the services' collection read-only to prevent further modifications after the host is built.
        _services.MakeReadOnly();
        
        // Build the service provider and return the WPF host.
        return new WpfHost<T>(Services.BuildServiceProvider(new ServiceProviderOptions() { ValidateOnBuild = true, ValidateScopes = true }));
    }
}