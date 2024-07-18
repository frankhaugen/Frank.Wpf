using System.Windows;

namespace Frank.Wpf.Hosting;

public interface IWindowFactory
{
    /// <summary>
    /// Creates a window of the specified type.
    /// </summary>
    /// <param name="show">Whether to show the window after creation.</param>
    /// <typeparam name="T">The type of the window to create.</typeparam>
    /// <returns>The window of the specified type.</returns>
    Window CreateWindow<T>(bool show = false) where T : Window;
    
    /// <summary>
    /// Creates a window with the specified name.
    /// </summary>
    /// <param name="name">Name of the window from the property <see cref="Window.Name"/>.</param>
    /// <param name="show">Whether to show the window after creation.</param>
    /// <returns>The window with the specified name.</returns>
    Window CreateWindow(string name, bool show = false);
}