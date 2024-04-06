using System;

namespace TaskManagement
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the alert has been shown before
                if (Session["NotificationShown"] == null)
                {
                    // Display the notification alert
                    string script = "alert('Welcome to the Dashboard!');";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "NotificationScript", script, true);

                    // Set a session variable to indicate that the alert has been shown
                    Session["NotificationShown"] = true;
                }

                // Retrieve the username from the session
                string userName = Session["userName"] as string;
                if (userName != null)
                {
                    userLabel.Text = "Welcome, " + userName;
                }
                else
                {
                    userLabel.Text = "Welcome, Guest";
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
    }
}
