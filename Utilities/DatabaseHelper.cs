using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class DatabaseHelper
{
    private string connectionString;

    public DatabaseHelper()
    {
        this.connectionString = ConfigurationManager.ConnectionStrings["taskConnectionString"].ConnectionString;
    }

    public SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }

    public void ExecuteNonQuery(SqlCommand command)
    {
        using (SqlConnection connection = GetConnection())
        {
            command.Connection = connection;
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public DataTable ExecuteQuery(SqlCommand command)
    {
        DataTable dataTable = new DataTable();
        using (SqlConnection connection = GetConnection())
        {
            command.Connection = connection;
            connection.Open();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }
        }
        return dataTable;
    }

    public object ExecuteScalar(SqlCommand command)
    {
        using (SqlConnection connection = GetConnection())
        {
            command.Connection = connection;
            connection.Open();
            return command.ExecuteScalar();
        }
    }
}
