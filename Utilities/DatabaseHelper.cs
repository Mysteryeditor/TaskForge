using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

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
        try
        {
            using (SqlConnection connection = GetConnection())
            {
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
            // Display a success alert message
            HttpContext.Current.Response.Write("<script>alert('Operation completed successfully.');</script>");
        }
        catch (Exception ex)
        {
            LogError(ex);
            // Display an error alert message
            HttpContext.Current.Response.Write("<script>alert('An error occurred. Please try again later.');</script>");
        }
    }

    public DataTable ExecuteQuery(SqlCommand command)
    {
        DataTable dataTable = new DataTable();
        try
        {
            using (SqlConnection connection = GetConnection())
            {
                command.Connection = connection;
                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }
            // Display a success alert message
            HttpContext.Current.Response.Write("<script>alert('Operation completed successfully.');</script>");
        }
        catch (Exception ex)
        {
            LogError(ex);
            // Display an error alert message
            HttpContext.Current.Response.Write("<script>alert('An error occurred. Please try again later.');</script>");
        }
        return dataTable;
    }

    public object ExecuteScalar(SqlCommand command)
    {
        try
        {
            using (SqlConnection connection = GetConnection())
            {
                command.Connection = connection;
                connection.Open();
                object result = command.ExecuteScalar();
                // Display a success alert message
                HttpContext.Current.Response.Write("<script>alert('Operation completed successfully.');</script>");
                return result;
            }
        }
        catch (Exception ex)
        {
            LogError(ex);
            // Display an error alert message
            HttpContext.Current.Response.Write("<script>alert('An error occurred. Please try again later.');</script>");
            return null;
        }
    }

    private void LogError(Exception ex)
    {
        // Log the error details into the ErrorLogs table
        string query = @"INSERT INTO ErrorLogs (ErrorTime, ErrorMessage, StackTrace, Source, TargetSite) 
                         VALUES (@ErrorTime, @ErrorMessage, @StackTrace, @Source, @TargetSite)";
        using (SqlConnection connection = GetConnection())
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ErrorTime", DateTime.Now);
                command.Parameters.AddWithValue("@ErrorMessage", ex.Message);
                command.Parameters.AddWithValue("@StackTrace", ex.StackTrace);
                command.Parameters.AddWithValue("@Source", ex.Source);
                command.Parameters.AddWithValue("@TargetSite", ex.TargetSite.ToString());
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
