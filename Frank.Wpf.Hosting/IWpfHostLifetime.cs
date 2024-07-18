namespace Frank.Wpf.Hosting;

public interface IWpfHostLifetime
{
    /// <summary>
    /// Gets the cancellation token to monitor for shutdown requests.
    /// </summary>
    /// <returns></returns>
    CancellationToken GetCancellationToken();
    
    /// <summary>
    /// Stops the application.
    /// </summary>
    /// <remarks>WARNING: This method is not thread-safe.</remarks>
    void StopApplication();
}