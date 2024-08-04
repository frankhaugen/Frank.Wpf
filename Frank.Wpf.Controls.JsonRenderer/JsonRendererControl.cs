using System.Windows;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.JsonRenderer;

public class JsonRendererControl : ContentControl
{
    private readonly JsonRendererTreeView _jsonRenderer = new();
    private readonly Dictionary<string, ColorConfiguration> _nameAndColorConfiguration = new Dictionary<string, ColorConfiguration>()
        {
            { "Default Colors", new DefaultColorConfiguration() },
            { "Colorful", new ColorfulColorConfiguration() }
        };

    public JsonRendererControl()
    {
        
        var menuBar = new Menu();
        var expansionToggleMenuItem = new MenuItem { Header = "Toggle Expand/Collapse" };
        var setColorMenuItem = new MenuItem { Header = "Set Color", ItemsSource = _nameAndColorConfiguration.Keys };

        foreach (var item in setColorMenuItem.Items)
        {
            if (item is MenuItem menuItem)
            {
                menuItem.Click += ChangeColors;
            }
        }
            
        expansionToggleMenuItem.Click += _jsonRenderer.ToggleExpandCollapse;
        menuBar.Items.Add(expansionToggleMenuItem);
        menuBar.Items.Add(setColorMenuItem);

        // Create a DockPanel to hold the menu and the TreeView
        var dockPanel = new DockPanel();
        DockPanel.SetDock(menuBar, Dock.Top);
        dockPanel.Children.Add(menuBar);
        dockPanel.Children.Add(_jsonRenderer);

        // Set the DockPanel as the content of the parent container (e.g., a Window or UserControl)
        Content = dockPanel;
    }

    private void ChangeColors(object sender, RoutedEventArgs e)
    {
        if (e.Source is MenuItem { Parent: MenuItem } menuItem)
        {
            if (_nameAndColorConfiguration.TryGetValue(menuItem.Header.ToString() ?? string.Empty, out var colorConfiguration))
            {
                _jsonRenderer.ChangeColors(colorConfiguration);
            }
        }
    }

    public void Render(string json)
    {
        _jsonRenderer.Render(json);
    }
}