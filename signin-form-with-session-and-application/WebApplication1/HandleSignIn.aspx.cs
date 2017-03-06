using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class HandleSignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string email = Request.Form["email"];
            string password = Request.Form["password"];

            SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=\"|DataDirectory|\\Database1.mdf\";Integrated Security=True;User Instance=True");
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = 
                String.Format("SELECT * FROM Users WHERE Email='{0}' AND Password='{1}'", email, password);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                int id = reader.GetInt32(0);
                string fullName = reader.GetString(3);
                Session["UserId"] = id;
                Session["UserFullName"] = fullName;
                Response.Redirect("index.aspx");
            }
            else
            {
                Response.Redirect("Failed.aspx");
            }
            reader.Close();
            connection.Close();
        }
    }
}