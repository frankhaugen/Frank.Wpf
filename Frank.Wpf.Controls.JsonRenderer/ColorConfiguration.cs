using System.Windows.Media;

namespace Frank.Wpf.Controls.JsonRenderer;

public abstract class ColorConfiguration
{
    internal ColorConfiguration() { }
    
    public abstract Brush StringColor { get;  }
    public abstract Brush NumberColor { get; }
    public abstract Brush BooleanColor { get; }
    public abstract Brush NullColor { get; }
    public abstract Brush UndefinedColor { get; }
    public abstract Brush ObjectColor { get;  }
    public abstract Brush ArrayColor { get; }
    public abstract Brush UnknownColor { get; }
    public abstract Brush GuidColor { get; }
    public abstract Brush IntegerColor { get; }
}