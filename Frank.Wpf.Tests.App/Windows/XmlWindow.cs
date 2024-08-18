using System.IO;
using System.Net.Http;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Serialization;
using Frank.Wpf.Controls.XmlRenderer;
using Frank.Wpf.Tests.App.Factories;
using Frank.Wpf.Tests.App.Models;

namespace Frank.Wpf.Tests.App.Windows;

public class XmlWindow : Window
{
    private readonly XmlRendererControl _xmlRendererControl = new();
    
    public XmlWindow()
    {
        Title = "Xml Window";
        Width = 800;
        Height = 600;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        
        var xml = GetXml();
        
        var xmlDocument = XDocument.Parse(xml);
        
        _xmlRendererControl.Document = xmlDocument;
        
        Content = _xmlRendererControl;
    }
    
    private static string GetXml()
    {
        var testData = TestDataFactory.CreateCommunity();
        
        var serializer = new XmlSerializer(typeof(Community));
        var xml = "";

        using var stringWriter = new StringWriter();
        serializer.Serialize(stringWriter, testData);
        xml = stringWriter.ToString();
        
        return xml;
    }
}