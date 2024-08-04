using System.IO;
using System.Windows.Markup;
using Frank.Wpf.Controls.JsonRenderer;
using Xunit.Abstractions;

namespace Frank.Wpf.Tests;

public class UnitTest1
{
    private readonly ITestOutputHelper _outputHelper;

    public UnitTest1(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [WpfFact]
    public void Test1()
    {
        var renderer = new JsonRendererTreeView();
        renderer.Render("""{"key":"value"}""");
        
        using var writer = new StringWriter();
        
        
        XamlWriter.Save(renderer, writer);
        
        _outputHelper.WriteLine(writer.ToString());
    }
}