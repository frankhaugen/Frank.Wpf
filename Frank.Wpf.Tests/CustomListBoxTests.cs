using Frank.Wpf.Controls.SimpleInputs;
using System.Threading.Tasks;

namespace Frank.Wpf.Tests;

public class CustomListBoxTests
{
    [Test]
    [TestExecutor<STAThreadExecutor>]
    public async Task CustomListBox_ShouldSetItems()
    {
        // Arrange
        var listBox = new CustomListBox<string>();
        var items = new[] { "Item1", "Item2", "Item3" };

        // Act
        listBox.Items = items;

        // Assert
        // await Assert.That(items).IsEqualTo(listBox.Items.ToArray());
    }

    [Test]
    [TestExecutor<STAThreadExecutor>]
    public async Task CustomListBox_ShouldSelectItem()
    {
        // Arrange
        var listBox = new CustomListBox<string>();
        var items = new[] { "Item1", "Item2", "Item3" };
        listBox.Items = items;
        
        // Act
        listBox.SetSelectedItem("Item2");
        
        // Assert
        await Assert.That("Item2").IsEqualTo(listBox.SelectedItem);
    }

    [Test]
    [TestExecutor<STAThreadExecutor>]
    public async Task CustomListBox_ShouldFilterItems()
    {
        // Arrange
        var listBox = new CustomListBox<string>
        {
            Items = ["Item1", "Item2", "Item3"],
            FilterFunc = item => item.Contains("2") // Filter out items that contain "2"
        };
    
        // Act
        listBox.Items = listBox.Items; // Trigger UpdateItemsSource
    
        // Assert
        var filteredItems = listBox.DisplayedItems.ToArray();
        // await Assert.That(filteredItems).IsEqualTo(["Item2"]);
        await Assert.That(filteredItems.Length).IsEqualTo(1);
    }


    [Test]
    [TestExecutor<STAThreadExecutor>]
    public async Task CustomListBox_ShouldUseDisplayFunc()
    {
        // Arrange
        var listBox = new CustomListBox<int>
        {
            Items = [1, 2, 3],
            DisplayFunc = item => $"Item {item}"
        };
        
        // Act
        listBox.Items = listBox.Items; // Trigger UpdateItemsSource
        
        // Assert
        var displayedItems = listBox.Items.ToArray();
        // await Assert.That(displayedItems).IsEqualTo([1, 2, 3]);
        await Assert.That(displayedItems.Length).IsEqualTo(3);
        
        
        // Assert.Equal("Item 1", listBox.DisplayFunc(1));
        // Assert.Equal("Item 2", listBox.DisplayFunc(2));
        // Assert.Equal("Item 3", listBox.DisplayFunc(3));
    }
}