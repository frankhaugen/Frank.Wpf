using System.Windows;
using System.Windows.Controls;
using Frank.Wpf.Controls.Pages;

namespace Frank.Wpf.Tests.App.Windows;

public class PagedTabControlWindow : Window
{
    public PagedTabControlWindow()
    {
        Title = "Paged Tab Control Window";
        Width = 800;
        Height = 600;
        Content = new PagedTabControl()
        {
            TabStripPlacement = Dock.Left,
            Pages = new Page[]
            {
                new Page1(),
                new Page2(),
                new Page3()
            }
        };
    }
    
    private class Page1 : Page
    {
        public Page1()
        {
            Title = "Page 1";
            Content = new TextBlock
            {
                Text = "Page 1",
                FontSize = 24
            };
        }
    }
    
    private class Page2 : Page
    {
        public Page2()
        {
            Title = "Page 2";
            Content = new TextBlock
            {
                Text = "Page 2",
                FontSize = 24
            };
        }
    }
    
    private class Page3 : Page
    {
        public Page3()
        {
            Title = "Page 3";
            Content = new TextBlock
            {
                Text = "Page 3",
                FontSize = 24
            };
        }
    }
}