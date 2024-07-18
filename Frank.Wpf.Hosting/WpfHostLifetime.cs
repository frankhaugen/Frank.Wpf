namespace Frank.Wpf.Hosting;

public class WpfHostLifetime : IWpfHostLifetime
{
    private readonly CancellationTokenSource _cts = new();

    /// <inheritdoc />
    public CancellationToken GetCancellationToken() => _cts.Token;

    /// <inheritdoc />
    public void StopApplication()
    {
        _cts.Cancel();
    }
}