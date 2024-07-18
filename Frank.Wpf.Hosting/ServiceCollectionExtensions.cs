using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Frank.Wpf.Hosting;

/// <summary>
/// Provides helper methods for registering pages, windows, and controls with the service collection for dependency injection to make them registered as correctly as possible.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPage<T>(this IServiceCollection services) where T : Page
    {
        services.AddTransient<T>();
        return services;
    }
    
    public static IServiceCollection AddWindow<T>(this IServiceCollection services) where T : Window
    {
        services.AddTransient<T>();
        services.AddTransient<Window, T>(provider => provider.GetRequiredService<T>());
        return services;
    }
}