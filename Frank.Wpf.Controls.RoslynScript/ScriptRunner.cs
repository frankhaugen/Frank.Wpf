using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace Frank.Wpf.Controls.RoslynScript;

public class ScriptRunner
{
    private readonly ScriptOptions _scriptOptions;

    public ScriptRunner()
    {
        _scriptOptions = ScriptOptions.Default
            .AddReferences(typeof(string).Assembly)
            .AddImports("System");
    }

    public async Task<T?> RoslynScriptingAsync<T>(string code, params Assembly[] assemblies)
    {
        _scriptOptions.AddReferences(assemblies);

        var script = CSharpScript.Create<T>(code, _scriptOptions);

        var state = await script.RunAsync();

        var result = state.ReturnValue;

        return result;
    }
}