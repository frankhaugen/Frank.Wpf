using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using Frank.Wpf.Controls.SimpleInputs;
using Frank.Wpf.Controls.XmlRenderer.Internals;
using Frank.Wpf.Core;
using Frank.Wpf.Core.Beatification;

namespace Frank.Wpf.Controls.XmlRenderer;

public class XmlRendererControl : UserControl
{
    private readonly TabControl _tabControl = new();
    private readonly TextBoxWithLineNumbers _textBoxWithLineNumbers = new();
    private readonly XmlTreeViewFactory _treeViewFactory = new();
    
    private readonly XmlBeautifier _xmlBeautifier = new();
    private readonly TreeViewWalker _treeViewWalker = new();
    private readonly TabItem _rendererTabItem;
    
    private TreeView _treeView = new();
    private XDocument? _document;
    private bool _isExpanded = false;
    private string? _searchText;

    public XmlRendererControl()
    {
        _rendererTabItem = CreateXmlRendererTabItem();
        var rawXmlTabItem = CreateRawXmlTabItem();
        
        _tabControl.Items.Add(_rendererTabItem);
        _tabControl.Items.Add(rawXmlTabItem);
        
        _tabControl.KeyDown += HandleKeyDownEvent;

        Content = _tabControl;
    }

    public XDocument? Document
    {
        get => _document;
        set
        {
            _document = value;
            Render();
        }
    }
    
    private TabItem CreateRawXmlTabItem() =>
        new()
        {
            Header = "Raw",
            Content = _textBoxWithLineNumbers,
        };

    private TabItem CreateXmlRendererTabItem()
    {
        var menuBar = new Menu();
        var expansionToggleMenuItem = new MenuItem { Header = "Toggle Expand/Collapse" };
        expansionToggleMenuItem.Click += ToggleExpandCollapse;

        var searchTextBox = new SearchBox("Search", SearchTextChanged);
        searchTextBox.KeyDown += SearchTextBoxOnKeyDown;
        var searchMenuItem = new MenuItem
        {
            Header = searchTextBox,
            Width = 64
        };
        
        menuBar.Items.Add(expansionToggleMenuItem);
        menuBar.Items.Add(searchMenuItem);

        var dockPanel = new DockPanel();
        DockPanel.SetDock(menuBar, Dock.Top);
        dockPanel.Children.Add(menuBar);
        dockPanel.Children.Add(_treeView);
        
        return new TabItem
        {
            Header = "Document",
            Content = dockPanel
        };
    }

    private void SearchTextBoxOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            if (_searchText is null)
            {
                return;
            }
            
            _searchText = _searchText.Trim();
            _searchText = _searchText.ToLower();
            
            _treeViewWalker.Walk(_treeView, item =>
            {
                if (item.Header.As<Label>()?.Content.As<string>()?.Contains(_searchText, StringComparison.InvariantCultureIgnoreCase) ?? false)
                {
                    item.IsSelected = true;
                
                    var parent = item.Parent as TreeViewItem;
                    while (parent is not null)
                    {
                        parent.IsExpanded = true;
                        parent = parent.Parent as TreeViewItem;
                    }
                
                    item.BringIntoView();
                
                    return false;
                }

                return true;
            });
        }
    }

    private void SearchTextChanged(string? obj)
    {
        _searchText = obj;
    }

    private void ToggleExpandCollapse(object sender, RoutedEventArgs e)
    {
        _isExpanded = !_isExpanded;
        _treeViewWalker.Walk(_treeView, OnToggleExpandCollapse);
    }

    private bool OnToggleExpandCollapse(TreeViewItem arg2)
    {
        arg2.IsExpanded = _isExpanded;
        return true;
    }

    private void HandleKeyDownEvent(object sender, KeyEventArgs e)
    {
        if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.C)
        {
            _treeViewWalker.Walk(_treeView, item =>
            {
                if (!item.IsSelected) return true;
            
                var text = item.Header.As<Label>()?.Content.As<string>();
                if (text is not null)
                    Clipboard.SetText(text);
                return false;
            });
        }
    }

    private void Render()
    {
        if (_document is null)
            return;
        
        _textBoxWithLineNumbers.Text = _xmlBeautifier.Beautify(_document.ToString());
        _treeView = _treeViewFactory.Create(_document);
        
        _rendererTabItem.Content.As<DockPanel>()?.Children.RemoveAt(1);
        _rendererTabItem.Content.As<DockPanel>()?.Children.Add(_treeView);
    }
}