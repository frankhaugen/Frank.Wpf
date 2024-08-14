using System.Windows.Controls;
using Frank.Wpf.Core;

namespace Frank.Wpf.Controls.Pages;

public class PagedTabControl : UserControl
{
    private readonly TabControl _tabControl = new();

    public PagedTabControl()
    {
        Content = _tabControl;
    }
    
    public Page? SelectedPage => _tabControl.SelectedItem.As<TabItem>()?.Content.As<Page>();

    public Dock TabStripPlacement
    {
        get => _tabControl.TabStripPlacement;
        set => _tabControl.TabStripPlacement = value;
    }
    
    public IEnumerable<Page> Pages
    {
        get => _tabControl.Items.OfType<TabItem>().Select(x => x.Content.As<Page>()).Where(x => x != null).Select(x => x!);
        set
        {
            _tabControl.Items.Clear();
            foreach (var page in value.OrderBy(x => x.Title))
            {
                _tabControl.Items.Add(new TabItem
                {
                    Header = page.Title,
                    Content = new PageFrame()
                    {
                        Page = page
                    }
                });
            }
        }
    }
}