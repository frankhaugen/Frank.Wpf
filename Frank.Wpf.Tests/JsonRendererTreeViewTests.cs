using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Linq;
using Frank.Wpf.Controls.JsonRenderer;

namespace Frank.Wpf.Tests;

public class JsonRendererTreeViewTests
{
    // [Test]
    [TestExecutor<STAThreadExecutor>]
    public async Task Test1()
    {
        var json = await DownloadJsonAsync("https://api.nuget.org/v3/index.json");
        var document = JsonDocument.Parse(json);
        var renderer = new JsonRendererControl
        {
            Document = document
        };

        var resultXml = XamlWriter.Save(renderer);
        var result = PrettyPrint(resultXml);

        await TestContext.Current?.OutputWriter.WriteLineAsync(result)!;
    }
    
    private static async Task<string> DownloadJsonAsync(string url)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
    
    private static string PrettyPrint(string xml)
    {
        var stringBuilder = new StringBuilder();

        var element = XElement.Parse(xml);

        var settings = new XmlWriterSettings();
        settings.OmitXmlDeclaration = true;
        settings.Indent = true;
        settings.NewLineOnAttributes = true;

        using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
        {
            element.Save(xmlWriter);
        }

        return stringBuilder.ToString();
    }
}