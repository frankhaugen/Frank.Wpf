using System.Windows.Media;

namespace Frank.Wpf.Controls.JsonRenderer;

public class DefaultColorConfiguration : ColorConfiguration
{
    private readonly Brush _color = Brushes.Black;
    
    /// <inheritdoc />
    public override Brush StringColor => _color;
    
    /// <inheritdoc />
    public override Brush NumberColor => _color;
    
    /// <inheritdoc />
    public override Brush BooleanColor => _color;
    
    /// <inheritdoc />
    public override Brush NullColor => _color;
    
    /// <inheritdoc />
    public override Brush UndefinedColor => _color;
    
    /// <inheritdoc />
    public override Brush ObjectColor => _color;
    
    /// <inheritdoc />
    public override Brush ArrayColor => _color;
    
    /// <inheritdoc />
    public override Brush UnknownColor => _color;
    
    /// <inheritdoc />
    public override Brush GuidColor => _color;

    /// <inheritdoc />
    public override Brush IntegerColor => _color;
}