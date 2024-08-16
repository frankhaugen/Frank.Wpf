using Frank.Wpf.Core;

namespace Frank.Wpf.Controls.JsonRenderer;

using System.Text.Json;
using System.Windows.Controls;

public class JsonTreeViewFactory
{
    public TreeView Create(JsonDocument document)
    {
        var treeView = new TreeView();

        // Render Root Element
        var root = document.RootElement;
        var rootItem = CreateTreeViewItem("Root");
        treeView.Items.Add(rootItem);

        RenderElement(root, rootItem);

        return treeView;
    }

    private void RenderElement(JsonElement element, TreeViewItem parentItem)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                RenderObject(element, parentItem);
                break;
            case JsonValueKind.Array:
                RenderArray(element, parentItem);
                break;
            case JsonValueKind.String:
            case JsonValueKind.Number:
            case JsonValueKind.True:
            case JsonValueKind.False:
            case JsonValueKind.Null:
                parentItem.Items.Add(CreateTreeViewItem(element.ToString()));
                break;
        }
    }

    private void RenderObject(JsonElement element, TreeViewItem parentItem)
    {
        foreach (var property in element.EnumerateObject())
        {
            var item = CreateTreeViewItem(property.Name);
            parentItem.Items.Add(item);
            RenderElement(property.Value, item);
        }
    }

    private void RenderArray(JsonElement element, TreeViewItem parentItem)
    {
        var index = 0;
        foreach (var arrayItem in element.EnumerateArray())
        {
            var item = CreateTreeViewItem($"[{index++}]");
            parentItem.Items.Add(item);
            RenderElement(arrayItem, item);
        }
        parentItem.Header = new Label { Content = $"{parentItem.Header.As<Label>()?.Content.As<string>()}[{index}]" };
    }

    private static TreeViewItem CreateTreeViewItem(string header) =>
        new()
        {
            Header = new Label { Content = header }
        };
}
