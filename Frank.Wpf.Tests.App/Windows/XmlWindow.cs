using System.Net.Http;
using System.Windows;
using System.Xml.Linq;
using Frank.Wpf.Controls.XmlRenderer;

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
        
        var xml = DownloadXmlAsync("https://docs.oasis-open.org/ubl/os-UBL-2.1/xml/UBL-Invoice-2.1-Example.xml");
        
        var xmlDocument = XDocument.Parse(xml);
        
        _xmlRendererControl.Document = xmlDocument;
        
        Content = _xmlRendererControl;
    }
    
    private static string DownloadXmlAsync(string url)
    {
        using var client = new HttpClient();
        
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("Accept", "application/xml");
        
        var response = client.SendAsync(request).GetAwaiter().GetResult();
        response.EnsureSuccessStatusCode();
        return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
    }
}