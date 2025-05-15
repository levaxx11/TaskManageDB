using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace TaskManagerDB
{
    public class TaskStorage
    {
        private readonly Dictionary<string, string> sqlQueries;

        public TaskStorage()
        {
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

        public void SaveTasks(List<Task> tasks)
        {
            try
            {
                DatabaseConnection.OpenConnection();

                // Clear existing tasks
                var clearCommand = new SqlCommand(sqlQueries["ClearTasks"], DatabaseConnection.Connection);
                clearCommand.ExecuteNonQuery();

                // Insert all tasks
                foreach (var task in tasks)
                {
                    var command = new SqlCommand(sqlQueries["InsertTask"], DatabaseConnection.Connection);
                    command.Parameters.AddWithValue("@Id", task.Id);
                    command.Parameters.AddWithValue("@Title", task.Title);
                    command.Parameters.AddWithValue("@Description", (object)task.Description ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Priority", (int)task.Priority);
                    command.Parameters.AddWithValue("@Status", (int)task.Status);
                    command.Parameters.AddWithValue("@CreatedAt", task.CreatedAt);
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                DatabaseConnection.CloseConnection();
            }
        }

        public List<Task> LoadTasks()
        {
            var tasks = new List<Task>();
            try
            {
                DatabaseConnection.OpenConnection();

                var command = new SqlCommand(sqlQueries["SelectTasks"], DatabaseConnection.Connection);
                using (var reader = command.ExecuteReader())
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
            finally
            {
                DatabaseConnection.CloseConnection();
            }
            return tasks;
        }
    }
}