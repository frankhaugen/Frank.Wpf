namespace Frank.Wpf.Core;

public class FilterBuilder
{
    private readonly List<string> _filterParts = new List<string>();

    public FilterBuilder AddFilter(string description, string pattern)
    {
        _filterParts.Add($"{description}|{pattern}");
        return this;
    }

    public string Build()
    {
        return string.Join("|", _filterParts);
    }
}