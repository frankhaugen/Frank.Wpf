using System.Windows.Controls;
using ComboBox = System.Windows.Controls.ComboBox;

namespace Frank.Wpf.Core;

public static class ComboboxExtensions
{
    public static void AddItem<T1, T2>(this T1 source, T2 item) where T1 : ComboBox where T2 : ComboBoxItem
    {
        source.Items.Add(item);
    }
}