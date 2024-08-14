using System.Windows.Controls;

namespace Frank.Wpf.Controls.SimpleInputs;

public class TextLabel : BaseControl
{
    private readonly Label _label;

    public TextLabel()
    {
        _label = new Label
        {
            Height = 32
        };
        Content = _label;
    }
    
    public double FontSize
    {
        get => _label.FontSize;
        set => _label.FontSize = value;
    }
    
    public double Height
    {
        get => _label.Height;
        set => _label.Height = value;
    }

    public string Text
    {
        get => _label.Content as string ?? string.Empty;
        set => _label.Content = value;
    }
}