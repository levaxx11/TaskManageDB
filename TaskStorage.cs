using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace TaskManagerDB
{
    public class TaskStorage : IDisposable
    {
        private readonly DatabaseConnection dbConnection;
        private readonly Dictionary<string, string> sqlQueries;
        private bool disposed = false;
        public TaskStorage()
        {
            dbConnection = new DatabaseConnection();
            sqlQueries = LoadSqlQueries();
        }
        private Dictionary<string, string> LoadSqlQueries()
        {
            var queries = new Dictionary<string, string>();
            try
            {
                string content = File.ReadAllText("sqlquary.txt").Trim();
                var queryPairs = content.Split(';', StringSplitOptions.RemoveEmptyEntries);
                foreach (var pair in queryPairs)
                {
                    var parts = pair.Split(':', 2);
                    if (parts.Length == 2)
                    {
                        queries[parts[0].Trim()] = parts[1].Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load SQL queries from sqlquary.txt", ex);
            }

            if (!queries.ContainsKey("ClearTasks") || !queries.ContainsKey("InsertTask") || !queries.ContainsKey("SelectTasks"))
            {
                throw new Exception("Required SQL queries (ClearTasks, InsertTask, SelectTasks) not found in sqlquary.txt");
            }
            return queries;

        }
        public void SaveTask(List<Task> tasks)
        {
            using (var connection = dbConnection.GetConnection())
            {
                var clearCommand = new SqlCommand(sqlQueries["ClearTasks"], connection);
                clearCommand.ExecuteNonQuery();
                foreach (var task in tasks)
                {
                    var command = new SqlCommand(sqlQueries["InsertTask"], connection);
                    command.Parameters.AddWithValue("@Title", task.Title);
                       command.Parameters.AddWithValue("@Description", (object)task.Description ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Priority", task.Priority);
                    command.Parameters.AddWithValue("@Status", task.Status);
                    command.Parameters.AddWithValue("@CreatedAt", task.CreatedAt);
                    command.ExecuteNonQuery();
                }

            }
        }
        public List<Task> LoadTasks()
        {
            var tasks = new List<Task>();
            using (var connection = dbConnection.GetConnection())
            {
                var selectCommand = new SqlCommand(sqlQueries["SelectTasks"], connection);
                using (var reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var task = new Task(
                             id: reader.GetInt32(0),
                             title: reader.GetString(1),
                             description: reader.IsDBNull(2) ? null : reader.GetString(2),
                             priority: (TaskPriority)reader.GetInt32(3)
                         )
                        {
                            Status = (TaskStatus)reader.GetInt32(4),
                            CreatedAt = reader.GetDateTime(5)
                        };
                        tasks.Add(task);
                    }
                }
            }
            return tasks;
        }
        public void Dispose()
        {
            if (!disposed)
            {
                dbConnection.Dispose();
                disposed = true;
            }
        }   
    }
    
    
}