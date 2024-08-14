using System.Windows.Controls;
using System.Windows.Navigation;

namespace Frank.Wpf.Controls.Pages;

/// <summary>
/// Represents a frame that can display pages.
/// </summary>
public class PageFrame : Frame
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PageFrame"/> class.
    /// </summary>
    public PageFrame()
    {
        NavigationUIVisibility = NavigationUIVisibility.Hidden;
    }

    public Page Page
    {
        get => Content as Page ?? throw new InvalidOperationException();
        set => Content = value;
    }

    /// <summary>
    /// Sets the page to be displayed in the frame.
    /// </summary>
    /// <param name="page"></param>
    public void SetPage(Page page) => Page = page;
}