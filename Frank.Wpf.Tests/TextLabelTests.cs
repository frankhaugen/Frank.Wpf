using System.Windows.Markup;
using Frank.Wpf.Controls.SimpleInputs;

namespace Frank.Wpf.Tests;

public class TextLabelTests
{
    [Test]
    [TestExecutor<STAThreadExecutor>]
    public async Task Test3()
    {
        var label = new TextLabel()
        {
            Text = "Hello world"
        };
        
        await TestContext.Current?.OutputWriter.WriteLineAsync($"<Test>");
        
        await TestContext.Current?.OutputWriter.WriteLineAsync($"<Step1>{XamlWriter.Save(label)}</Step1>");
        
        label.Header = "My Header";
        
        await TestContext.Current?.OutputWriter.WriteLineAsync($"<Step2>{XamlWriter.Save(label)}</Step2>");
        
        label.Header = string.Empty;
        
        await TestContext.Current?.OutputWriter.WriteLineAsync($"<Step3>{XamlWriter.Save(label)}</Step3>");
        
        label.Header = "My Header";
        
        await TestContext.Current?.OutputWriter.WriteLineAsync($"<Step4>{XamlWriter.Save(label)}</Step4>");
        
        await TestContext.Current?.OutputWriter.WriteLineAsync($"</Test>");
    }
}