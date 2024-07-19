using System.Windows.Controls;
using Control = System.Windows.Controls.Control;
using Panel = System.Windows.Controls.Panel;
using TextBox = System.Windows.Controls.TextBox;

namespace Frank.Wpf.Core;

public static class ControlExtensions
{
    public static void SetGridPosition<T>(this T control, int x, int y) where T : Control
    {
        Grid.SetColumn(control, x);
        Grid.SetRow(control, y);
    }

    public static void SetGridSpan<T>(this T control, int x, int y) where T : Control
    {
        Grid.SetColumnSpan(control, x);
        Grid.SetRowSpan(control, y);
    }

    public static void SetGridZIndex<T>(this T control, int z) where T : Control
    {
        Panel.SetZIndex(control, z);
    }

    public static Guid GetId<T>(this T control) where T : Control
    {
        var uid = control.Uid;
        return Guid.Parse(uid);
    }

    public static void SetId<T>(this T control, Guid id) where T : Control
    {
        var uid = id.ToString();
        control.Uid = uid;
    }

    public static bool HasId<T>(this T control, Guid id) where T : Control
    {
        var uid = id.ToString();
        return control.Uid == uid;
    }

    public static TResult As<TSource, TResult>(this TSource control) where TSource : Control where TResult : Control
    {
        return control as TResult ?? throw new ArgumentException($"The type {typeof(TSource).Name} is not castable to {typeof(TResult).Name}");
    }

    public static bool TryAs<TSource, TResult>(this TSource control, out TResult output) where TSource : Control where TResult : Control
    {
        if (control is not TResult casted)
        {
            output = default!;
            return false;
        }

        output = casted;
        return true;
    }

    public static bool TryAs<TSource, TResult>(this TSource control) where TSource : Control where TResult : Control => control is TResult casted;

    public static TextBox? GetTextboxById<T>(this T source, Guid id) where T : Control
    {
        return source.GetDecendants<TextBox>().FirstOrDefault(x => x.HasId(id));
    }

    public static string GetTextById<T>(this T source, Guid id) where T : Control
    {
        return source.GetTextboxById(id)?.Text ?? "";
    }

    public static T GetDecendantById<TSource, T>(this TSource obj, Guid id) where TSource : Control where T : Control => obj.GetDecendants<T>().FirstOrDefault(x => x.HasId(id));
}