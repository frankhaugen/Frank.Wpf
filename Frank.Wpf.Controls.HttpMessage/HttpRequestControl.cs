using System.Net.Http;
using System.Windows.Controls;
using Frank.Http.Abstractions;

namespace Frank.Wpf.Controls.HttpMessage;

public class HttpRequestControl : UserControl
{
    private readonly IRestClient _restClient;
    private HttpRequestMessageControl? _httpRequestMessageControl;
    private HttpResponseMessageControl? _httpResponseMessageControl;

    public HttpRequestControl(IRestClient restClient)
    {
        _restClient = restClient;
    }
    
    public required HttpRequestMessage HttpRequestMessage
    {
        get => _httpRequestMessageControl!.HttpRequestMessage;
        set
        {
            if (_httpRequestMessageControl == null)
            {
                _httpRequestMessageControl = new HttpRequestMessageControl();
                Content = _httpRequestMessageControl;
            }
            _httpRequestMessageControl.HttpRequestMessage = value;
        }
    }
    
    public HttpResponseMessage? HttpResponseMessage
    {
        get => _httpResponseMessageControl?.HttpResponseMessage;
        private set
        {
            if (_httpResponseMessageControl == null)
            {
                _httpResponseMessageControl = new HttpResponseMessageControl();
                Content = _httpResponseMessageControl;
            }
            _httpResponseMessageControl.HttpResponseMessage = value;
        }
    }
    
    public async Task SendAsync(CancellationToken cancellationToken = default) => HttpResponseMessage = await _restClient.SendAsync(HttpRequestMessage, cancellationToken);
}