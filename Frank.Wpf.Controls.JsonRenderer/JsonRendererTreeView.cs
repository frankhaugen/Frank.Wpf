using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Frank.Wpf.Controls.JsonRenderer;

internal class JsonRendererTreeView : TreeView
{
    private JsonElement? _selectedElement;
    private JsonDocument? Document { get; set; }
    private ColorConfiguration ColorConfiguration { get; set; } = new DefaultColorConfiguration();
    
    public JsonRendererTreeView()
    {
        KeyDown += HandleKeyDownEvent;
    }

    public void Render(string json)
    {
        Document = JsonDocument.Parse(json);
        Render();
    }
    
    public void ToggleExpandCollapse(object sender, RoutedEventArgs e)
    {
        foreach (TreeViewItem item in Items)
        {
            ToggleExpandCollapse(item, item.IsExpanded);
        }
    }
    
    public void ChangeColors(ColorConfiguration colorConfiguration)
    {
        ColorConfiguration = colorConfiguration;
        Render();
    }

    private void Render()
    {
        if (Document is null)
        {
            return;
        }

        Items.Clear();

        var root = new TreeViewItem
        {
            Header = "Document",
            IsExpanded = true
        };
        
        root.Selected += (sender, args) => SetSelectedElement(Document.RootElement);
        
        Items.Add(root);

        if (Document.RootElement.ValueKind == JsonValueKind.Object)
        {
            RenderObject(Document.RootElement, root);
        }
        else if (Document.RootElement.ValueKind == JsonValueKind.Array)
        {
            RenderArray(Document.RootElement, root);
        }
    }

    private void RenderArray(JsonElement documentRootElement, TreeViewItem root)
    {
        var index = 0;
        foreach (var element in documentRootElement.EnumerateArray())
        {
            var item = new TreeViewItem
            {
                Header = $"[{index}]"
            };
            root.Items.Add(item);

            RenderElement(element, item);
            index++;
        }
    }

    private void RenderObject(JsonElement documentRootElement, TreeViewItem root)
    {
        foreach (var property in documentRootElement.EnumerateObject())
        {
            var item = new TreeViewItem();
            root.Items.Add(item);

            RenderElement(property.Value, item, property.Name);
        }
    }

    private void RenderElement(JsonElement element, TreeViewItem item, string? propertyName = null)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                item.Header = CreateHeader(propertyName ?? "", ColorConfiguration.ObjectColor, "Object");
                RenderObject(element, item);
                break;
            case JsonValueKind.Array:
                item.Header = CreateHeader($"{propertyName ?? ""} [{element.GetArrayLength()}]", ColorConfiguration.ArrayColor, "Array");
                RenderArray(element, item);
                break;
            case JsonValueKind.String:
                var stringValue = element.GetString();
                var color = IsGuid(stringValue) ? ColorConfiguration.GuidColor : ColorConfiguration.StringColor;
                var type = IsGuid(stringValue) ? "GUID" : "String";
                item.Header = CreateHeader($"{propertyName ?? string.Empty}: {stringValue}", color, type);
                break;
            case JsonValueKind.Number:
                var numberText = element.GetRawText();
                color = int.TryParse(numberText, out var _) ? ColorConfiguration.IntegerColor : ColorConfiguration.StringColor;
                item.Header = CreateHeader($"{propertyName ?? string.Empty}: {numberText}", color, "Number");
                break;
            case JsonValueKind.True:
            case JsonValueKind.False:
                item.Header = CreateHeader($"{propertyName ?? string.Empty}: {element.GetBoolean()}", ColorConfiguration.BooleanColor, "Boolean");
                break;
            case JsonValueKind.Null:
                item.Header = CreateHeader($"{propertyName ?? string.Empty}: null", ColorConfiguration.NullColor, "Null");
                break;
            case JsonValueKind.Undefined:
                item.Header = CreateHeader($"{propertyName ?? string.Empty}: undefined", ColorConfiguration.UndefinedColor, "Undefined");
                break;
            default:
                item.Header = CreateHeader($"{propertyName ?? string.Empty}: {element.GetRawText()}", ColorConfiguration.UnknownColor, "Unknown");
                break;
        }
        item.Selected += (sender, args) => SetSelectedElement(element);
    }

    private void SetSelectedElement(JsonElement element)
    {
        _selectedElement = element;
    }
    
    private void HandleKeyDownEvent(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.C && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
        {
            if (_selectedElement is not null)
            {
                CopyJsonToClipboard(_selectedElement);
            }
        }
    }

    private UIElement CreateHeader(string text, Brush color, string tooltip)
    {
        var textBlock = new TextBlock
        {
            Text = text,
            Foreground = color,
            ToolTip = tooltip
        };
        return textBlock;  // Make sure this is what's being returned and used as the header
    }


    private bool IsGuid(string? value)
    {
        return Guid.TryParse(value, out _);
    }

    private void ToggleExpandCollapse(TreeViewItem item, bool expand)
    {
        item.IsExpanded = !expand;
        foreach (TreeViewItem child in item.Items)
        {
            ToggleExpandCollapse(child, expand);
        }
    }

    private void CopyJsonToClipboard(JsonElement? element)
    {
        if (element is null)
            return;
        var json = element.Value.GetRawText();
        Clipboard.SetText(json);
    }
}