using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml;

namespace Frank.Wpf.Core;

public static class XamlSerializer
{
    public static string SerializeToXaml(UIElement element)
    {
        using var stringWriter = new StringWriter();
        using var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true });

        xmlWriter.WriteStartDocument();
        var visitedElements = new HashSet<object>();
        WriteElement(xmlWriter, element, visitedElements);
        xmlWriter.WriteEndDocument();

        var result = string.Empty;
        stringWriter.Write(result);
        return result;
    }

    private static void WriteElement(XmlWriter writer, UIElement element, HashSet<object> visitedElements)
    {
        if (element == null || !visitedElements.Add(element))
            return;

        var type = element.GetType();
        writer.WriteStartElement(type.Name, type.Namespace);

        // Write attributes first
        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (property.CanRead && property.GetValue(element) is not null)
            {
                var value = property.GetValue(element);
                
                if (value is string || value.GetType().IsPrimitive)
                {
                    writer.WriteAttributeString(property.Name, value.ToString());
                }
            }
        }

        // Write child elements
        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (property.CanRead && property.GetValue(element) is UIElement childElement)
            {
                WriteElement(writer, childElement, visitedElements);
            }
        }

        writer.WriteEndElement();
    }
}