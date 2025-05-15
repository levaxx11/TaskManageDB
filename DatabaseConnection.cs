using System.Data.SqlClient;
using System.IO;

namespace TaskManagerDB
{
    public static class DatabaseConnection
    {
        public static SqlConnection Connection { get; } = CreateConnection();

        private static SqlConnection CreateConnection()
        {
            string connectionString;
            try
            {
                connectionString = File.ReadAllText("connection.txt").Trim();
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("The connection.txt file was not found. Please create it with the SQL connection string.");
            }
            catch (IOException ex)
            {
                throw new IOException("An error occurred while reading the connection.txt file.", ex);
            }

            return new SqlConnection(connectionString);
        }

        public static void OpenConnection()
        {
            Connection.Open();
        }

        public static void CloseConnection()
        {
            Connection.Close();
        }
    }
}