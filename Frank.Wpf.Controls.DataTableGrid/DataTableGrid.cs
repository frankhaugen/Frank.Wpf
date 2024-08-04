using System.Data;
using System.Windows.Controls;

namespace Frank.Wpf.Controls.DataTableGrid;

public class DataTableGrid : DataGrid
{
    public DataTableGrid(DataTable dataTable)
    {
        DataTable = dataTable;
        AutoGenerateColumns = true;
        CanUserAddRows = false;
        CanUserDeleteRows = false;
        CanUserReorderColumns = false;
        CanUserResizeColumns = true;
        CanUserResizeRows = true;
        CanUserSortColumns = true;
        IsReadOnly = true;
        
        ItemsSource = DataTable.DefaultView;
    }
    
    public DataTable DataTable { get; set; }
    
    public void Refresh()
    {
        ItemsSource = DataTable.DefaultView;
    }
}