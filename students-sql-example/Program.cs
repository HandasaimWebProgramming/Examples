using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // from the Server Explorer
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Eidan Cohen\\documents\\visual studio 2015\\Projects\\ConsoleApplication1\\ConsoleApplication1\\Database1.mdf\";Integrated Security=True";
            // A connection allows me to run commands on the database
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open(); // This line will fail if the connection string is faulty.

            //CreateTables(connection); // Only run once

            //InsertData(connection); // Again, run only once, but the insert functions used inside could be used a gain with different values

            PrintTable("Grades", connection);

            PrintStudentGrades("John", "Doe", connection);
            Console.WriteLine(CalculateGradeAverage("Jane", "Doe", connection));
            Console.WriteLine(CalculateGradeAverageSingleQuery("John", "Doe", connection));

            connection.Close(); // You always flush the toilet right ? Always close your system resources!
        }
        
        private static void CreateTables(SqlConnection connection)
        {
            // Thats one way to create and execute a command
            SqlCommand command = new SqlCommand("CREATE TABLE Students (Id int IDENTITY(1,1) PRIMARY KEY, FirstName varchar(255), LastName varchar(255));", connection);
            command.ExecuteNonQuery();

            command = new SqlCommand("CREATE TABLE Teachers (Id int IDENTITY(1,1) PRIMARY KEY, FirstName varchar(255), LastName varchar(255), Profession varchar(255));", connection);
            command.ExecuteNonQuery();
            
            // That's another way
            SqlCommand command2 = connection.CreateCommand();
            command2.CommandText = "CREATE TABLE Grades (StudentId int FOREIGN KEY REFERENCES Students(Id), TeacherId int FOREIGN KEY REFERENCES Teachers(Id), Grade int);";
            command2.ExecuteNonQuery();
        }

        private static void InsertData(SqlConnection connection)
        {
            InsertStudent("John", "Doe", connection);
            InsertStudent("Jane", "Doe", connection);
            InsertTeacher("Eidan", "Cohen", "Computer Science", connection);
            InsertTeacher("Shlomi", "Cohen", "Electronics", connection);
            InsertGrade(1, 1, 90, connection);
            InsertGrade(2, 1, 95, connection);
            InsertGrade(1, 2, 85, connection);
            InsertGrade(2, 2, 100, connection);
        }
        
        private static void InsertStudent(string firstName, string lastName, SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            // We can embed parameters in our query using the + operator
            command.CommandText = "INSERT INTO Students VALUES ('" + firstName + "','" + lastName + "');";
            command.ExecuteNonQuery();
        }

        private static void InsertTeacher(string firstName, string lastName, string profession, SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            // But we can also embed parameters using the much better String.Format method
            command.CommandText = string.Format("INSERT INTO Teachers VALUES ('{0}', '{1}', '{2}');", firstName, lastName, profession);
            command.ExecuteNonQuery();
        }

        private static void InsertGrade(int studentId, int teacherId, int grade, SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = string.Format("INSERT INTO Grades VALUES ('{0}', '{1}', '{2}');", studentId, teacherId, grade);
            command.ExecuteNonQuery();
        }

        private static void PrintTable(string tableName, SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM " + tableName + ";"; // The simplest select statement that gets all data from a table
            SqlDataReader reader = command.ExecuteReader(); // We want to get data from the query, that's why we use a reader
            PrintTable(reader);
            reader.Close(); // Again, flush your toilet... and close your resources.
        }

        private static void PrintTable(SqlDataReader reader)
        {
            int columnCount = reader.FieldCount;
            // Print Header (Column names)
            for(int i = 0; i < columnCount; i++)
            {
                // reader.GetName(i) gets the name of the ith coulmn
                // \t is a tab
                Console.Write(reader.GetName(i) + "\t");
            }
            Console.WriteLine();
            // Print Header (Data Names)
            while (reader.Read()) // Read next row, stop if no more rows
            {
                for (int i = 0; i < columnCount; i++)
                {
                    // reader.GetValue(i) gets the value of the ith column in the current row
                    // I use GetValue instead of GetString or GetInt32 because I don't know what the datatype is and I just want to print it.
                    Console.Write(reader.GetValue(i).ToString() + "\t");
                }
                Console.WriteLine();
            }
        }

        private static double CalculateGradeAverage(string firstName, string lastName, SqlConnection connection)
        {
            // First thing first, I need the Student Id
            SqlCommand command = connection.CreateCommand();
            command.CommandText = String.Format("SELECT Id FROM Students WHERE FirstName = '{0}' AND LastName = '{1}';", firstName, lastName);
            int id = (int)command.ExecuteScalar(); // If i know I will only get a single value, I may use ExecuteScalar
            /*
                Another way of doing this (without ExecuteScalar):
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int id = reader.GetInt32(0);
                reader.Close();
            */
            command = connection.CreateCommand();
            command.CommandText = "SELECT Grade FROM Grades WHERE StudentId = " + id + ";"; // Note that I dont use '' because the column is an integer, not a string
            SqlDataReader reader = command.ExecuteReader();
            double sum = 0;
            int count = 0;
            while(reader.Read())
            {
                sum += reader.GetInt32(0);
                count++;
            }
            reader.Close();
            return sum / count;
        }

        private static void PrintStudentGrades(string firstName, string lastName, SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            // I use the join command to match rows in two tables based on a two matching columns: Students.Id and Grades.StudentId
            command.CommandText = String.Format("SELECT FirstName, LastName, Grade FROM Students JOIN Grades ON Students.Id = Grades.StudentId WHERE FirstName = '{0}' AND LastName = '{1}';", firstName, lastName);
            SqlDataReader reader = command.ExecuteReader();
            PrintTable(reader);
            reader.Close();
        }

        private static int CalculateGradeAverageSingleQuery(string firstName, string lastName, SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            // I use the AVG function to magically calculate the avarage of a column. Notice the use of the sub-query (in parentheses) to find the required student id.
            command.CommandText = String.Format("SELECT AVG(Grade) FROM Grades WHERE StudentId = (SELECT Id FROM Students WHERE FirstName = '{0}' AND LastName = '{1}');", firstName, lastName);
            return (int)command.ExecuteScalar();
        }

    }
}
