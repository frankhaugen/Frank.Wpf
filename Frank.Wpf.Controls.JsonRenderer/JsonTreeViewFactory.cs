using Frank.Wpf.Core;
using System.Text.Json;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.JsonRenderer;

public class JsonTreeViewFactory
{
    public TreeView Create(JsonDocument document)
    {
        var treeView = new TreeView();

        // Render Root Element
        var root = document.RootElement;
        var rootItem = new TreeViewItem { Header = "Document" };
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
                // Set the header to the value and do not add child items
                parentItem.Header = $"{parentItem.Header}: {element}";
                parentItem.Tag = element;
                break;
        }
    }

    private void RenderObject(JsonElement element, TreeViewItem parentItem)
    {
        foreach (var property in element.EnumerateObject())
        {
            if (property.Name == "$id" || property.Name == "$type" || property.Name == "$ref")
                continue;

            if (property.Name == "$values")
            {
                // Set the count of the $values array on the parent
                RenderArray(property.Value, parentItem);
                continue;
            }

            var item = new TreeViewItem { Header = property.Name };
            parentItem.Items.Add(item);

            // If the property is a primitive value, display it directly in the header
            if (property.Value.ValueKind != JsonValueKind.Object && property.Value.ValueKind != JsonValueKind.Array)
            {
                item.Header = $"{property.Name}: {property.Value}";
            }
            else
            {
                // Recursively render child elements
                RenderElement(property.Value, item);
            }
        }
    }

    private void RenderArray(JsonElement element, TreeViewItem parentItem)
    {
        var index = 0;
        foreach (var arrayItem in element.EnumerateArray())
        {
            var item = new TreeViewItem { Header = $"[{index}]" };
            parentItem.Items.Add(item);
            RenderElement(arrayItem, item);
            index++;
        }
        parentItem.Header = $"{parentItem.Header} [{index}]";
    }
}
