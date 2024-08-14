using System.Windows.Controls;

namespace Frank.Wpf.Controls.SimpleInputs;

public abstract class BaseControl : UserControl
{
    private readonly GroupBox _groupBox = new();
    private object? _innerContent;
    private bool _isGroupBoxVisible;

    public string Header
    {
        get => _groupBox.Header as string ?? string.Empty;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                HideGroupBox();
            }
            else
            {
                ShowGroupBoxWithHeader(value);
            }
        }
    }

    public new object? Content
    {
        get => _isGroupBoxVisible ? _groupBox.Content : _innerContent;
        set
        {
            if (value is GroupBox groupBox)
            {
                SetGroupBoxContent(groupBox.Content);
            }
            else
            {
                SetInnerContent(value);
            }
        }
    }

    private void HideGroupBox()
    {
        _groupBox.Header = string.Empty;
        _isGroupBoxVisible = false;
        base.Content = _innerContent;
    }

    private void ShowGroupBoxWithHeader(string header)
    {
        _groupBox.Header = header;
        _isGroupBoxVisible = true;

        
        _groupBox.Content = _innerContent ?? _groupBox.Content;
        if (_groupBox.Content != _innerContent)
        {
            _groupBox.Content = _innerContent;
        }
        _groupBox.UpdateLayout();
        
        if (_groupBox.Content != _innerContent)
        {
            _groupBox.Content = _innerContent;
        }
        
        base.Content = _groupBox;
    }

    private void SetGroupBoxContent(object? content)
    {
        _groupBox.Content = content;
        _isGroupBoxVisible = true;
        base.Content = _groupBox;
    }

    private void SetInnerContent(object? value)
    {
        _innerContent = value;

        if (_isGroupBoxVisible)
        {
            _groupBox.Content = _innerContent;
            base.Content = _groupBox;
        }
        else
        {
            base.Content = _innerContent;
        }
    }
}
