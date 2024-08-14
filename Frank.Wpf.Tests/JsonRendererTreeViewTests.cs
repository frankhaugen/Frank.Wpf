using System.IO;
using System.Windows.Markup;
using Frank.Wpf.Controls.JsonRenderer;
using Frank.Wpf.Controls.SimpleInputs;
using Xunit.Abstractions;

namespace Frank.Wpf.Tests;

public class JsonRendererTreeViewTests
{
    private readonly ITestOutputHelper _outputHelper;

    public JsonRendererTreeViewTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [WpfFact]
    public void Test1()
    {
        var renderer = new JsonRendererTreeView();
        renderer.Render("""{"key":"value"}""");
        
        var result = XamlWriter.Save(renderer);
        
        _outputHelper.WriteLine(result);
    }
    
    [WpfFact]
    public void Test2()
    {
        var textBoxWithLineNumbers = new TextBoxWithLineNumbers();
        textBoxWithLineNumbers.Text = "Hello world";
        
        var result = XamlWriter.Save(textBoxWithLineNumbers);
        
        _outputHelper.WriteLine(result);
    }
    
    [WpfFact]
    public void Test3()
    {
        var label = new TextLabel()
        {
            Text = "Hello world"
        };
        
        _outputHelper.WriteLine($"<Test>");
        
        _outputHelper.WriteLine($"<Step1>{XamlWriter.Save(label)}</Step1>");
        
        label.Header = "My Header";
        
        _outputHelper.WriteLine($"<Step2>{XamlWriter.Save(label)}</Step2>");
        
        label.Header = string.Empty;
        
        _outputHelper.WriteLine($"<Step3>{XamlWriter.Save(label)}</Step3>");
        
        label.Header = "My Header";
        
        _outputHelper.WriteLine($"<Step4>{XamlWriter.Save(label)}</Step4>");
        
        _outputHelper.WriteLine($"</Test>");
    }
}