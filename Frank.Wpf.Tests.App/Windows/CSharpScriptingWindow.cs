using System.Windows;
using Frank.Wpf.Controls.RoslynScript;

namespace Frank.Wpf.Tests.App.Windows;

public class CSharpScriptingWindow : Window
{
    public CSharpScriptingWindow()
    {
        Content = new CSharpScriptControl();
    }
}