using System.Net.Http;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.HttpMessage;

public class HttpResponseMessageControl : ContentControl
{
    private HttpResponseMessage? _httpResponseMessage;
    private readonly TextBlock _textBlock;

    public HttpResponseMessageControl()
    {
        _textBlock = new TextBlock();
        
        Content = _textBlock;
    }

    public HttpResponseMessage? HttpResponseMessage
    {
        get => _httpResponseMessage;
        set
        {
            _httpResponseMessage = value;
            Render();
        }
    }

    private void Render()
    {
        Content = new TextBlock
        {
            Text = _httpResponseMessage?.ToString()
        };
    }
}