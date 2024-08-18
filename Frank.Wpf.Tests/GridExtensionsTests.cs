using System.Windows;
using System.Windows.Controls;
using Frank.Wpf.Core;

namespace Frank.Wpf.Tests;

public class GridExtensionsTests
{
    [WpfFact]
    public void SetRowHeight_SetsHeightForExistingRow()
    {
        var grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition());
        var height = new GridLength(100);

        grid.SetRowHeight(0, height);

        Assert.Equal(height, grid.RowDefinitions[0].Height);
    }

    [WpfFact]
    public void SetRowHeight_AddsRowDefinitionsIfNeeded()
    {
        var grid = new Grid();
        var height = new GridLength(100);

        grid.SetRowHeight(2, height);

        Assert.Equal(3, grid.RowDefinitions.Count);
        Assert.Equal(height, grid.RowDefinitions[2].Height);
    }

    [WpfFact]
    public void SetRowHeight_DoesNotChangeOtherRows()
    {
        var grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
        var height = new GridLength(100);

        grid.SetRowHeight(1, height);

        Assert.Equal(new GridLength(50), grid.RowDefinitions[0].Height);
        Assert.Equal(height, grid.RowDefinitions[1].Height);
    }

    [WpfFact]
    public void SetColumnWidth_SetsWidthForExistingColumn()
    {
        var grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition());
        var width = new GridLength(100);

        grid.SetColumnWidth(0, width);

        Assert.Equal(width, grid.ColumnDefinitions[0].Width);
    }
    
    [WpfFact]
    public void SetColumnWidth_AddsColumnDefinitionsIfNeeded()
    {
        var grid = new Grid();
        var width = new GridLength(100);

        grid.SetColumnWidth(2, width);

        Assert.Equal(3, grid.ColumnDefinitions.Count);
        Assert.Equal(width, grid.ColumnDefinitions[2].Width);
    }
    
    [WpfFact]
    public void SetColumnWidth_DoesNotChangeOtherColumns()
    {
        var grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
        var width = new GridLength(100);

        grid.SetColumnWidth(1, width);

        Assert.Equal(new GridLength(50), grid.ColumnDefinitions[0].Width);
        Assert.Equal(width, grid.ColumnDefinitions[1].Width);
    }
}