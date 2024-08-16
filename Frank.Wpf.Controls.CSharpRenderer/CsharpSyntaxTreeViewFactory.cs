using System.Windows.Controls;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Frank.Wpf.Controls.CSharpRenderer;

public class CsharpSyntaxTreeViewFactory
{
    public TreeView Create(SyntaxTree syntaxTree)
    {
        var treeView = new TreeView();
        var root = syntaxTree.GetRoot();

        // Find all namespace declarations
        var namespaceNodes = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>().ToList();

        // Add namespaces first
        foreach (var namespaceNode in namespaceNodes)
        {
            var namespaceItem = new TreeViewItem
            {
                Header = namespaceNode.Name.ToString()
            };

            treeView.Items.Add(namespaceItem);

            // For each namespace, add the types within it
            AddTypesToNamespace(namespaceItem, namespaceNode);
        }

        // Find all types that are not inside namespaces
        var nonNamespaceTypes = root.DescendantNodes().OfType<TypeDeclarationSyntax>()
            .Where(t => !t.Ancestors().OfType<NamespaceDeclarationSyntax>().Any()).ToList();

        // Add these types at the root level, after namespaces
        foreach (var typeNode in nonNamespaceTypes)
        {
            var typeItem = new TreeViewItem
            {
                Header = typeNode.Identifier.Text
            };

            treeView.Items.Add(typeItem);

            // Add members (methods, properties, etc.) to the type
            AddMembersToType(typeItem, typeNode);
        }

        return treeView;
    }

    private void AddTypesToNamespace(TreeViewItem namespaceItem, NamespaceDeclarationSyntax namespaceNode)
    {
        var types = namespaceNode.DescendantNodes().OfType<TypeDeclarationSyntax>();

        foreach (var typeNode in types)
        {
            var typeItem = new TreeViewItem
            {
                Header = typeNode.Identifier.Text
            };

            namespaceItem.Items.Add(typeItem);

            // Add members (methods, properties, etc.) to the type
            AddMembersToType(typeItem, typeNode);
        }
    }

    private void AddMembersToType(TreeViewItem typeItem, TypeDeclarationSyntax typeNode)
    {
        // Add methods
        var methodNodes = typeNode.DescendantNodes().OfType<MethodDeclarationSyntax>();
        foreach (var methodNode in methodNodes)
        {
            var methodItem = new TreeViewItem
            {
                Header = $"{methodNode.Identifier.Text}({string.Join(", ", methodNode.ParameterList.Parameters.Select(p => p.Type.ToString()))})"
            };

            typeItem.Items.Add(methodItem);
        }

        // Add properties
        var propertyNodes = typeNode.DescendantNodes().OfType<PropertyDeclarationSyntax>();
        foreach (var propertyNode in propertyNodes)
        {
            var propertyItem = new TreeViewItem
            {
                Header = propertyNode.Identifier.Text
            };

            typeItem.Items.Add(propertyItem);
        }
    }
}
