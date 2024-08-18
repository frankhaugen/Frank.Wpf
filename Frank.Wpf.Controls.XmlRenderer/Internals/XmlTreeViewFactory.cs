using System.Linq;
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
        var childGroups = element.Elements().GroupBy(e => e.Name.LocalName).ToList();

        foreach (var group in childGroups)
        {
            var isCollection = group.Count() > 1;

            // Add count suffix for collections, even if they have only one element
            if (isCollection || group.Count() == 1)
            {
                treeViewItem.Header = $"{treeViewItem.Header} [{group.Count()}]";
            }

            int index = 0;

            foreach (var childElement in group)
            {
                var childItem = CreateTreeViewItem(childElement);

                // Prepend index if this is part of a collection
                if (isCollection || group.Count() == 1)
                {
                    childItem.Header = $"[{index}] {childItem.Header}";
                    index++;
                }

                treeViewItem.Items.Add(childItem);
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
            IsExpanded = true
        };
    }

    private static TreeViewItem CreateTreeViewItemFromAttribute(XAttribute attribute)
    {
        return new TreeViewItem
        {
            Header = $"@{attribute.Name.LocalName}: {attribute.Value}",
            IsExpanded = true
        };
    }
}
