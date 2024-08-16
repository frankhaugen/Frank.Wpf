using System.Windows.Markup;
using Frank.Wpf.Controls.SimpleInputs;
using Xunit.Abstractions;

namespace Frank.Wpf.Tests;

public class TextBoxWithLineNumbersTests(ITestOutputHelper outputHelper)
{
    [WpfFact]
    public void Test1()
    {
        var textBoxWithLineNumbers = new TextBoxWithLineNumbers();
        textBoxWithLineNumbers.Text = "Hello world";
        
        var result = XamlWriter.Save(textBoxWithLineNumbers);
        
        outputHelper.WriteLine(result);
    }
}