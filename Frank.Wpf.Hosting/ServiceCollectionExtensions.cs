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
        services.AddTransient<Page, T>(provider => provider.GetRequiredService<T>());
        return services;
    }
    
    public static IServiceCollection AddWindow<T>(this IServiceCollection services) where T : Window
    {
        services.AddTransient<T>();
        services.AddTransient<Window, T>(provider => provider.GetRequiredService<T>());
        return services;
    }
    
    public static IServiceCollection AddContentControl<T>(this IServiceCollection services) where T : ContentControl
    {
        services.AddTransient<T>();
        services.AddTransient<ContentControl, T>(provider => provider.GetRequiredService<T>());
        return services;
    }
    
    public static IServiceCollection AddControl<T>(this IServiceCollection services) where T : Control
    {
        services.AddTransient<T>();
        services.AddTransient<Control, T>(provider => provider.GetRequiredService<T>());
        return services;
    }
    
    public static IServiceCollection AddUserControl<T>(this IServiceCollection services) where T : UserControl
    {
        services.AddTransient<T>();
        services.AddTransient<UserControl, T>(provider => provider.GetRequiredService<T>());
        return services;
    }
    
    public static IServiceCollection AddFrameworkElement<T>(this IServiceCollection services) where T : FrameworkElement
    {
        services.AddTransient<T>();
        services.AddTransient<FrameworkElement, T>(provider => provider.GetRequiredService<T>());
        return services;
    }
}