using System.IO;

namespace Frank.Wpf.Core;

public class SaveFileDialogBuilder : FileDialogBuilderBase, ISingleFileDialogResult
{
    private new readonly SaveFileDialog _dialog;

    public SaveFileDialogBuilder() : base(new SaveFileDialog())
    {
        _dialog = (SaveFileDialog)base._dialog;
    }

    public FileInfo? FileInfo => _dialog.FileName != null ? new FileInfo(_dialog.FileName) : null;

    public override DialogResult ShowDialog()
    {
        return _dialog.ShowDialog();
    }
}