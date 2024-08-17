using Frank.Wpf.Controls.SimpleInputs;

namespace Frank.Wpf.Tests;

public class CustomListBoxTests
{
    [WpfFact]
    public void CustomListBox_ShouldSetItems()
    {
        // Arrange
        var listBox = new CustomListBox<string>();
        var items = new[] { "Item1", "Item2", "Item3" };
        
        // Act
        listBox.Items = items;
        
        // Assert
        Assert.Equal(items, listBox.Items.ToArray());
    }

    [WpfFact]
    public void CustomListBox_ShouldSelectItem()
    {
        // Arrange
        var listBox = new CustomListBox<string>();
        var items = new[] { "Item1", "Item2", "Item3" };
        listBox.Items = items;
        
        // Act
        listBox.SetSelectedItem("Item2");
        
        // Assert
        Assert.Equal("Item2", listBox.SelectedItem);
    }

    [WpfFact]
    public void CustomListBox_ShouldFilterItems()
    {
        // Arrange
        var listBox = new CustomListBox<string>
        {
            Items = new[] { "Item1", "Item2", "Item3" },
            FilterFunc = item => item.Contains("2") // Filter out items that contain "2"
        };
    
        // Act
        listBox.Items = listBox.Items; // Trigger UpdateItemsSource
    
        // Assert
        var filteredItems = listBox.DisplayedItems.ToArray();
        Assert.Single(filteredItems);
        Assert.Equal("Item2", filteredItems[0]);
    }


    [WpfFact]
    public void CustomListBox_ShouldUseDisplayFunc()
    {
        // Arrange
        var listBox = new CustomListBox<int>
        {
            Items = new[] { 1, 2, 3 },
            DisplayFunc = item => $"Item {item}"
        };
        
        // Act
        listBox.Items = listBox.Items; // Trigger UpdateItemsSource
        
        // Assert
        var displayedItems = listBox.Items.ToArray();
        Assert.Equal("Item 1", listBox.DisplayFunc(1));
        Assert.Equal("Item 2", listBox.DisplayFunc(2));
        Assert.Equal("Item 3", listBox.DisplayFunc(3));
    }
}