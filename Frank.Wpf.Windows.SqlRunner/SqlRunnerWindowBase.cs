using System.Data;
using System.Windows;
using System.Windows.Controls;
using Frank.Wpf.Controls.Code;
using Frank.Wpf.Controls.DataTableGrid;
using ICSharpCode.AvalonEdit.Highlighting;

namespace Frank.Wpf.Windows.SqlRunner;

public abstract class SqlRunnerWindowBase : Window
{
    protected readonly StackPanel _stackPanel = new();
    protected readonly CodeArea _codeArea = new(HighlightingManager.Instance.GetDefinition("TSQL"));
    protected readonly Button _runSqlQueryButton = new();
    protected readonly Button _runSqlNonQueryButton = new();
    protected readonly GroupBox _outputWrapper = new();
    
    protected SqlRunnerWindowBase()
    {
        _runSqlQueryButton.Content = "Run SQL Query";
        _runSqlQueryButton.Click += async (sender, args) => await RunButton_Click(sender, args);

        _runSqlNonQueryButton.Content = "Run SQL Non Query";
        _runSqlNonQueryButton.Click += async (sender, args) => await RunButton_Click(sender, args);

        _outputWrapper.Header = "Output";

        _stackPanel.Children.Add(_codeArea);
        _stackPanel.Children.Add(new StackPanel() { Orientation = Orientation.Horizontal, Children = { _runSqlQueryButton, _runSqlNonQueryButton } });
        _stackPanel.Children.Add(_outputWrapper);

        Content = _stackPanel;
    }
    
    
    private async Task RunButton_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var code = _codeArea.Text.Trim();

        using var connection = CreateConnection();
        using var command = CreateCommand(code, connection);

        connection.Open();

        if (button == _runSqlQueryButton)
        {
            using var reader = command.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(reader);

            var dataTableGrid = new DataTableGrid(dataTable);
            _outputWrapper.Content = dataTableGrid;
        }
        else if (button == _runSqlNonQueryButton)
        {
            var results = command.ExecuteNonQuery();
            _outputWrapper.Content = new TextBlock() { Text = $"Rows affected: {results}" };
        }
        
        connection.Close();
        
        await Task.CompletedTask;
    }
    
    protected abstract string ConnectionString { get; }

    protected abstract IDbConnection CreateConnection();
    protected abstract IDbCommand CreateCommand(string commandText, IDbConnection connection);
}