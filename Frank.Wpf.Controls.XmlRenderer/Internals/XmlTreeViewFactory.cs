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
            var rootItem = CreateTreeViewItem(rootElement);
            treeView.Items.Add(rootItem);
        }

        return treeView;
    }

    private static TreeViewItem CreateTreeViewItem(XElement element)
    {
        var treeViewItem = CreateBasicTreeViewItem(element);

        // Add attributes
        AddAttributesToTreeViewItem(element, treeViewItem);

        // Add child elements or text content
        if (element.HasElements)
        {
            AddChildElementsToTreeViewItem(element, treeViewItem);
        }
        else if (!string.IsNullOrWhiteSpace(element.Value))
        {
            AddTextToTreeViewItem(element, treeViewItem);
        }

        return treeViewItem;
    }

    private static void AddAttributesToTreeViewItem(XElement element, TreeViewItem treeViewItem)
    {
        foreach (var attribute in element.Attributes())
        {
            var attributeItem = CreateTreeViewItemFromAttribute(attribute);
            treeViewItem.Items.Add(attributeItem);
        }
    }

    private static void AddChildElementsToTreeViewItem(XElement element, TreeViewItem treeViewItem)
    {
        var childElements = element.Elements().ToList();
        
        // Group child elements by their name to identify collections
        var groupedElements = childElements.GroupBy(e => e.Name.LocalName);

        foreach (var group in groupedElements)
        {
            var isCollection = group.Count() > 1;
            int index = 0;

            foreach (var childElement in group)
            {
                var childItem = CreateTreeViewItem(childElement);

                // Only apply index prefix if it is part of a collection (i.e., multiple elements with the same name)
                if (isCollection)
                {
                    childItem.Header = $"[{index}] {childItem.Header}";
                    index++;
                }

                treeViewItem.Items.Add(childItem);
            }

            // Add count suffix for collection parent
            if (isCollection)
            {
                treeViewItem.Header = $"{treeViewItem.Header} [{group.Count()}]";
                treeViewItem.Tag = group.Count();
            }
        }
    }

    private static void AddTextToTreeViewItem(XElement element, TreeViewItem treeViewItem)
    {
        // Add the text content directly to the TreeViewItem header if no child elements exist
        treeViewItem.Header = $"{treeViewItem.Header}: {element.Value.Trim()}";
    }

    private static TreeViewItem CreateBasicTreeViewItem(XElement element)
    {
        return new TreeViewItem
        {
            Header = element.Name.LocalName,
            Tag = element.Value
        };
    }

    private static TreeViewItem CreateTreeViewItemFromAttribute(XAttribute attribute)
    {
        return new TreeViewItem
        {
            Header = $"@{attribute.Name.LocalName}: {attribute.Value}"
        };
    }
}
