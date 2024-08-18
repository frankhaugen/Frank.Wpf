using System.Windows.Controls;

namespace Frank.Wpf.Core;

public static class GridFactory
{
    public static Grid Create(int columns, int rows, Grid? grid = null)
    {
        grid ??= new Grid();
        
        grid.ColumnDefinitions.Clear();
        grid.RowDefinitions.Clear();
        
        for (var i = 0; i < columns; i++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition() {  });
        }
        for (var i = 0; i < rows; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition() { });
        }
        return grid;
    }
}