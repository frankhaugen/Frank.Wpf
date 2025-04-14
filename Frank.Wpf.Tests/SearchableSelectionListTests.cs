using System.Windows.Controls;
using Frank.Wpf.Controls.SearchableList;
using Frank.Wpf.Controls.SimpleInputs;
using Frank.Wpf.Core;

namespace Frank.Wpf.Tests;

public class SearchableSelectionListTests
{
    [Test]
    [TestExecutor<STAThreadExecutor>]
    public async Task SearchableSelectionList_ShouldDisplayItems()
    {
        // Arrange
        var searchableList = new SearchableSelectionList<string>
        {
            Items = new List<string> { "Apple", "Banana", "Cherry" }
        };
        
        // Act
        var displayedItems = searchableList.Items.ToList();
        
        // Assert
        await Assert.That(displayedItems).HasCount(3);
        await Assert.That(displayedItems).Contains("Apple");
        await Assert.That(displayedItems).Contains("Banana");
        await Assert.That(displayedItems).Contains("Cherry");
    }

    [Test]
    [TestExecutor<STAThreadExecutor>]
    public async Task SearchableSelectionList_ShouldFilterItems()
    {
        // Arrange
        var searchableList = new SearchableSelectionList<string>
        {
            Items = new List<string> { "Apple", "Banana", "Cherry" },
            Filter = (item, searchText) => item.Contains(searchText ?? string.Empty)
        };
        
        // Act
        searchableList.IsSearchBoxVisible = true;
        searchableList.IsSelectedBoxVisible = false;
        
        var searchBox = searchableList.Content.As<StackPanel>()?.Children.OfType<SearchBox>().FirstOrDefault();
        
        searchBox!.SearchText = "pp";
        
        // Assert
        var filteredItems = searchableList.DisplayedItems.ToList();
        await Assert.That(filteredItems).HasCount(1);
        await Assert.That(filteredItems).Contains("Apple");
    }

    // [WpfFact]
    // public void SearchableSelectionList_ShouldHandleItemSelection()
    // {
    //     // Arrange
    //     var selectedItemInvoked = false;
    //     var searchableList = new SearchableSelectionList<string>
    //     {
    //         Items = new List<string> { "Apple", "Banana", "Cherry" },
    //         SelectionChangedAction = _ => selectedItemInvoked = true,
    //         Display = item => $"Selected: {item}"
    //     };
    //
    //     // Act
    //     searchableList.SelectedItem = "Banana";
    //     
    //     // Assert
    //     Assert.Equal("Banana", searchableList.SelectedItem);
    //     Assert.True(selectedItemInvoked);
    //     Assert.Equal("Selected: Banana", ((TextBlock)searchableList._groupBox.Content).Text);
    // }
    //
    // [WpfFact]
    // public void SearchableSelectionList_ShouldToggleSearchBoxVisibility()
    // {
    //     // Arrange
    //     var searchableList = new SearchableSelectionList<string>();
    //     
    //     // Act
    //     searchableList.IsSearchBoxVisible = false;
    //     
    //     // Assert
    //     Assert.Equal(Visibility.Collapsed, searchableList._searchBox.Visibility);
    //     
    //     // Act
    //     searchableList.IsSearchBoxVisible = true;
    //     
    //     // Assert
    //     Assert.Equal(Visibility.Visible, searchableList._searchBox.Visibility);
    // }
    //
    // [WpfFact]
    // public void SearchableSelectionList_ShouldToggleSelectedBoxVisibility()
    // {
    //     // Arrange
    //     var searchableList = new SearchableSelectionList<string>();
    //     
    //     // Act
    //     searchableList.IsSelectedBoxVisible = false;
    //     
    //     // Assert
    //     Assert.Equal(Visibility.Collapsed, searchableList._groupBox.Visibility);
    //     
    //     // Act
    //     searchableList.IsSelectedBoxVisible = true;
    //     
    //     // Assert
    //     Assert.Equal(Visibility.Visible, searchableList._groupBox.Visibility);
    // }
    //
    // [WpfFact]
    // public void SearchableSelectionList_ShouldUpdateDisplayedItemWithCustomDisplayFunc()
    // {
    //     // Arrange
    //     var searchableList = new SearchableSelectionList<string>
    //     {
    //         Items = new List<string> { "Apple", "Banana", "Cherry" },
    //         Display = item => $"Fruit: {item}"
    //     };
    //
    //     // Act
    //     searchableList.SelectedItem = "Cherry";
    //
    //     // Assert
    //     Assert.Equal("Cherry", searchableList.SelectedItem);
    //     Assert.Equal("Fruit: Cherry", ((TextBlock)searchableList._groupBox.Content).Text);
    // }
    //
    // [WpfFact]
    // public void SearchableSelectionList_ShouldTriggerSelectionChangedAction()
    // {
    //     // Arrange
    //     var selectedItemInvoked = false;
    //     var selectedItem = string.Empty;
    //     var searchableList = new SearchableSelectionList<string>
    //     {
    //         Items = new List<string> { "Apple", "Banana", "Cherry" },
    //         SelectionChangedAction = item =>
    //         {
    //             selectedItemInvoked = true;
    //             selectedItem = item;
    //         }
    //     };
    //
    //     // Act
    //     searchableList.SelectedItem = "Cherry";
    //     
    //     // Assert
    //     Assert.True(selectedItemInvoked);
    //     Assert.Equal("Cherry", selectedItem);
    // }

    [Test]
    [TestExecutor<STAThreadExecutor>]
    public async Task SearchableSelectionList_ShouldDisplayEmptyGroupBoxWhenNoSelection()
    {
        // Arrange
        var searchableList = new SearchableSelectionList<string>
        {
            Items = new List<string> { "Apple", "Banana", "Cherry" }
        };
        var groupBox = searchableList.Content.As<StackPanel>()?.Children.OfType<GroupBox>().FirstOrDefault();

        // Act
        searchableList.SelectedItem = null;

        // Assert
        await Assert.That(groupBox).IsNotNull();
    }
}