using System.Net.Http;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.HttpMessageRenderer;

public class HttpRequestMessageControl : ContentControl
{
    private HttpRequestMessage _httpRequestMessage;

    public HttpRequestMessageControl(HttpRequestMessage httpRequestMessage)
    {
        _httpRequestMessage = httpRequestMessage;
        Render();
    }
    
    public void Update(HttpRequestMessage httpRequestMessage)
    {
        _httpRequestMessage = httpRequestMessage;
        Render();
    }
    
    public void Render()
    {
        Content = new TextBlock
        {
            Text = _httpRequestMessage.ToString()
        };
    }
}