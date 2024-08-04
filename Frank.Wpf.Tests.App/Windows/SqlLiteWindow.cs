using Frank.Wpf.Windows.SqlRunner;

namespace Frank.Wpf.Tests.App.Windows;

public class SqlLiteWindow : SqlLiteRunnerWindow
{
    /// <inheritdoc />
    protected override string ConnectionString { get; } = "Data Source=:memory:";
}