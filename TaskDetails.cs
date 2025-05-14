namespace TaskManagerDB
{
    public class TaskDetails
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskPriority? Priority { get; set; }
        public TaskStatus? Status { get; set; }
        public int? Id { get; set; }

        public TaskDetails()
        {
        }

        public TaskDetails(string title, string description, TaskPriority? priority)
        {
            Title = title;
            Description = description;
            Priority = priority;
        }

        public TaskDetails(int? id, string title, string description, TaskPriority? priority, TaskStatus? status)
        {
            Id = id;
            Title = title;
            Description = description;
            Priority = priority;
            Status = status;
        }
    }
}