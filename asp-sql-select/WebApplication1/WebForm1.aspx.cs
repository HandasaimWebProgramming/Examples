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
        public string tableContent = "";
        public string template = @"<tr>
                <td>{0}</td>
                <td>{1}</td>
                <td>{2}</td>
            </tr>
";
        protected void Page_Load(object sender, EventArgs e)
        {
            string connetionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=\"|DataDirectory|\\Database1.mdf\";Integrated Security=True;User Instance=True";
            SqlConnection connection = new SqlConnection(connetionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM People;";
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string row = String.Format(template, reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                tableContent += row;
            }
            reader.Close();

            connection.Close();
        }
    }
}