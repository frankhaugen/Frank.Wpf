using System.Windows.Controls;
using System.Xml.Linq;

namespace Frank.Wpf.Controls.XmlRenderer;

public class XmlTreeViewFactory
{
    public TreeView Create(XDocument document)
    {
        var treeView = new TreeView();
        var rootElement = document.Root;

        if (rootElement != null)
        {
            var rootItem = new TreeViewItem { Header = rootElement.Name.LocalName };
            treeView.Items.Add(rootItem);

            RenderElement(rootElement, rootItem);
        }

        return treeView;
    }

    private void RenderElement(XElement element, TreeViewItem parentItem)
    {
        foreach (var attribute in element.Attributes())
        {
            var attributeItem = new TreeViewItem { Header = $"@{attribute.Name.LocalName}: {attribute.Value}" };
            parentItem.Items.Add(attributeItem);
        }

        foreach (var childElement in element.Elements())
        {
            var childItem = new TreeViewItem { Header = childElement.Name.LocalName };
            parentItem.Items.Add(childItem);

            RenderElement(childElement, childItem);
        }

        // If the element contains text content, add it as a TreeViewItem
        if (!string.IsNullOrWhiteSpace(element.Value) && !element.HasElements)
        {
            var textItem = new TreeViewItem { Header = element.Value.Trim() };
            parentItem.Items.Add(textItem);
        }
    }
}