using System.Data;
using System.Data.SqlClient;

namespace Wobshep.Interfaces.Interfaces
{
    public interface IDatabase
    {
        DataTable Query(SqlCommand query);
        void ExecuteQuery(SqlCommand query);
        int ExecuteNonQuery(SqlCommand query);
    }
}