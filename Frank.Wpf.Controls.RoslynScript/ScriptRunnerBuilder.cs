using System.Reflection;
using Microsoft.CodeAnalysis.Scripting;

namespace Frank.Wpf.Controls.RoslynScript;

public class ScriptRunnerBuilder
{
    private readonly ScriptOptions _scriptOptions;
    private readonly Dictionary<string, object> _globals = new();

    public ScriptRunnerBuilder()
    {
        _scriptOptions = ScriptOptions.Default
            .AddReferences(typeof(string).Assembly)
            .AddImports("System")
            .AddImports("System.Collections.Generic")
            .AddImports("System.Text")
            .AddImports("System.Threading.Tasks")
            .AddImports("System.Text.RegularExpressions");
    }

    public ScriptRunnerBuilder WithReference(Assembly assembly)
    {
        _scriptOptions.AddReferences(assembly);
        return this;
    }

    public ScriptRunnerBuilder WithImport(string import)
    {
        _scriptOptions.AddImports(import);
        return this;
    }

    public ScriptRunnerBuilder WithInput(string name, object value)
    {
        _globals[name] = value;
        return this;
    }

    public ScriptRunner Build()
    {
        return new ScriptRunner(_scriptOptions, _globals);
    }

    public ScriptRunner<T> Build<T>()
    {
        return new ScriptRunner<T>(_scriptOptions, _globals);
    }
}