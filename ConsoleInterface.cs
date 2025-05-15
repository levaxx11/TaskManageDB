using System;

namespace TaskManagerDB
{
    public class ConsoleInterface
    {
        public void DisplayMenu()
        {
            Console.WriteLine("\nTask Manager");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Edit Task");
            Console.WriteLine("3. Delete Task");
            Console.WriteLine("4. List All Tasks");
            Console.WriteLine("5. Filter Tasks by Status");
            Console.WriteLine("6. Filter Tasks by Priority");
            Console.WriteLine("7. Exit");
            Console.Write("Choose an option: ");
        }

        public string GetUserInput()
        {
            return Console.ReadLine();
        }

        public void ClearConsole()
        {
            Console.Clear();
        }

        public void DisplayError(string message)
        {
            Console.WriteLine(message);
        }

        private TaskPriority? ParsePriority(string input)
        {
            switch (input)
            {
                case "0":
                    return TaskPriority.Low;
                case "1":
                    return TaskPriority.Medium;
                case "2":
                    return TaskPriority.High;
                default:
                    return null;
            }
        }

        private TaskStatus? ParseStatus(string input)
        {
            switch (input)
            {
                case "0":
                    return TaskStatus.ToDo;
                case "1":
                    return TaskStatus.InProgress;
                case "2":
                    return TaskStatus.Done;
                default:
                    return null;
            }
        }

        public TaskDetails GetTaskDetailsForAdd()
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine();
            Console.Write("Enter description: ");
            string desc = Console.ReadLine();
            Console.Write("Enter priority (0=Low, 1=Medium, 2=High): ");
            string priorityInput = Console.ReadLine();
            TaskPriority? priority = ParsePriority(priorityInput);

            return new TaskDetails(title, desc, priority);
        }

        public TaskDetails GetTaskDetailsForEdit()
        {
            Console.Write("Enter task ID: ");
            int? id = null;
            string idInput = Console.ReadLine();
            if (int.TryParse(idInput, out int parsedId))
            {
                id = parsedId;
            }

            Console.Write("Enter new title: ");
            string title = Console.ReadLine();
            Console.Write("Enter new description: ");
            string desc = Console.ReadLine();
            Console.Write("Enter new priority (0=Low, 1=Medium, 2=High): ");
            string priorityInput = Console.ReadLine();
            TaskPriority? priority = ParsePriority(priorityInput);

            Console.Write("Enter new status (0=ToDo, 1=InProgress, 2=Done): ");
            string statusInput = Console.ReadLine();
            TaskStatus? status = ParseStatus(statusInput);

            return new TaskDetails(id, title, desc, priority, status);
        }

        public int? GetTaskIdForDelete()
        {
            Console.Write("Enter task ID: ");
            string idInput = Console.ReadLine();
            if (int.TryParse(idInput, out int id))
            {
                return id;
            }
            return null;
        }

        public TaskStatus? GetStatusFilter()
        {
            Console.Write("Enter status to filter (0=ToDo, 1=InProgress, 2=Done): ");
            string statusInput = Console.ReadLine();
            return ParseStatus(statusInput);
        }

        public TaskPriority? GetPriorityFilter()
        {
            Console.Write("Enter priority to filter (0=Low, 1=Medium, 2=High): ");
            string priorityInput = Console.ReadLine();
            return ParsePriority(priorityInput);
        }
    }
}