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
        
        _jsonRenderer.Render("""
                             {
                                 "name": "Frank",
                                 "age": 30,
                                 "isCool": true,
                                 "address": {
                                    "street": "123 Main St",
                                    "city": "Anytown",
                                    "state": "AS",
                                    "zip": 12345
                                 },
                                 "phoneNumbers": [
                                    "123-456-7890",
                                    "098-765-4321"
                                 ]
                             }
                             """);
        
        Content = _jsonRenderer;
    }
}