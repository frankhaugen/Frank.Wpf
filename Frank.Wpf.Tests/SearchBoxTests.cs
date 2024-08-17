namespace Frank.Wpf.Tests;

using Xunit;
using Controls.SimpleInputs;

public class SearchBoxTests
{
    [WpfFact]
    public void SearchBox_ShouldHaveDefaultHeader()
    {
        // Arrange
        var searchBox = new SearchBox();
        
        // Act & Assert
        Assert.Equal("Search", searchBox.Header);
    }

    [WpfFact]
    public void SearchBox_ShouldUpdateSearchText()
    {
        // Arrange
        var searchBox = new SearchBox();
        
        // Act
        searchBox.SearchText = "Test search";
        
        // Assert
        Assert.Equal("Test search", searchBox.SearchText);
    }

    [WpfFact]
    public void SearchBox_ShouldRaiseSearchTextChangedEvent()
    {
        // Arrange
        var searchBox = new SearchBox();
        string? receivedText = null;
        
        searchBox.SearchTextChanged += (text) => receivedText = text;
        
        // Act
        searchBox.SearchText = "New search text";
        
        // Assert
        Assert.Equal("New search text", receivedText);
    }
}
