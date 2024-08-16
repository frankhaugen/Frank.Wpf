using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace Frank.Wpf.Controls.RoslynScript;

public class ScriptRunner : ScriptRunnerBase
{
    internal ScriptRunner(ScriptOptions scriptOptions, Dictionary<string, object> globals)
        : base(scriptOptions, globals) { }

    public object? Run(string code)
        => RunInternalAsync(code, false).GetAwaiter().GetResult();

    public Task<object?> RunAsync(string code)
        => RunInternalAsync(code, true);

    private async Task<object?> RunInternalAsync(string code, bool isAsync)
    {
        var script = CSharpScript.Create(code, ScriptOptions, globalsType: typeof(ScriptGlobals));
        var state = isAsync
            ? await script.RunAsync(new ScriptGlobals(Globals))
            : script.RunAsync(new ScriptGlobals(Globals)).GetAwaiter().GetResult();

        return state?.ReturnValue;
    }

    public override Type GetExpectedOutputType()
    {
        return typeof(object); // Non-generic returns object
    }
}

public class ScriptRunner<T> : ScriptRunnerBase
{
    internal ScriptRunner(ScriptOptions scriptOptions, Dictionary<string, object> globals)
        : base(scriptOptions, globals) { }

    public T? Run(string code)
        => RunInternalAsync(code, false).GetAwaiter().GetResult();

    public Task<T?> RunAsync(string code)
        => RunInternalAsync(code, true);

    private async Task<T?> RunInternalAsync(string code, bool isAsync)
    {
        var script = CSharpScript.Create<T>(code, ScriptOptions, globalsType: typeof(ScriptGlobals));
        var state = isAsync
            ? await script.RunAsync(new ScriptGlobals(Globals))
            : script.RunAsync(new ScriptGlobals(Globals)).GetAwaiter().GetResult();

        var returnValue = state.ReturnValue;
        return returnValue == null ? default : (T)returnValue;
    }

    public override Type GetExpectedOutputType()
    {
        return typeof(T); // Generic returns the type T
    }
}