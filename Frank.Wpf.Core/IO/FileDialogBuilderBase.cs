using System.IO;

namespace Frank.Wpf.Core;

public abstract class FileDialogBuilderBase
{
    protected readonly FileDialog _dialog;

    protected FileDialogBuilderBase(FileDialog dialog)
    {
        _dialog = dialog;
    }

    public FileDialogBuilderBase SetDefaultExtension(string defaultExtension)
    {
        _dialog.DefaultExt = defaultExtension;
        return this;
    }

    public FileDialogBuilderBase SetFilter(string filter)
    {
        _dialog.Filter = filter;
        return this;
    }

    public FileDialogBuilderBase SetInitialDirectory(DirectoryInfo initialDirectory)
    {
        _dialog.InitialDirectory = initialDirectory.FullName;
        return this;
    }

    public abstract DialogResult ShowDialog();
}