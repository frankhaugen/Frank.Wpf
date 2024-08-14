using System.Net.Http;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.HttpMessage;

public class HttpRequestMessageControl : UserControl
{
    private HttpRequestMessage _httpRequestMessage;
    private TextBlock _textBlock;
    
    public HttpRequestMessageControl()
    {
        _textBlock = new TextBlock();
        Content = _textBlock;
    }

    public HttpRequestMessage HttpRequestMessage
    {
        get => _httpRequestMessage;
        set
        {
            _httpRequestMessage = value;
            _textBlock.Text = _httpRequestMessage.ToString();
        }
    }
}