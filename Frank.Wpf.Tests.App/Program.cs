using System.Data;
using System.Windows;
using System.Windows.Controls;
using Frank.Wpf.Controls.Code;
using Frank.Wpf.Controls.DataTableGrid;
using Frank.Wpf.Hosting;
using ICSharpCode.AvalonEdit.Highlighting;
using Microsoft.Data.Sqlite;

namespace Frank.Wpf.Tests.App;

public static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var builder = Host.CreateWpfHostBuilder();

        var host = builder.Build<DefaultWindow>();
        
        host.Run();
    }
}