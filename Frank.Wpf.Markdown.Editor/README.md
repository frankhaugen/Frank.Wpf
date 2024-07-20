# Frank.Wpf.Hosting

Frank.Wpf.Hosting is a library that allows you to host WPF without XAML and using Dependency Injection.

___
- [Frank.Wpf.Hosting](#frankwpfhosting)
  - [Features](#features)
  - [Installation](#installation)
  - [Usage](#usage)+
  - 
  - [Advanced usage](#advanced-usage)
___

[![NuGet](https://img.shields.io/nuget/v/Frank.Wpf.Hosting.svg)](https://www.nuget.org/packages/Frank.Wpf.Hosting)
[![NuGet](https://img.shields.io/nuget/dt/Frank.Wpf.Hosting.svg)](https://www.nuget.org/packages/Frank.Wpf.Hosting)

___

## Features

- Dependency Injection
- No XAML
- Multiple windows

## Installation

```bash
dotnet add package Frank.Wpf.Hosting
```

## Usage

```csharp
using Frank.Wpf.Hosting;

public static class Program
{
    [STAThread] // Required for WPF
    public static void Main()
    {
        var builder = Host.CreateWpfHostBuilder(); // Create a WPF host builder
        builder.Services.AddTransient<IMyService, MyService>(); // Register services
        var host = builder.Build<MyWindow>(); // Create the host with the window inheriting from MainWindow
        host.Run(); // Start the application and show the window
    }
}
```

To get this point you need to create a window that inherits from `MainWindow`:
```csharp
using Frank.Wpf.Hosting;

public class MyWindow : MainWindow
{
    public MyWindow(IMyService myService)
    {
        // Use the service
    }
}
```

## Advanced usage

If you want to have multiple windows, and not just dialogs, you can use the `IWindowFactory` interface:
```csharp
using Frank.Wpf.Hosting;

public class MyService : IMyService
{
    private readonly IWindowFactory _windowFactory;

    public MyService(IWindowFactory windowFactory)
    {
        _windowFactory = windowFactory;
    }

    public void DoSomething()
    {
        _windowFactory.CreateWindow<OtherWindow>(true); // Create a new window and show it (true)
    }
}
```

You can also get the window by name using the `IWindowFactory` interface:
```csharp
using Frank.Wpf.Hosting;

public class MyService : IMyService
{
    private readonly IWindowFactory _windowFactory;

    public MyService(IWindowFactory windowFactory)
    {
        _windowFactory = windowFactory;
    }

    public void DoSomething()
    {
        _windowFactory.CreateWindow("DisplayWindow", true); // Create a new window and show it (true)
    }
}
```

__Note:__ The window name is the actual property name of the window, not the class name.

If you want to share data between windows, you can use the `IApplicationCache` interface, and access it using the 
`Dispatcher` directly when needed:
```csharp
using Frank.Wpf.Hosting;

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

    private void UpdateLabel() => Dispatcher.Invoke(() => _label.Content = _cache.Get<string>("message")); // Access the cache using the Dispatcher because the timer runs on a different thread
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
```