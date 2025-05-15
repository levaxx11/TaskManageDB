namespace TaskManagerDB
{
    public class TaskManager
    {
        private readonly TaskRepository repository;
        private readonly TaskFilter filter;
        private readonly TaskStorage storage;

        public TaskManager()
        {
            repository = new TaskRepository();
            filter = new TaskFilter();
            storage = new TaskStorage();
            repository.GetTasks().AddRange(storage.LoadTasks());
        }

        public void AddTask(string title, string description, TaskPriority priority)
        {
            repository.AddTask(title, description, priority);
            storage.SaveTasks(repository.GetTasks());
        }

        public void EditTask(int id, string title, string description, TaskPriority priority, TaskStatus status)
        {
            repository.EditTask(id, title, description, priority, status);
            storage.SaveTasks(repository.GetTasks());
        }

        public void DeleteTask(int id)
        {
            repository.DeleteTask(id);
            storage.SaveTasks(repository.GetTasks());
        }

        public void ListTasks(TaskStatus? statusFilter = null, TaskPriority? priorityFilter = null)
        {
            filter.ListTasks(repository.GetTasks(), statusFilter, priorityFilter);
        }
    }
}