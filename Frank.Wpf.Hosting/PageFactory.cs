using System.Windows.Controls;

namespace Frank.Wpf.Hosting;

public class PageFactory : IPageFactory
{
    private readonly IEnumerable<Page> _pages;

    public PageFactory(IEnumerable<Page> pages)
    {
        _pages = pages;
    }

    public T CreatePage<T>() where T : Page => _pages.OfType<T>().FirstOrDefault() ?? throw new InvalidOperationException($"Page of type {typeof(T).Name} not found");
    
    public T CreatePage<T>(bool newInstanceOfPage) where T : Page => newInstanceOfPage ? Activator.CreateInstance<T>() : CreatePage<T>();

    public T CreatePage<T>(Action<T> configure, bool newInstanceOfPage) where T : Page
    {
        var page = CreatePage<T>(newInstanceOfPage);
        configure(page);
        return page;
    }
}