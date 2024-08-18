using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Frank.Wpf.Controls.SimpleInputs;
using Frank.Wpf.Core;
using Frank.Wpf.Core.Beatification;

namespace Frank.Wpf.Controls.JsonRenderer;

public class JsonRendererControl : ContentControl
{
    private readonly TabControl _tabControl = new();
    private readonly TextBoxWithLineNumbers _textBoxWithLineNumbers = new();
    // private readonly JsonTreeView _jsonRenderer = new();
    private readonly JsonTreeViewFactory _treeViewFactory = new();
    
    private readonly JsonBeautifier _jsonBeautifier = new();
    private readonly TreeViewWalker _treeViewWalker = new();
    
    private readonly TabItem _rendererTabItem;

    private TreeView _treeView = new();
    private JsonDocument? _document;
    private bool _isExpanded = false;
    
    public JsonRendererControl()
    {
        _rendererTabItem = CreateJsonRendererTabItem();
        var rawJsonTabItem = CreateRawJsonTabItem();
        
        _tabControl.Items.Add(_rendererTabItem);
        _tabControl.Items.Add(rawJsonTabItem);
        
        _tabControl.KeyDown += HandleKeyDownEvent;

        Content = _tabControl;
    }
    
    public JsonDocument? Document
    {
        get => _document;
        set
        {
            _document = value;

            if (_document is null)
            {
                _textBoxWithLineNumbers.Text = string.Empty;
                _tabControl.Items.Clear();
                return;
            }

            _textBoxWithLineNumbers.Text = _jsonBeautifier.Beautify(value?.RootElement.GetRawText() ?? "{}");
            _treeView = _treeViewFactory.Create(_document);
        
            _rendererTabItem.Content.As<DockPanel>()?.Children.RemoveAt(1);
            _rendererTabItem.Content.As<DockPanel>()?.Children.Add(_treeView);
        }
    }

    private TabItem CreateRawJsonTabItem() =>
        new()
        {
            Header = "Raw",
            Content = _textBoxWithLineNumbers,
        };

    private TabItem CreateJsonRendererTabItem()
    {
        var menuBar = new Menu();
        var expansionToggleMenuItem = new MenuItem { Header = "Toggle Expand/Collapse" };
        expansionToggleMenuItem.Click += ToggleExpandCollapse;

        var searchTextBox = new SearchBox();
        searchTextBox.SearchTextChanged += SearchTextChanged;
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
            SearchTextChanged((sender as SearchBox)?.SearchText);
        }
    }

    private void SearchTextChanged(string? obj)
    {
        if (string.IsNullOrWhiteSpace(obj))
        {
            _treeViewWalker.Walk(_treeView, item => item.IsExpanded = false);
            return;
        }

        _treeViewWalker.Walk(_treeView, item => item.IsExpanded = true);
        _treeViewWalker.Walk(_treeView, item => item.IsExpanded = item.Header.ToString()?.Contains(obj, StringComparison.OrdinalIgnoreCase) ?? false);
    }

    private void ToggleExpandCollapse(object sender, RoutedEventArgs e)
    {
        _isExpanded = !_isExpanded;
        _treeViewWalker.Walk(_treeView, item => item.IsExpanded = _isExpanded);
    }
    
    private void HandleKeyDownEvent(object sender, KeyEventArgs e)
    {
        if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.C)
        {
            _treeViewWalker.Walk(_treeView, item =>
            {
                if (!item.IsSelected) return true;
            
                var text = item.Tag.As<string>();
                if (text is not null)
                    Clipboard.SetText(text);
                return false;
            });
        }
        
        if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.D)
        {
            try
            {
                var xaml = _treeView.ToXaml();
                Clipboard.SetText(xaml);
                Console.Beep();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}