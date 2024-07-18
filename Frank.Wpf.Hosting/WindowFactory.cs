using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Frank.Wpf.Hosting;

/// <inheritdoc />
public class WindowFactory : IWindowFactory
{
    private readonly IServiceProvider _services;

    public WindowFactory(IServiceProvider services)
    {
        _services = services;
    }
    
    /// <inheritdoc />
    public Window CreateWindow(string name, bool show = false)
    {
        var windows = _services.GetServices<Window>();
        var window = windows.FirstOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        
        if (window == null)
            throw new InvalidOperationException($"Window with name '{name}' not found.");
        
        if (show)
            window.Show();
        
        return window;
    }

    /// <inheritdoc />
    public Window CreateWindow<T>(bool show = false) where T : Window
    {
        var window = _services.GetRequiredService<T>();
        
        if (show)
            window.Show();
        
        return window;
    }
}