using System.Windows;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.Grid;

public class AdvancedGrid : UserControl
{
    private readonly Cell[,] _cells;
    private readonly System.Windows.Controls.Grid _grid = new();

    public AdvancedGrid(int columns, int rows)
    {
        _cells = new Cell[columns, rows];

        for (var i = 0; i < columns; i++) _grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
        for (var i = 0; i < rows; i++) _grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
        
        for (var i = 0; i < columns; i++)
        for (var j = 0; j < rows; j++) 
            _cells[i, j] = new Cell(new CellPosition(i, j));

        foreach (var cell in _cells) _grid.Children.Add(cell);
        
        Content = _grid;
    }
    
    /// <summary>
    /// Clears the grid of all content.
    /// </summary>
    public void Clear() => _grid.Children.Clear();
    
    /// <summary>
    /// Gets the underlying grid's UI element collection.
    /// </summary>
    /// <returns></returns>
    public UIElementCollection GetUIElementCollection() => _grid.Children;
    
    /// <summary>
    /// Gets the UI elements in the grid.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<UIElement> GetUIElements() => _grid.Children.Cast<UIElement>();

    /// <summary>
    /// Sets the content of the cell at the specified position.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="content"></param>
    /// <typeparam name="T"></typeparam>
    public void SetCellContent<T>(CellPosition position, T content) where T : UIElement => _cells[position.Column, position.Row].Content = content;

    /// <summary>
    /// Gets the content of the cell at the specified position.
    /// </summary>
    /// <param name="position"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetCellContent<T>(CellPosition position) where T : UIElement => (T)_cells[position.Column, position.Row].Content;

    /// <summary>
    /// Gets the content of the cell at the specified position without knowing the type.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public object GetCellContent(CellPosition position) => _cells[position.Column, position.Row].Content;
}