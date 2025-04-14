using System.Windows.Markup;
using Frank.Wpf.Controls.SimpleInputs;

namespace Frank.Wpf.Tests;

public class TextBoxWithLineNumbersTests
{
    [Test]
    [TestExecutor<STAThreadExecutor>]
    public async Task Test1()
    {
        var textBoxWithLineNumbers = new TextBoxWithLineNumbers();
        textBoxWithLineNumbers.Text = "Hello world";
        
        var result = XamlWriter.Save(textBoxWithLineNumbers);
        
        await TestContext.Current?.OutputWriter.WriteLineAsync(result)!;
    }
}