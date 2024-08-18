using System.Windows.Controls;
using System.Xml.Linq;

namespace Frank.Wpf.Controls.XmlRenderer.Internals;

public class XmlTreeViewFactory
{
    public TreeView Create(XDocument document)
    {
        var treeView = new TreeView();
        var rootElement = document.Root;

        if (rootElement != null)
        {
            var rootItem = CreateTreeViewItemFromElement(rootElement);
            treeView.Items.Add(rootItem);
        }

        return treeView;
    }

    private static TreeViewItem CreateTreeViewItemFromElement(XElement element)
    {
        var treeViewItem = new TreeViewItem
        {
            Header = element.Name.LocalName,
            IsExpanded = true
        };

        // Add attributes
        foreach (var attribute in element.Attributes())
        {
            var attributeItem = CreateTreeViewItemFromAttribute(attribute);
            treeViewItem.Items.Add(attributeItem);
        }

        // Add child elements or text content
        if (element.HasElements)
        {
            foreach (var childElement in element.Elements())
            {
                var childItem = CreateTreeViewItemFromElement(childElement);
                treeViewItem.Items.Add(childItem);
            }
        }
        else if (!string.IsNullOrWhiteSpace(element.Value))
        {
            var textItem = new TreeViewItem
            {
                Header = element.Value.Trim()
            };
            treeViewItem.Items.Add(textItem);
        }

        return treeViewItem;
    }

    private static TreeViewItem CreateTreeViewItemFromAttribute(XAttribute attribute)
    {
        return new TreeViewItem
        {
            Header = $"@{attribute.Name.LocalName}: {attribute.Value}"
        };
    }
}