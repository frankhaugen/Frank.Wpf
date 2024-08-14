using System.IO;

namespace Frank.Wpf.Core;

public interface ISingleFileDialogResult : IDialogResult
{
    FileInfo? FileInfo { get; }
}