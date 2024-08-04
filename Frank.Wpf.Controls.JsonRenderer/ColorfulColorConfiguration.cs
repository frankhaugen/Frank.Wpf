using System.Windows.Media;

namespace Frank.Wpf.Controls.JsonRenderer;

public class ColorfulColorConfiguration : ColorConfiguration
{
    /// <inheritdoc />
    public override Brush StringColor { get; } = Brushes.Black;

    /// <inheritdoc />
    public override Brush NumberColor { get; } = Brushes.Blue;

    /// <inheritdoc />
    public override Brush BooleanColor { get; } = Brushes.Green;

    /// <inheritdoc />
    public override Brush NullColor { get; } = Brushes.Gray;

    /// <inheritdoc />
    public override Brush UndefinedColor { get; } = Brushes.Gray;

    /// <inheritdoc />
    public override Brush ObjectColor { get; } = Brushes.Black;

    /// <inheritdoc />
    public override Brush ArrayColor { get; } = Brushes.Sienna;

    /// <inheritdoc />
    public override Brush UnknownColor { get; } = Brushes.Red;

    /// <inheritdoc />
    public override Brush GuidColor { get; } = Brushes.Maroon;

    /// <inheritdoc />
    public override Brush IntegerColor { get; } = Brushes.Blue;
}