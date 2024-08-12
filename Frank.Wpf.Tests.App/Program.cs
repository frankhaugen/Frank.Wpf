using Frank.Wpf.Hosting;

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