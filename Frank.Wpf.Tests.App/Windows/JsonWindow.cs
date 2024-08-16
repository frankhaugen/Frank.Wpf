using System.Net.Http;
using System.Windows;
using Frank.Wpf.Controls.JsonRenderer;

namespace Frank.Wpf.Tests.App.Windows;

public class JsonWindow : Window
{
    private readonly JsonRendererControl _jsonRenderer = new();
    
    public JsonWindow()
    {
        Title = "Json Window";
        Width = 800;
        Height = 600;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        
        var json = DownloadJsonAsync("https://api.nuget.org/v3/index.json");
        
        _jsonRenderer.Document = System.Text.Json.JsonDocument.Parse(json);
        
        Content = _jsonRenderer;
    }
    
    private static string DownloadJsonAsync(string url)
    {
        using var client = new HttpClient();
        var response = client.GetAsync(url).GetAwaiter().GetResult();
        response.EnsureSuccessStatusCode();
        return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
    }
}