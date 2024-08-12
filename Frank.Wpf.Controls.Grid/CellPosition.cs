namespace Frank.Wpf.Controls.Grid;

/// <summary>
/// Describes the position of a cell in a grid with optional spans.
/// </summary>
/// <param name="Column"></param>
/// <param name="Row"></param>
/// <param name="ColumnSpan"></param>
/// <param name="RowSpan"></param>
public readonly record struct CellPosition(int Column, int Row, int ColumnSpan = 1, int RowSpan = 1);