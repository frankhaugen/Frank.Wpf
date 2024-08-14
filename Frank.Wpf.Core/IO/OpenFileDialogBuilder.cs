using System.IO;

namespace Frank.Wpf.Core;

public class OpenFileDialogBuilder : FileDialogBuilderBase, ISingleFileDialogResult, IMultipleFileDialogResult
{
    private readonly OpenFileDialog _dialog;

    public OpenFileDialogBuilder() : base(new OpenFileDialog())
    {
        _dialog = (OpenFileDialog)_dialog;
    }

    public FileInfo? FileInfo => _dialog.FileNames.Length > 0 ? new FileInfo(_dialog.FileNames[0]) : null;

    public IEnumerable<FileInfo> FileInfos => _dialog.FileNames.Select(fileName => new FileInfo(fileName));

    public override DialogResult ShowDialog()
    {
        return _dialog.ShowDialog();
    }
}