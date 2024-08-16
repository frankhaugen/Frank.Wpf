using System.Windows.Markup;
using Frank.Wpf.Controls.SimpleInputs;
using Xunit.Abstractions;

namespace Frank.Wpf.Tests;

public class TextLabelTests(ITestOutputHelper outputHelper)
{
    [WpfFact]
    public void Test3()
    {
        var label = new TextLabel()
        {
            Text = "Hello world"
        };
        
        outputHelper.WriteLine($"<Test>");
        
        outputHelper.WriteLine($"<Step1>{XamlWriter.Save(label)}</Step1>");
        
        label.Header = "My Header";
        
        outputHelper.WriteLine($"<Step2>{XamlWriter.Save(label)}</Step2>");
        
        label.Header = string.Empty;
        
        outputHelper.WriteLine($"<Step3>{XamlWriter.Save(label)}</Step3>");
        
        label.Header = "My Header";
        
        outputHelper.WriteLine($"<Step4>{XamlWriter.Save(label)}</Step4>");
        
        outputHelper.WriteLine($"</Test>");
    }
}