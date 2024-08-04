namespace Frank.Wpf.Controls.Console;

public class ConsoleControlWriter
{
    private readonly System.Threading.Channels.ChannelWriter<string> _writer;

    internal ConsoleControlWriter(System.Threading.Channels.ChannelWriter<string> writer)
    {
        _writer = writer;
    }

    public async Task WriteAsync(string message)
    {
        await _writer.WriteAsync(message);
    }
}