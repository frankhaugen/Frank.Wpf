namespace Frank.Wpf.Core.Beatification;

public abstract class TextBeautifierBase : ITextBeautifier
{
    public abstract string Beautify(string text);

    protected string IndentLines(string text, int indentLevel = 4)
    {
        var lines = text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = new string(' ', indentLevel) + lines[i].Trim();
        }
        return string.Join("\n", lines);
    }
}