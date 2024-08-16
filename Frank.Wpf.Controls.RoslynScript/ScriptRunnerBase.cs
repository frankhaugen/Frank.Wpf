using System.Text;
using Microsoft.CodeAnalysis.Scripting;

namespace Frank.Wpf.Controls.RoslynScript;

public abstract class ScriptRunnerBase
{
    protected ScriptOptions ScriptOptions { get; }
    protected Dictionary<string, object> Globals { get; }

    protected ScriptRunnerBase(ScriptOptions scriptOptions, Dictionary<string, object> globals)
    {
        ScriptOptions = scriptOptions;
        Globals = globals;
    }

    public IEnumerable<string> GetAvailableNamespaces()
    {
        var namespaces = new HashSet<string>();
        foreach (var import in ScriptOptions.Imports)
        {
            var parts = import.Split(' ');
            if (parts.Length == 1)
            {
                namespaces.Add(parts[0]);
            }
            else if (parts.Length == 2 && parts[0] == "using")
            {
                namespaces.Add(parts[1]);
            }
        }
        
        return namespaces;
    }

    public string GetInputParameters()
    {
        var parameters = Globals.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.GetType());
        
        var stringBuilder = new StringBuilder();
        
        foreach (var parameter in parameters)
        {
            stringBuilder.Append($"{parameter.Value.Name} {parameter.Key}, ");
        }
        
        if (stringBuilder.Length > 0)
        {
            stringBuilder.Length -= 2;
        }
        
        return stringBuilder.ToString();
    }

    public abstract Type GetExpectedOutputType();
}