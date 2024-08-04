using System.Data;
using Microsoft.Data.Sqlite;

namespace Frank.Wpf.Windows.SqlRunner;

public abstract class SqlLiteRunnerWindow : SqlRunnerWindowBase
{
    protected override IDbConnection CreateConnection()
    {
        return new SqliteConnection(ConnectionString);
    }

    protected override IDbCommand CreateCommand(string commandText, IDbConnection connection)
    {
        return new SqliteCommand(commandText, (SqliteConnection)connection);
    }
}