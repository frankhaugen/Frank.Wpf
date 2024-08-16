using System.Net.Http;
using System.Text;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Linq;
using Frank.Wpf.Controls.XmlRenderer;
using Frank.Wpf.Core;
using Xunit.Abstractions;

namespace Frank.Wpf.Tests;

public class XmlRendererTreeViewTests(ITestOutputHelper outputHelper)
{
    [WpfFact]
    public void Test1()
    {
        var renderer = new XmlRendererControl();

        var xml = DownloadXmlAsync("https://docs.oasis-open.org/ubl/os-UBL-2.1/xml/UBL-Invoice-2.1-Example.xml");
        renderer.Document = XDocument.Parse(xml);

        var resultXml = XamlWriter.Save(renderer.Content.As<TabControl>()?.Items[0].As<TabItem>() ?? throw new InvalidOperationException());
        // var resultXml = XamlWriter.Save(renderer.Content.As<TabControl>()?.Items[0].As<TabItem>()?.Content.As<DockPanel>()?.Children[1].As<TreeView>() ?? throw new InvalidOperationException());
        var result = PrettyPrint(resultXml);

        outputHelper.WriteLine(result);
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