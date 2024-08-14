using System.Windows.Controls;

namespace Frank.Wpf.Hosting;

public interface IPageFactory
{
    T CreatePage<T>() where T : Page;
    
    T CreatePage<T>(bool newInstanceOfPage) where T : Page;
    
    T CreatePage<T>(Action<T> configure, bool newInstanceOfPage) where T : Page;
}