using System.Data.SqlClient;

namespace TaskManagerDB
{
    public static class DatabaseConnection
    {
         public static SqlConnection Connection { get; } = CreateConnection();

  private static SqlConnection CreateConnection()
  {
      string connectionString = "Data Source=LEVAXX11\\MSSQLSERVER2022;Initial Catalog=TaskManagerDB;User ID=levax; Integrated Security=True";
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