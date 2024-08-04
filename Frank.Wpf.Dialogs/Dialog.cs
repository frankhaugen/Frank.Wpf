using System.Windows;

namespace Frank.Wpf.Dialogs;

public abstract class Dialog : Window
{
    public object? ResultData => GetResultData();

    protected abstract object? GetResultData();
    
    protected Dialog()
    {
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Topmost = true;
        ShowInTaskbar = false;
        ShowActivated = true;
        ResizeMode = ResizeMode.NoResize;
        SizeToContent = SizeToContent.WidthAndHeight;
        WindowStyle = WindowStyle.ToolWindow;
    }

    protected new void ShowDialog() => base.ShowDialog();
}

public abstract class Dialog<T> : Dialog where T : class
{
    public new T? ResultData => GetResultData() as T;
    
    protected abstract override object? GetResultData();
    
    protected Dialog() : base()
    {
    }
    
    public new T? ShowDialog()
    {
        base.ShowDialog();
        return ResultData;
    }
}