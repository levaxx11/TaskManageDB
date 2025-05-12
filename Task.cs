using System;

namespace TaskManagerDB
{
    public enum TaskPriority { Low, Medium, High }
    public enum TaskStatus { ToDo, InProgress, Done }

    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public Task(int id, string title, string description, TaskPriority priority)
        {
            Id = id;
            Title = title;
            Description = description;
            Priority = priority;
            Status = TaskStatus.ToDo;
            CreatedAt = DateTime.Now;
        }
    }
}