using System.Windows;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.ExpandoControl;

public class ExpandoControl : ContentControl
{
    static ExpandoControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ExpandoControl), new FrameworkPropertyMetadata(typeof(ExpandoControl)));
    }
}