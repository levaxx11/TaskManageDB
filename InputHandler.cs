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
                        consoleInterface.DisplayError("Invalid status.");
                    break;
                case "6":
                    var priorityFilter = consoleInterface.GetPriorityFilter();
                    if (priorityFilter.HasValue)
                        taskManager.ListTasks(priorityFilter: priorityFilter);
                    else
                        consoleInterface.DisplayError("Invalid priority.");
                    break;
                case "7":
                    return false;
                default:
                    consoleInterface.DisplayError("Invalid choice. Please try again.");
                    break;

            }
                return true;
        }
        
    }



}
