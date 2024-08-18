using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using Frank.Wpf.Controls.JsonRenderer;
using Frank.Wpf.Tests.App.Factories;

public class JsonWindow : Window
{
    private readonly JsonRendererControl _jsonRenderer = new();
    
    public JsonWindow()
    {
        Title = "Json Window";
        Width = 800;
        Height = 600;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        
        Content = _jsonRenderer;

        // Initiate async operation to download and set JSON document
        Loaded += OnWindowLoadedAsync;
    }

    private void OnWindowLoadedAsync(object sender, RoutedEventArgs e)
    {
        string json = GetJson();

        // Parse the document and update the UI on the UI thread
        _jsonRenderer.Document = JsonDocument.Parse(json);
    }
    
    private static string GetJson()
    {
        var testData = TestDataFactory.CreateCommunity();
        var json = JsonSerializer.Serialize(testData, new JsonSerializerOptions { WriteIndented = true, Converters = { new JsonStringEnumConverter()}, ReferenceHandler = ReferenceHandler.Preserve});
        return json;
    }
}