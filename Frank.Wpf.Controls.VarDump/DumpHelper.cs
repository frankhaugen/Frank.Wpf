using System.Reflection;
using System.Windows;
using Frank.Reflection.Dump;
using VarDump.Visitor;

namespace Frank.Wpf.Controls.VarDump;

public class DumpHelper
{
    private static DumpOptions? _options = null;
    // private static DumpOptions _options = new()
    // {
    //     DateKind = DateKind.ConvertToUtc,
    //     DateTimeInstantiation = DateTimeInstantiation.New,
    //     // Descriptors = null,
    //     // ExcludeTypes = null,
    //     // GenerateVariableInitializer = false,
    //     GetPropertiesBindingFlags = BindingFlags.Default,
    //     // GetFieldsBindingFlags = null,
    //     // IgnoreDefaultValues = false,
    //     IgnoreNullValues = true,
    //     // MaxCollectionSize = 0,
    //     MaxDepth = 32,
    //     // SortDirection = null,
    //     // UseNamedArgumentsForReferenceRecordTypes = false,
    //     UseTypeFullName = false,
    //     // WritablePropertiesOnly = false
    // };

    public static string DumpEnumerable<T>(IEnumerable<T> enumerable, Func<T, string> idSelector) => enumerable.DumpEnumerable(idSelector, _options);
    public static string DumpVar<T>(T obj) => obj.DumpVar(_options);
    public static string DumpClass<T>(T obj) => obj.DumpClass(_options);

    public static void DumpEnumerableToClipboard<T>(IEnumerable<T> enumerable, Func<T, string> idSelector) => Clipboard.SetText(enumerable.DumpEnumerable(idSelector, _options));
    public static void DumpVarToClipboard<T>(T obj) => Clipboard.SetText(obj.DumpVar(_options));
    public static void DumpClassToClipboard<T>(T obj) => Clipboard.SetText(obj.DumpClass(_options));
}