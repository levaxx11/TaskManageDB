using System;
namespace TaskManagerDB
{
    public class ConsoleInterface
    {
        private void ShowMenu()
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
        public bool GetTaskDetailsForAdd(out string title, out string description, out TaskPriority? priority)
        {
            Console.Write("Enter title: ");
            title = Console.ReadLine();
            Console.Write("Enter description: ");
            description = Console.ReadLine();
            Console.Write("Enter priority (0=Low, 1=Medium, 2=High): ");
            priority = null;
            string priorityInput = Console.ReadLine();
            try
            {
                priority = (TaskPriority)int.Parse(priorityInput);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }

        }
        public bool GetTaskDetailsForEdit(out int? id, out string title, out string description, out TaskPriority? priority, out TaskStatus? status)
        {
            Console.Write("Enter task ID: ");
            id = null;
            string idInput = Console.ReadLine();
            try
            {
                id = int.Parse(idInput);
            }
            catch (FormatException)
            {
                id = null;
                title = null;
                description = null;
                priority = null;
                status = null;
                return false;
            }
            Console.Write("Enter new title: ");
            title = Console.ReadLine();
            Console.Write("Enter new description: ");
            description = Console.ReadLine();
            Console.Write("Enter new priority (0=Low, 1=Medium, 2=High): ");
            priority = null;
            string priorityInput = Console.ReadLine();
            try
            {
                priority = (TaskPriority)int.Parse(priorityInput);
            }
            catch (FormatException)
            {
                priority = null;
                status = null;
                return false;
            }
            Console.Write("Enter new status (0=ToDo, 1=InProgress, 2=Done): ");
            status = null;
            string statusInput = Console.ReadLine();
            try
            {
                status = (TaskStatus)int.Parse(statusInput);
                return true;
            }
            catch (FormatException)
            {
                status = null;
                return false;
            }
        }
        public int? GetTaskIdForDelete()
        {
            Console.Write("Enter task ID to delete: ");
            string idInput = Console.ReadLine();
            try
            {
                return int.Parse(idInput);
            }
            catch (FormatException)
            {
                return null;
            }
        }
        public TaskStatus? GetStatusFilter()
        {
            Console.Write("Enter status to filter (0=ToDo, 1=InProgress, 2=Done): ");
            string input = Console.ReadLine();
            try
            {
                return (TaskStatus)int.Parse(input);
            }
            catch (FormatException)
            {
                return null;
            }

        }
        public TaskPriority? GetPriorityFilter()
        {
            Console.Write("Enter priority to filter (0=Low, 1=Medium, 2=High): ");
            string input = Console.ReadLine();
            try
            {
                return (TaskPriority)int.Parse(input);
            }
            catch (FormatException)
            {
                return null;
            }
        }

    }
}
       
 
                
                   
              
         