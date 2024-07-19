using System.Windows;
using System.Windows.Controls;

namespace Frank.Wpf.Core;

public static class GridExtensions
{
    public static void AddChild(this Grid grid, UIElement child, int row, int column)
    {
        Grid.SetRow(child, row);
        Grid.SetColumn(child, column);
        grid.Children.Add(child);
    }
    
    public static void AddPage<T>(this Grid grid, T child, int row, int column) where T : Page
    {
        var frame = new Frame() { Content = child, MinWidth = 256, MinHeight = 256};
        Grid.SetRow(frame, row);
        Grid.SetColumn(frame, column);
        grid.Children.Add(frame);
    }

    public static void GenerateGridRowsAndColumns(this Grid grid, int rows, int columns)
    {
        for (var i = 0; i < columns; i++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
        }

        for (var i = 0; i < rows; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
        }
    }
}