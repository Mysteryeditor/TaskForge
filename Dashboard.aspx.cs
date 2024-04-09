using System;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TaskManagement
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userName = Session["userName"] as string;

            if (!IsPostBack)
            {
                // Check if the user is logged in
                if (userName != null)
                {
                    userLabel.Text = "Welcome, " + userName;
                    LoadTasks(userName);
                }
                else
                {
                    userLabel.Text = "Welcome, Guest";
                    Response.Redirect("~/Login.aspx");
                }
            }

            // Always load tasks on every page load (including postbacks)
            LoadTasks(userName);
        }


        protected void ChangeTaskStatus_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int taskId = Convert.ToInt32(button.CommandArgument);

            // Update task status in the database
            string query = "UPDATE Tasks SET IsCompleted = CASE WHEN IsCompleted = 1 THEN 0 ELSE 1 END WHERE Id = @TaskId";

            using (SqlConnection connection = new DatabaseHelper().GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TaskId", taskId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // Reload tasks after status change
            string userName = Session["userName"] as string;
            LoadTasks(userName);
        }

        private void LoadTasks(string userName)
        {
            string query = $"SELECT * FROM Tasks WHERE CreatedBy = (SELECT UserId FROM UserTable WHERE FirstName = @UserName)";

            using (SqlConnection connection = new DatabaseHelper().GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", userName);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        pendingTasks.Controls.Clear();
                        completedTasks.Controls.Clear();

                        while (reader.Read())
                        {
                            // Create task card and add it to the appropriate container
                            var card = new HtmlGenericControl("div");
                            card.Attributes["class"] = "card mb-3";

                            var cardHeader = new HtmlGenericControl("div");
                            cardHeader.Attributes["class"] = "card-header fw-bold";
                            cardHeader.InnerText = reader["TaskName"].ToString();
                            card.Controls.Add(cardHeader);

                            var cardBody = new HtmlGenericControl("div");
                            cardBody.Attributes["class"] = "card-body";

                            // Priority Label
                            var priorityLabel = new Label();
                            priorityLabel.Text = "Priority: ";
                            priorityLabel.CssClass = "fw-bold my-3";
                            cardBody.Controls.Add(priorityLabel);

                            var priorityValueLabel = new Label();
                            priorityValueLabel.Text = reader["Priority"].ToString() + "<br />";
                            cardBody.Controls.Add(priorityValueLabel);

                            // Description Label
                            var descriptionLabel = new Label();
                            descriptionLabel.Text = "Description: ";
                            descriptionLabel.CssClass = "fw-bold my-5";
                            cardBody.Controls.Add(descriptionLabel);

                            var descriptionValueLabel = new Label();
                            descriptionValueLabel.Text = reader["Description"].ToString() + "<br />";
                            cardBody.Controls.Add(descriptionValueLabel);

                            // Status Button (Mark as Complete/Incomplete)
                            var statusButton = new Button();
                            statusButton.ID = "statusButton_" + reader["Id"].ToString(); // Unique ID for each button
                            statusButton.Text = Convert.ToBoolean(reader["IsCompleted"]) ? "Mark as Incomplete" : "Mark as Complete";
                            statusButton.CommandArgument = reader["Id"].ToString();
                            statusButton.Command += ChangeTaskStatus_Click;
                            statusButton.CssClass = "btn btn-black-text";
                            cardBody.Controls.Add(statusButton);

                            // Delete Button
                            var deleteButton = new Button();
                            deleteButton.ID = "deleteButton_" + reader["Id"].ToString(); // Unique ID for each button
                            deleteButton.Text = "Delete";
                            deleteButton.CommandArgument = reader["Id"].ToString();
                            deleteButton.Command += DeleteTask_Click;
                            deleteButton.CssClass = "btn btn-danger"; // Apply CSS class for danger (red) color
                            cardBody.Controls.Add(deleteButton);

                            card.Controls.Add(cardBody);

                            // Add the task card to the appropriate container based on task status
                            if (Convert.ToBoolean(reader["IsCompleted"]))
                            {
                                completedTasks.Controls.Add(card);
                            }
                            else
                            {
                                pendingTasks.Controls.Add(card);
                            }
                        }
                    }
                }
            }
        }

        protected void DeleteTask_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int taskId = Convert.ToInt32(button.CommandArgument);

            // Delete task from the database
            string query = "DELETE FROM Tasks WHERE Id = @TaskId";

            using (SqlConnection connection = new DatabaseHelper().GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TaskId", taskId);
                    new DatabaseHelper().ExecuteNonQuery(command);
                }
            }

            // Reload tasks after deletion
            string userName = Session["userName"] as string;
            LoadTasks(userName);
        }

        protected void btnCreateTask_Click(object sender, EventArgs e)
        {
            // Collect the task values
            string taskName = txtTaskName.Text;
            string description = txtDescription.Text;
            string deadline = txtDeadline.Text;

            // Instantiate DatabaseHelper
            DatabaseHelper dbHelper = new DatabaseHelper();
            string userName = Session["userName"] as string;

            // Create SQL command for inserting the new task
            string query = $@"INSERT INTO Tasks (TaskName, Description, CreatedBy, Priority, Deadline) 
                     VALUES (@TaskName, @Description,(select userId from usertable where firstname='{userName}'), @Priority, @Deadline);";


            using (SqlCommand command = new SqlCommand(query))
            {
                // Set parameters for the SQL command
                command.Parameters.AddWithValue("@TaskName", taskName);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@Priority", 1);
                command.Parameters.AddWithValue("@Deadline", deadline);

                // Execute the SQL command
                dbHelper.ExecuteNonQuery(command);
              
                LoadTasks(userName);
                txtTaskName.Text="";
                txtDescription.Text = "";
               txtDeadline.Text = "";

            }


            // After inserting the task, you may want to reload the task list or perform other actions
        }





    }
}
