using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private string connectionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=\"|DataDirectory|\\Database1.mdf\";Integrated Security=True;User Instance=True";

        public string message = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST")
            {
                string name = Request.Form["name"];
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = String.Format("INSERT INTO Names VALUES ('{0}');", name);
                try
                {
                    command.ExecuteNonQuery();
                    message = "Success!";
                }
                catch (SqlException)
                {
                    message = "User already exists!";
                }
                connection.Close();
            }
        }
    }
}