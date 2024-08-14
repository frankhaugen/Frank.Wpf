using System.Windows;
using System.Windows.Controls;

namespace Frank.Wpf.Tests.App.Windows;

public class CodeWindow : Window
{
    public CodeWindow()
    {
        Title = "Code Window";
        Width = 800;
        Height = 600;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        
        var codeArea = new Frank.Wpf.Controls.Code.CodeArea(new ICSharpCode.AvalonEdit.Highlighting.HighlightingDefinitionTypeConverter().ConvertFrom("C#") as ICSharpCode.AvalonEdit.Highlighting.IHighlightingDefinition ?? throw new InvalidOperationException());
        codeArea.Text = """
                        {
                            "name": "Frank",
                            "age": 30,
                          "isCool": true,
                            "address": {
                                   "street": "123 Main St",
                               "city": "Anytown",
                               "state": "AS",
                               "zip": 12345},
                            "phoneNumbers": [
                            "123-456-7890",
                            "098-765-4321"
                            ]
                        }
                        """;
        
        var codeBeautifier = new Frank.Wpf.Controls.Code.JsonBeautifier();
        
        var beautifyButton = new Button
        {
            Content = "Beautify",
            Margin = new(5),
            Padding = new(5),
            HorizontalAlignment = HorizontalAlignment.Left
        };
        
        beautifyButton.Click += (sender, args) => codeArea.Beautify(codeBeautifier);
        
        var stackPanel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Margin = new(5),
            Children =
            {
                beautifyButton
            }
        };
        
        var grid = new Grid();
        
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        
        grid.Children.Add(stackPanel);
        grid.Children.Add(codeArea);
        
        Grid.SetRow(codeArea, 1);
        
        Content = grid;
    }
}