using System.IO;

namespace Frank.Wpf.Core;

public interface IMultipleFileDialogResult : IDialogResult
{
    IEnumerable<FileInfo> FileInfos { get; }
}