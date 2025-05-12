using System;
using System.Data.SqlClient;
using System.IO;

namespace TaskManager
{
    public class DatabaseConnection : IDisposable
    {
        private readonly SqlConnection connection;
        private bool disposed = false;

        public DatabaseConnection()
        {
            try
            {
                string connectionString = File.ReadAllText("connection.txt").Trim();
                connection = new SqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Successfully connected to the database!");
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to connect to the database", ex);
            }
        }

        public SqlConnection GetConnection()
        {
            if (disposed)
                throw new ObjectDisposedException("DatabaseConnection");
            return connection;
        }

        public void Dispose()
        {
            if (!disposed)
            {
                connection.Close();
                connection.Dispose();
                disposed = true;
            }
        }
    }
}