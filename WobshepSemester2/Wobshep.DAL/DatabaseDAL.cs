using System.Data;
using System.Data.SqlClient;
using Wobshep.DAL.Extensions;
using Wobshep.Interfaces.Interfaces;

namespace Wobshep.DAL
{
    public class DatabaseDAL : IDatabase
    {
        private readonly SqlConnection _connection;

        public DatabaseDAL()
        {
            _connection = new SqlConnection(ConnectionStringHelper.ConnectionString);
        }

        public DataTable Query(SqlCommand query)
        {
            query.Connection = _connection;
            var table = new DataTable();
            var adapter = new SqlDataAdapter(query);

            _connection.Open();
            adapter.Fill(table);
            _connection.Close();
            query.Dispose();

            return table;
        }

        public void ExecuteQuery(SqlCommand query)
        {
            query.Connection = _connection;

            _connection.Open();
            query.ExecuteNonQuery();
            _connection.Close();
            query.Dispose();
        }

        public int ExecuteNonQuery(SqlCommand query)
        {
            query.Connection = _connection;

            _connection.Open();
            var affectedRows = query.ExecuteNonQuery();
            _connection.Close();
            query.Dispose();

            return affectedRows;
        }
    }
}