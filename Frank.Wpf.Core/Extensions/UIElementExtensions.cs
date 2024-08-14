using System.Windows;
using System.Windows.Markup;

namespace Frank.Wpf.Core;

public static class UIElementExtensions
{
    /// <summary>
    /// Converts a UIElement to XAML.
    /// </summary>
    /// <param name="dumpMe"></param>
    /// <returns>The XAML representation of the UIElement or an exception message.</returns>
    public static string ToXaml(this UIElement dumpMe)
    {
        try
        {
            return XamlWriter.Save(dumpMe);
        }
        catch (Exception e)
        {
            return e.Message + Environment.NewLine + e.StackTrace;
        }
    }
}