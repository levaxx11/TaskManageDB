using System;

namespace TaskManagerDB
{
    public class InputHandler
    {
        private readonly TaskManager taskManager;
        private readonly ConsoleInterface consoleInterface;

        public InputHandler(TaskManager taskManager, ConsoleInterface consoleInterface)
        {
            this.taskManager = taskManager;
            this.consoleInterface = consoleInterface;
        }

        public bool HandleInput(string choice)
        {
            switch (choice)
            {
                case "1":
                    HandleAddTask();
                    break;
                case "2":
                    HandleEditTask();
                    break;
                case "3":
                    HandleDeleteTask();
                    break;
                case "4":
                    taskManager.ListTasks();
                    break;
                case "5":
                    var statusFilter = consoleInterface.GetStatusFilter();
                    if (statusFilter.HasValue)
                        taskManager.ListTasks(statusFilter: statusFilter);
                    else
                        consoleInterface.DisplayError("Invalid status!");
                    break;
                case "6":
                    var priorityFilter = consoleInterface.GetPriorityFilter();
                    if (priorityFilter.HasValue)
                        taskManager.ListTasks(priorityFilter: priorityFilter);
                    else
                        consoleInterface.DisplayError("Invalid priority!");
                    break;
                case "7":
                    return false;
                default:
                    consoleInterface.DisplayError("Invalid option!");
                    break;
            }
            return true;
        }

        private void HandleAddTask()
        {
            var details = consoleInterface.GetTaskDetailsForAdd();
            if (details.Priority.HasValue)
                taskManager.AddTask(details.Title, details.Description, details.Priority.Value);
            else
                consoleInterface.DisplayError("Invalid priority!");
        }

        private void HandleEditTask()
        {
            var details = consoleInterface.GetTaskDetailsForEdit();
            if (details.Id.HasValue && details.Priority.HasValue && details.Status.HasValue)
                taskManager.EditTask(details.Id.Value, details.Title, details.Description, details.Priority.Value, details.Status.Value);
            else
            {
                if (!details.Id.HasValue) consoleInterface.DisplayError("Invalid ID!");
                if (!details.Priority.HasValue) consoleInterface.DisplayError("Invalid priority!");
                if (!details.Status.HasValue) consoleInterface.DisplayError("Invalid status!");
            }
        }

        private void HandleDeleteTask()
        {
            var id = consoleInterface.GetTaskIdForDelete();
            if (id.HasValue)
                taskManager.DeleteTask(id.Value);
            else
                consoleInterface.DisplayError("Invalid ID!");
        }
    }
}