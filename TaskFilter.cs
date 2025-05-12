using System;
using System.Collections.Generic;

namespace TaskManagerDB
{
    public class TaskFilter
    {
        public void ListTasks(List<Task> tasks, TaskStatus? statusFilter = null, TaskPriority? priorityFilter = null)
        {
            var filteredTasks = tasks;
            if (statusFilter.HasValue)
                filteredTasks = filteredTasks.FindAll(t => t.Status == statusFilter.Value);
            if (priorityFilter.HasValue)
                filteredTasks = filteredTasks.FindAll(t => t.Priority == priorityFilter.Value);

            if (filteredTasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            foreach (var task in filteredTasks)
            {
                Console.WriteLine($"ID: {task.Id}, Title: {task.Title}, Status: {task.Status}, Priority: {task.Priority}, Created: {task.CreatedAt}");
                Console.WriteLine($"Description: {task.Description}\n");
            }
        }
    }
}