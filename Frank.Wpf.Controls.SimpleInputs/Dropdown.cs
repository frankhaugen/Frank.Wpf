using System.Diagnostics;
using System.Windows.Controls;
using Frank.Wpf.Core;

namespace Frank.Wpf.Controls.SimpleInputs;

public class Dropdown : GroupBox
{
    private readonly ComboBox _content = new ComboBox();

    public Dropdown(string header, SelectionChangedEventHandler selectionChanged)
    {
        Header = header;
        _content.SelectionChanged += selectionChanged;
        _content.SelectionChanged += ContentOnSelectionChanged;

        base.Content = _content;
    }

    private void ContentOnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            SelectedId = e.AddedItems[0].As<ComboBoxItem>()!.GetId();
        }
        catch (Exception exception)
        {
            Trace.TraceError(exception.Message);
            Debugger.Log(0, "Error", exception.Message);
            Debugger.Break();
        }
    }

    public Guid SelectedId { get; private set; } = Guid.Empty;

    public void CreateAndAddItem(Guid id, string value)
    {
        var item = CreateItem(id, value);
        _content.AddItem(item);
    }

    private ComboBoxItem CreateItem(Guid id, string value)
    {
        var item = new ComboBoxItem();
        item.Content = value;
        item.SetId(id);
        return item;
    }
}