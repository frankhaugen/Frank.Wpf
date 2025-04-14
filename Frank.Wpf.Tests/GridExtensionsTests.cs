using System.Windows;
using System.Windows.Controls;
using Frank.Wpf.Core;

namespace Frank.Wpf.Tests;

public class GridExtensionsTests
{
    [Test]
    [TestExecutor<STAThreadExecutor>]
    public async Task SetRowHeight_SetsHeightForExistingRow()
    {
        var grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition());
        var height = new GridLength(100);

        grid.SetRowHeight(0, height);

        // Assert
        await Assert.That(grid.RowDefinitions[0].Height).IsEqualTo(height);
    }

    [Test]
    [TestExecutor<STAThreadExecutor>]
    public async Task SetRowHeight_AddsRowDefinitionsIfNeeded()
    {
        var grid = new Grid();
        var height = new GridLength(100);

        grid.SetRowHeight(2, height);
        
        // Assert
        await Assert.That(grid.RowDefinitions[2].Height).IsEqualTo(height);

        // Assert.Equal(3, grid.RowDefinitions.Count);
        // Assert.Equal(height, grid.RowDefinitions[2].Height);
    }

    [Test]
    [TestExecutor<STAThreadExecutor>]
    public async Task SetRowHeight_DoesNotChangeOtherRows()
    {
        var grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
        var height = new GridLength(100);

        grid.SetRowHeight(1, height);
        
        // Assert
        await Assert.That(grid.RowDefinitions[0].Height).IsEqualTo(new GridLength(50));
        await Assert.That(grid.RowDefinitions[1].Height).IsEqualTo(height);

        // Assert.Equal(new GridLength(50), grid.RowDefinitions[0].Height);
        // Assert.Equal(height, grid.RowDefinitions[1].Height);
    }

    [Test]
    [TestExecutor<STAThreadExecutor>]
    public async Task SetColumnWidth_SetsWidthForExistingColumn()
    {
        var grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition());
        var width = new GridLength(100);

        grid.SetColumnWidth(0, width);

        // Assert
        await Assert.That(grid.ColumnDefinitions[0].Width).IsEqualTo(width);
        // Assert.Equal(width, grid.ColumnDefinitions[0].Width);
    }
    
    [Test]
    [TestExecutor<STAThreadExecutor>]
    public async Task SetColumnWidth_AddsColumnDefinitionsIfNeeded()
    {
        var grid = new Grid();
        var width = new GridLength(100);

        grid.SetColumnWidth(2, width);
        
        // Assert
        await Assert.That(grid.ColumnDefinitions[2].Width).IsEqualTo(width);
        await Assert.That(grid.ColumnDefinitions.Count).IsEqualTo(3);

        // Assert.Equal(3, grid.ColumnDefinitions.Count);
        // Assert.Equal(width, grid.ColumnDefinitions[2].Width);
    }
    
    [Test]
    [TestExecutor<STAThreadExecutor>]
    public async Task SetColumnWidth_DoesNotChangeOtherColumns()
    {
        var grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
        var width = new GridLength(100);

        grid.SetColumnWidth(1, width);

        // Assert
        await Assert.That(grid.ColumnDefinitions[0].Width).IsEqualTo(new GridLength(50));
        await Assert.That(grid.ColumnDefinitions[1].Width).IsEqualTo(width);
        
        // Assert.Equal(new GridLength(50), grid.ColumnDefinitions[0].Width);
        // Assert.Equal(width, grid.ColumnDefinitions[1].Width);
    }
}