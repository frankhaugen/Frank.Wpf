using System.Net.Http;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.HttpMessageRenderer;

public class HttpResponseMessageControl : ContentControl
{
    private HttpResponseMessage _httpResponseMessage;

    public HttpResponseMessageControl(HttpResponseMessage httpResponseMessage)
    {
        _httpResponseMessage = httpResponseMessage;
        Render();
    }
    
    public void Update(HttpResponseMessage httpResponseMessage)
    {
        _httpResponseMessage = httpResponseMessage;
        Render();
    }
    
    public void Render()
    {
        Content = new TextBlock
        {
            Text = _httpResponseMessage.ToString()
        };
    }
}