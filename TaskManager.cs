namespace TaskManagerDB
{
    public class TaskManagerDB : IDisposable
    {
        private readonly TaskRepository repository;
        private readonly TaskFilter filter;
        private readonly TaskStorage storage;
        private bool disposed = false;

        public TaskManagerDB()
        {
            repository = new TaskRepository();
            filter = new TaskFilter();
            storage = new TaskStorage();
            repository.GetTasks().AddRange(storage.LoadTasks());
        }
        public void AddTask(string title, string description, TaskPriority priority)
        {
            repository.AddTask(title, description, priority);
            storage.SaveTask(repository.GetTasks());
        }
        public void EditTask(int id, string title, string description, TaskPriority priority, TaskStatus status)
        {
            repository.EditTask(id, title, description, priority, status);
            storage.SaveTask(repository.GetTasks());
        }
        public void DeleteTask(int id)
        {
            repository.DeleteTask(id);
            storage.SaveTask(repository.GetTasks());
        }
        public void ListTasks(TaskStatus? statusFilter = null, TaskPriority? priorityFilter = null)
        {
            filter.ListTasks(repository.GetTasks(), statusFilter, priorityFilter);
        }
        public void Dispose()
        {
            if (!disposed)
            {
                storage.Dispose();
                disposed = true;
            }
        }


    }
}