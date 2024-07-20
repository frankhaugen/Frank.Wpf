using System.IO;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.PdfViewer;

/// <summary>
/// A PDF viewer control that displays a PDF file.
/// </summary>
public class PdfViewer : GroupBox
{
    private readonly WebBrowser _content = new();

    public PdfViewer(string header)
    {
        Header = header;
        base.Content = _content;
    }
    
    public void SetContent(FileInfo pdfFile) => _content.Source = new Uri(pdfFile.FullName);

    public void SetContent(Memory<byte> pdfFile)
    {
        var tempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".pdf");
        File.WriteAllBytes(tempFile, pdfFile.ToArray());
        _content.Source = new Uri(tempFile);
    }
    
    public Memory<byte> GetContent() => new(File.ReadAllBytes(new Uri(_content.Source.OriginalString).LocalPath));

    public PdfViewer(string header, FileInfo pdfFile)
    {
        Header = header;
        _content.Source = new Uri(pdfFile.FullName);
        base.Content = _content;
    }

    public new FileInfo Content
    {
        get => new(_content.Source.OriginalString);
        set => _content.Source = new Uri(value.FullName);
    }
}