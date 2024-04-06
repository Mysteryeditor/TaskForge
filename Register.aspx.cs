using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Configuration;

namespace TaskManagement
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Your Page_Load code here
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Validate the form controls
            if (Page.IsValid)
            {
                string action = "Insert"; // or "Update" or "Delete"
                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                string email = txtEmail.Text;
                string password = txtPassword.Text;
                int age = Convert.ToInt32(txtAge.Text);
                string securityQuestion = txtSecurityQuestion.Text;
                string securityAnswer = txtSecurityAnswer.Text;

                try
                {
                    // Create a DatabaseHelper instance with your connection string
                    DatabaseHelper dbHelper = new DatabaseHelper();

                    // Create a SQL command
                    SqlCommand command = new SqlCommand("ManageUser");
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Action", action);
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", EncryptPassword(password));
                    command.Parameters.AddWithValue("@Age", age);
                    command.Parameters.AddWithValue("@SecurityQuestion", securityQuestion);
                    command.Parameters.AddWithValue("@SecurityAnswer", securityAnswer);

                    // Execute the command using the helper method
                    dbHelper.ExecuteNonQuery(command);

                    // Registration successful, display a success message or redirect to another page
                    Response.Write("<script>alert('Registration successful!');</script>");
                    Response.Redirect("Login.aspx");
                }
                catch (Exception ex)
                {
                    // Handle the exception
                    Response.Write("<script>alert('An error occurred: " + ex.Message + "');</script>");
                }
            }
        }



        private string EncryptPassword(string password)
        {
            
            string key = "tR!@l_5tR1n9";
            char[] passwordChars = password.ToCharArray();
            // Encrypt each character of the password using XOR operation with the key
            for (int i = 0; i < passwordChars.Length; i++)
            {
                passwordChars[i] = (char)(passwordChars[i] ^ key[i % key.Length]);
            }
            return new string(passwordChars);
        }

       

    }
}
