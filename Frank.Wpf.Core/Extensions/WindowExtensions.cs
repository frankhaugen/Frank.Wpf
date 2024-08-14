using System.Windows;
using System.Windows.Interop;
using Size = System.Drawing.Size;

namespace Frank.Wpf.Core;

public static class WindowExtensions
{
    public static List<Screen> GetScreens(this Window window)
    {
        var output = new List<Screen>();

        output.AddRange(Screen.AllScreens);

        return output;
    }

    public static bool IsOnPrimaryScreen(this Window window)
    {
        var screen = GetScreen(window);
        return screen.Primary;
    }

    public static Size GetScreenWorkingAreaSize(this Window window)
    {
        var screen = GetScreen(window);
        return new Size(screen.WorkingArea.Size.Width, screen.WorkingArea.Size.Height);
    }

    public static Size GetScreenSize(this Window window)
    {
        var screen = GetScreen(window);
        return new Size(screen.Bounds.Size.Width, screen.WorkingArea.Size.Height);
    }

    public static Screen GetScreen(this Window window)
    {
        var windowInteropHelper = new WindowInteropHelper(window);
        return Screen.FromHandle(windowInteropHelper.Handle);
    }
}