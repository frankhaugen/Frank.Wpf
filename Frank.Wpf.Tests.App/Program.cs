using System.Windows;
using System.Windows.Controls;
using Frank.Wpf.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Timer = System.Timers.Timer;

namespace Frank.Wpf.Tests.App;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        var builder = Host.CreateWpfHostBuilder();
        builder.Services.AddTransient<IMyService, MyService>();
        builder.Services.AddWindow<OtherWindow>();
        builder.Services.AddWindow<DisplayWindow>();
        builder.Services.AddWindow<InputWindow>();
        var host = builder.Build<MyWindow>();
        host.Run();
    }
}

public class MyWindow : MainWindow
{
    private readonly IMyService _service;
    
    public MyWindow(IMyService service)
    {
        _service = service;
        Title = "Hello, World!";
        Width = 800;
        Height = 600;
        
        var button = new Button()
        {
            Content = "Click me!"
        };
        
        button.Click += (sender, args) => _service.DoSomething();
        
        Content = button;
    }
}

public interface IMyService
{
    void DoSomething();
}

public class MyService : IMyService
{
    private readonly IWindowFactory _windowFactory;

    public MyService(IWindowFactory windowFactory)
    {
        _windowFactory = windowFactory;
    }

    public void DoSomething()
    {
        _windowFactory.CreateWindow<OtherWindow>(true);
        _windowFactory.CreateWindow("DisplayWindow", true);
        _windowFactory.CreateWindow<InputWindow>(true);
    }
}

public class OtherWindow : Window
{
    public OtherWindow(IServiceCollection services)
    {
        Title = "Hello, World!";
        Width = 800;
        Height = 600;
        
        var servicesGrid = new DataGrid
        {
            ItemsSource = services,
            AutoGenerateColumns = true,
            CanUserAddRows = false,
            CanUserDeleteRows = false,
            CanUserReorderColumns = false,
            CanUserResizeColumns = true,
            CanUserResizeRows = true,
            CanUserSortColumns = true,
            IsReadOnly = true,
            SelectionMode = DataGridSelectionMode.Single,
            SelectionUnit = DataGridSelectionUnit.FullRow,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto
        };
        
        var scrollViewer = new ScrollViewer
        {
            Content = servicesGrid,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto
        };

        Content = scrollViewer;
    }
}

public class DisplayWindow : Window
{
    private readonly IApplicationCache _cache;
    private readonly Label _label;
    private readonly Timer _timer;
    
    public DisplayWindow(IApplicationCache cache)
    {
        _cache = cache;
        Title = "Hello, World!";
        Name = "DisplayWindow";
        Width = 800;
        Height = 600;
        
        _label = new Label
        {
            Content = "Hello, World!"
        };
        
        Content = _label;
        
        _timer = new Timer(1000);
        _timer.Elapsed += (_, _) => UpdateLabel();
        _timer.Start();
    }

    private void UpdateLabel() => Dispatcher.Invoke(() => _label.Content = _cache.Get<string>("message"));
}

public class InputWindow : Window
{
    private readonly IApplicationCache _cache;
    private readonly TextBox _textBox;
    
    public InputWindow(IApplicationCache cache)
    {
        _cache = cache;
        Title = "Hello, World!";
        Width = 800;
        Height = 600;
        
        _textBox = new TextBox();
        
        var button = new Button
        {
            Content = "Set message"
        };
        
        button.Click += (sender, args) => _cache.Set("message", _textBox.Text);
        
        Content = new StackPanel
        {
            Children =
            {
                _textBox,
                button
            }
        };
    }
}