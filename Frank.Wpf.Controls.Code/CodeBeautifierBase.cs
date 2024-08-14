namespace Frank.Wpf.Controls.Code;

public abstract class CodeBeautifierBase : ICodeBeautifier
{
    public abstract string Beautify(string code);

    protected string IndentLines(string code, int indentLevel = 4)
    {
        var lines = code.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = new string(' ', indentLevel) + lines[i].Trim();
        }
        return string.Join("\n", lines);
    }
}