using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace TaskManagement
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Your Page_Load code here
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Validate the form controls
            if (Page.IsValid)
            {
                string email = txtEmail.Text;
                string encryptedPassword = txtPassword.Text; // Encrypt the entered password for comparison

                try
                {
                    // Retrieve the user's encrypted password from the database based on the provided email
                    string encryptedPasswordFromDatabase = RetrieveEncryptedPasswordFromDatabase(email);

                    if (encryptedPasswordFromDatabase != null && DecryptPassword(encryptedPasswordFromDatabase) == txtPassword.Text)
                    {
                        // Passwords match, login successful
                        string userName = RetrieveUserNameFromDatabase(email);
                        if (userName != null)
                        {
                            Session["UserName"] = userName;
                            Response.Redirect("Dashboard.aspx");
                        }
                        //Response.Redirect("Dashboard.aspx", false);
                        //HttpContext.Current.ApplicationInstance.CompleteRequest();

                    }
                    else
                    {
                        // Passwords don't match, display an error message
                        Response.Write("<script>alert('Incorrect email or password. Please try again.');</script>");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions
                    Response.Write("<script>alert('An error occurred: " + ex.Message + "');</script>");
                }
            }
        }

        private string RetrieveEncryptedPasswordFromDatabase(string email)
        {
            string query = "SELECT Password FROM UserTable WHERE Email = @Email";
            using (SqlConnection connection = new DatabaseHelper().GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                connection.Open();
                return new DatabaseHelper().ExecuteScalar(command) as string;
            }
        }

        private string DecryptPassword(string encryptedPassword)
        {
            string key = "tR!@l_5tR1n9";
            char[] encryptedChars = encryptedPassword.ToCharArray();
            for (int i = 0; i < encryptedChars.Length; i++)
            {
                encryptedChars[i] = (char)(encryptedChars[i] ^ key[i % key.Length]);
            }
            return new string(encryptedChars);
        }

        public string RetrieveUserNameFromDatabase(string email)
        {
            string userName = null;

            // Your database query to retrieve the user's name based on email
            string query = $"SELECT FirstName FROM UserTable WHERE Email = '{email}'";

            using (SqlConnection connection = new DatabaseHelper().GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
             

                    object result = new DatabaseHelper().ExecuteScalar(command);

                    if (result != null)
                    {
                        userName = result.ToString();
                    }
                }
            }

            return userName;
        }

    }
}
