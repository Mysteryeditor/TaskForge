using System;
using System.Web.Optimization;
using System.Web.UI;

namespace TaskManagement
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the username is stored in the session
                if (Session["UserName"] != null)
                {
                    string userName = Session["UserName"].ToString();
                    lblUserName.Text = "Welcome, " + userName;
                }
                else
                {
                    // Redirect to login page if username is not found in session
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Clear session and redirect to login page
            Session.Clear();
            string script = "alert('Logout successful!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LogoutAlert", script, true);

            Response.Redirect("~/Login.aspx");
        }
    
    }
}
