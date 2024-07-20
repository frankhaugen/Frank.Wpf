using System.Windows;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.Grid;

public class Grid : System.Windows.Controls.Grid
{
    private readonly Cell[,] _cells;

    public Grid(int columns, int rows)
    {
        _cells = new Cell[columns, rows];

        for (var i = 0; i < columns; i++) ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
        for (var i = 0; i < rows; i++) RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
        
        for (var i = 0; i < columns; i++)
        for (var j = 0; j < rows; j++) 
            _cells[i, j] = new Cell(new CellPosition(i, j));

        foreach (var cell in _cells) Children.Add(cell);
    }

    public void SetCellContent<T>(int column, int row, T content) where T : UIElement => SetCellContent(new CellPosition(column, row), content);

    public void SetCellContent<T>(CellPosition position, T content) where T : UIElement => _cells[position.Column, position.Row].Content = content;

    public T GetCellContent<T>(int column, int row) where T : UIElement => GetCellContent<T>(new CellPosition(column, row));
    
    public T GetCellContent<T>(CellPosition position) where T : UIElement => (T)_cells[position.Column, position.Row].Content;

    public object GetCellContent(int column, int row) => GetCellContent(new CellPosition(column, row));
    
    public object GetCellContent(CellPosition position) => _cells[position.Column, position.Row].Content;
}