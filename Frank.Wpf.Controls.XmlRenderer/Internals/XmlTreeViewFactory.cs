using System.Windows.Controls;
using System.Xml.Linq;

namespace Frank.Wpf.Controls.XmlRenderer.Internals;

public class XmlTreeViewFactory
{
    public TreeView Create(XDocument document, bool includeDeclaration = false)
    {
        var treeView = new TreeView();

        // Render XML Declaration
        if (includeDeclaration && document.Declaration != null) 
            document.Declaration?.Let(declaration => treeView.Items.Add(CreateDeclarationItem(declaration)));

        // Render Root Element
        document.Root?.Let(root =>
        {
            var rootItem = CreateTreeViewItem(root);
            treeView.Items.Add(rootItem);
            RenderElement(root, rootItem);
        });

        return treeView;
    }

    private void RenderElement(XElement element, TreeViewItem parentItem)
    {
        // Check for arrays and render if necessary
        if (IsArrayElement(element, out int arrayCount))
        {
            // Render Array Container but NOT recurse into the parentItem again
            RenderArray(element, parentItem, arrayCount);
            return;
        }

        var item = CreateTreeViewItem(element);
        parentItem.Items.Add(item);

        RenderAttributes(element, item);

        // Recursively render child elements
        foreach (var child in element.Elements()) RenderElement(child, item);

        // Render element value
        RenderElementValue(element, item);
    }

    private void RenderAttributes(XElement element, TreeViewItem item) =>
        element.Attributes()
            .Select(attr => CreateTreeViewItem($"@{attr.Name.LocalName}: {attr.Value}"))
            .ForEach(attrItem => item.Items.Add(attrItem));

    private void RenderElementValue(XElement element, TreeViewItem item)
    {
        if (!element.HasElements && !string.IsNullOrWhiteSpace(element.Value)) 
            item.Items.Add(CreateTreeViewItem($"{element.Value}"));
    }

    private static bool IsArrayElement(XElement element, out int arrayCount)
    {
        var parent = element.Parent;
        arrayCount = parent?.Elements(element.Name).Count() ?? 0;
        return arrayCount > 1 && !(parent?.Name.LocalName.EndsWith("Array") ?? false);
    }

    private void RenderArray(XElement element, TreeViewItem parentItem, int arrayCount)
    {
        // Create the header for the array
        var headerText = $"{element.Name.LocalName} [{arrayCount}]";
            
        // Check if the array has already been rendered and return if it has
        var existing = parentItem.Items.Cast<TreeViewItem>().Select(x => x.Header).Cast<Label>().Select(x => x.Content).Cast<string>().ToList();
        if (existing.Contains(headerText)) return;
            
        // Create a container for the array
        var arrayContainer = CreateTreeViewItem(headerText);
        parentItem.Items.Add(arrayContainer);

        // Render each item in the array
        var siblings = element.Parent?.Elements(element.Name).ToList();
        if (siblings == null) return;

        for (int i = 0; i < siblings.Count; i++)
        {
            var siblingElement = siblings[i];

            // Create a container for the individual array item
            var arrayItem = CreateTreeViewItem($"{siblingElement.Name.LocalName}[{i}]");
            arrayContainer.Items.Add(arrayItem);

            // Render the child elements inside this array item
            siblingElement.Elements().ForEach(child => RenderElement(child, arrayItem));
        }
    }

    private TreeViewItem CreateDeclarationItem(XDeclaration declaration) =>
        CreateTreeViewItem("XML Declaration", children: new[]
        {
            CreateTreeViewItem($"Version: {declaration.Version}"),
            CreateTreeViewItem($"Encoding: {declaration.Encoding}"),
            CreateTreeViewItem($"Standalone: {declaration.Standalone}")
        });

    private static TreeViewItem CreateTreeViewItem(XElement element) =>
        new()
        {
            Header = new Label { Content = element.Name.LocalName },
            ToolTip = new ToolTip { Content = new TextBlock { Text = element.Value } }
        };

    private static TreeViewItem CreateTreeViewItem(string header, params TreeViewItem[] children)
    {
        var item = new TreeViewItem
        {
            Header = new Label { Content = header }
        };

        children.ForEach(child => item.Items.Add(child));

        return item;
    }
}