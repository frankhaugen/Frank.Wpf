namespace Frank.Wpf.Controls.RoslynScript;

public class ScriptGlobals
{
    private readonly Dictionary<string, object> _globals;

    public ScriptGlobals(Dictionary<string, object> globals)
    {
        _globals = globals;
    }

    public object? this[string name] => _globals.ContainsKey(name) ? _globals[name] : null;
}