using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Frank.Wpf.Core;

public static class GridExtensions
{

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

    public static void Add(this Grid grid, UIElement element, int column, int row, int columnSpan = 1, int rowSpan = 1)
    {
        grid.EnsureRowExist(row);
        grid.EnsureColumnExist(column);
        
        Grid.SetColumn(element, column);
        Grid.SetRow(element, row);
        Grid.SetColumnSpan(element, columnSpan);
        Grid.SetRowSpan(element, rowSpan);
        grid.Children.Add(element);
    }
    
    public static void AddPage<T>(this Grid grid, T child, int row, int column, int columnSpan = 1, int rowSpan = 1) where T : Page 
        => grid.Add(new Frame() { Content = child, MinWidth = 256, MinHeight = 256, NavigationUIVisibility = NavigationUIVisibility.Hidden}, column, row, columnSpan, rowSpan);

    public static void EnsureRowExist(this Grid grid, int row)
    {
        if (grid.RowDefinitions.Count > row) return;
        while (grid.RowDefinitions.Count <= row)
            grid.RowDefinitions.Add(new RowDefinition());
    }
    
    public static void EnsureColumnExist(this Grid grid, int column)
    {
        if (grid.ColumnDefinitions.Count > column) return;
        while (grid.ColumnDefinitions.Count <= column)
            grid.ColumnDefinitions.Add(new ColumnDefinition());
    }
    
    public static void SetRowHeight(this Grid grid, int row, GridLength height)
    {
        grid.EnsureRowExist(row);
        grid.RowDefinitions[row].Height = height;
    }

    public static void SetColumnWidth(this Grid grid, int column, GridLength width)
    {
        grid.EnsureColumnExist(column);
        grid.ColumnDefinitions[column].Width = width;
    }

    public static void SetRowHeight(this Grid grid, int row, double height) => grid.SetRowHeight(row, new GridLength(height));

    public static void SetColumnWidth(this Grid grid, int column, double width) => grid.SetColumnWidth(column, new GridLength(width));

    public static void SetCellWidthAndHeight(this Grid grid, int column, int row, double width, double height)
    {
        grid.SetColumnWidth(column, width);
        grid.SetRowHeight(row, height);
    }
    
    public static void SetCellWidthAndHeight(this Grid grid, int column, int row, GridLength width, GridLength height)
    {
        grid.SetColumnWidth(column, width);
        grid.SetRowHeight(row, height);
    }
}