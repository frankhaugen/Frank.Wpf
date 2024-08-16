using System.Text;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Linq;
using Frank.Wpf.Controls.CSharpRenderer;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit.Abstractions;

namespace Frank.Wpf.Tests;

public class CsharpTreeViewFactoryTests(ITestOutputHelper outputHelper)
{
    [WpfFact]
    public void Test1()
    {
        var factory = new CsharpSyntaxTreeViewFactory();
        
        var syntaxTree = GetSyntaxTree();
        
        var treeView = factory.Create(syntaxTree);
        var resultXml = XamlWriter.Save(treeView);
        var result = PrettyPrint(resultXml);

        outputHelper.WriteLine(result);
    }

    private SyntaxTree GetSyntaxTree()
    {
        var code = """
                   using System;
                   using System.Collections.Generic;
                   
                   namespace HelloWorld
                   {
                       public class Program
                       {
                           public static void Main()
                           {
                               Console.WriteLine(""Hello, World!"");
                           }
                       }
                   }
                   """;
        
        return CSharpSyntaxTree.ParseText(code);
    }

    private static string PrettyPrint(string xml)
    {
        var stringBuilder = new StringBuilder();

        var element = XElement.Parse(xml);

        var settings = new XmlWriterSettings
        {
            OmitXmlDeclaration = true,
            Indent = true,
            NewLineOnAttributes = true
        };

        using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
        {
            element.Save(xmlWriter);
        }

        return stringBuilder.ToString();
    }
}