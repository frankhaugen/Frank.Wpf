using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using Frank.Wpf.Controls.JsonRenderer;
using Frank.Wpf.Controls.SearchableList;

namespace Frank.Wpf.Tests.App.Windows;

public class SearchableSelectionListWindow : Window
{
    private readonly StackPanel _stackPanel = new() { Orientation = Orientation.Horizontal };
    private readonly SearchableSelectionList<Person> _searchableSelectionList;
    private readonly JsonRendererControl _jsonRenderer = new();
    
    public SearchableSelectionListWindow()
    {
        Title = "Searchable Selection List Window";
        Width = 800;
        Height = 600;
        
        var items = new List<Person>
        {
            new Person { Name = "Frank", Age = 30, Address = new Address { Street = "123 Oak St", City = "Anytown", State = "NY", Zip = "12345" } },
            new Person { Name = "John", Age = 40, Address = new Address { Street = "123 Elm St", City = "Anytown", State = "NY", Zip = "12345" } },
            new Person { Name = "Jane", Age = 50, Address = new Address { Street = "123 Main St", City = "Anytown", State = "NY", Zip = "12345" } }
        };
        
        _searchableSelectionList = new SearchableSelectionList<Person>()
        {
            DisplayFunc = item => item.Name,
            Items = items,
            SelectionChangedAction = item =>
            {
                _jsonRenderer.Document = JsonSerializer.SerializeToDocument(item, new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase, Converters = { new JsonStringEnumConverter() }});
            }
        };
        
        _stackPanel.Children.Add(_searchableSelectionList);
        _stackPanel.Children.Add(_jsonRenderer);

        Content = _stackPanel;
    }

    internal class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Address Address { get; set; }
    }
    
    internal class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
