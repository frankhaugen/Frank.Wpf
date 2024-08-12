using System.Windows.Controls;
using Frank.Wpf.Core;

namespace Frank.Wpf.Controls.Grid;

public class Cell : UserControl
{
    private CellPosition _position;

    public Cell(CellPosition position)
    {
        SetPosition(position);
    }

    public CellPosition GetPosition() => _position;

    public void SetPosition(CellPosition position)
    {
        _position = position;
        this.SetGridPosition(_position.Column, _position.Row);
        this.SetGridSpan(_position.ColumnSpan, _position.RowSpan);
    }
}