using System.IO;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.PdfViewer;

/// <summary>
/// A PDF viewer control that displays a PDF file.
/// </summary>
public class PdfViewer : UserControl
{
    private readonly WebBrowser _content = new();
    private readonly GroupBox _groupBox = new();
    
    private Memory<byte>? _pdfFileContent;
    private FileInfo? _pdfFile;

    public PdfViewer()
    {
        _groupBox.Header = "PDF Viewer";
        Content = _groupBox;
    }
    
    public FileInfo? LocalPdfFile
    {
        get => _pdfFile;
        set
        {
            _pdfFile = value;
            if (value is null)
            {
                _pdfFileContent = null;
                _content.Navigate("about:blank");
                return;
            }

            if (_pdfFileContent == null || !value.Exists)
            {
                _pdfFileContent = null;
                _content.Navigate("about:blank");
                return;
            }

            _groupBox.Header = "PDF Viewer - " + value.Name;
            _pdfFileContent = File.ReadAllBytes(value.FullName);
            _content.NavigateToStream(new MemoryStream(_pdfFileContent.Value.ToArray()));
        }
    }
    
    public Memory<byte>? PdfFileContent
    {
        get => _pdfFileContent;
        set
        {
            _pdfFileContent = value;
            _groupBox.Header = "PDF Viewer";
            if (value is null)
            {
                _pdfFile = null;
                _content.Navigate("about:blank");
                return;
            }

            _content.NavigateToStream(new MemoryStream(value.Value.ToArray()));
        }
    }
}