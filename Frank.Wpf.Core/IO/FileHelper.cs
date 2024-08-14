using System.IO;

namespace Frank.Wpf.Core;

using System;

public class FileHelper
{
    public ISingleFileDialogResult CreateSaveFileDialog()
    {
        return new SaveFileDialogBuilder();
    }

    public ISingleFileDialogResult CreateOpenFileDialog()
    {
        return new OpenFileDialogBuilder();
    }

    public IMultipleFileDialogResult CreateOpenMultipleFileDialog()
    {
        return new OpenFileDialogBuilder();
    }
}