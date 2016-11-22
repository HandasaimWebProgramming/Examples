using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace ConsoleApplication1
{
    class Program
    {
        const int NAME_INDEX = 0;
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=\"c:\\users\\user1\\documents\\visual studio 2010\\Projects\\ConsoleApplication1\\ConsoleApplication1\\Database1.mdf\";Integrated Security=True;User Instance=True");
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT Name, ID FROM Students WHERE ID = 2;";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(NAME_INDEX));
            }

            connection.Close();
        }
    }
}
