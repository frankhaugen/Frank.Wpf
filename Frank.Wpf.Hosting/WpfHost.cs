using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Frank.Wpf.Hosting;

/// <summary>
/// Represents a WPF host with the specified main window.
/// </summary>
/// <remarks>This can only be instantiated through the <see cref="WpfHostBuilder"/> class.</remarks>
/// <typeparam name="T"></typeparam>
public class WpfHost<T> where T : MainWindow
{
    private readonly IServiceProvider _services;

    // Prevent external instantiation.
    internal WpfHost(IServiceProvider services) => _services = services;

    /// <summary>
    /// Called to run the application with the specified main window.
    /// </summary>
    [STAThread]
    public void Run() 
    {
        var app = _services.GetRequiredService<Application>();
        var window = _services.GetRequiredService<T>();
        var lifetime = _services.GetRequiredService<IWpfHostLifetime>();
        
        app.ShutdownMode = ShutdownMode.OnMainWindowClose;
        app.Exit += (sender, args) =>
        {
            lifetime.StopApplication();
            app.Shutdown();
            Environment.Exit(0);
        };
        
        app.MainWindow = window;

        app.Run(window);
    }
}