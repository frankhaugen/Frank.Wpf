using System.Data;
using Microsoft.Data.SqlClient;

namespace Frank.Wpf.Windows.SqlRunner;

public abstract class SqlServerRunnerWindow : SqlRunnerWindowBase
{
    protected override IDbConnection CreateConnection()
    {
        // Use your actual SQL Server connection string
        return new SqlConnection(ConnectionString);
    }

    protected override IDbCommand CreateCommand(string commandText, IDbConnection connection)
    {
        return new SqlCommand(commandText, (SqlConnection)connection);
    }
}