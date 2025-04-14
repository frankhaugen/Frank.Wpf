using System.Threading.Tasks;

namespace Frank.Wpf.Tests;
using Controls.SimpleInputs;

public class SearchBoxTests
{
    // [Test]
    public async Task SearchBox_ShouldHaveDefaultHeader()
    {
        // Arrange
        var searchBox = new SearchBox();

        // Act & Assert
        await Assert.That(searchBox.Header).IsEqualTo("Search");
    }

    [Test]
    [TestExecutor<STAThreadExecutor>]
    public async Task SearchBox_ShouldUpdateSearchText()
    {
        // Arrange
        var searchBox = new SearchBox();

        // Act
        searchBox.SearchText = "Test search";

        // Assert
        await Assert.That(searchBox.SearchText).IsEqualTo("Test search");
    }

    [Test]
    [TestExecutor<STAThreadExecutor>]
    public async Task SearchBox_ShouldRaiseSearchTextChangedEvent()
    {
        // Arrange
        var searchBox = new SearchBox();
        string? receivedText = null;
        
        searchBox.SearchTextChanged += (text) => receivedText = text;
        
        // Act
        searchBox.SearchText = "New search text";
        
        // Assert
        await Assert.That(receivedText).IsEqualTo("New search text");
    }
}
