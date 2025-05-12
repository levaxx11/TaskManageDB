using System;
using System.Collections.Generic;

namespace TaskManagerDB
{
    public class TaskRepository
    {
        private List<Task> tasks = new List<Task>();

        public void AddTask(string title, string description, TaskPriority priority)
        {
            int newId = tasks.Count > 0 ? tasks[^1].Id + 1 : 1;
            tasks.Add(new Task(newId, title, description, priority));
            Console.WriteLine("Task added successfully!");
        }

        public void EditTask(int id, string title, string description, TaskPriority priority, TaskStatus status)
        {
            var task = tasks.Find(t => t.Id == id);
            if (task != null)
            {
                task.Title = title;
                task.Description = description;
                task.Priority = priority;
                task.Status = status;
                Console.WriteLine("Task updated successfully!");
            }
            else
            {
                Console.WriteLine("Task not found!");
            }
        }
              public void DeleteTask(int id)
        {
            var task = tasks.Find(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
                Console.WriteLine("Task deleted successfully!");
            }
            else
            {
                Console.WriteLine("Task not found!");
            }
        }

        public List<Task> GetTasks()
        {
            return tasks;
        }

      
    }
}