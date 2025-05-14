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
         public (string title, string description, TaskPriority? priority) GetTaskDetailsForAdd()
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine();
            Console.Write("Enter description: ");
            string desc = Console.ReadLine();
            Console.Write("Enter priority (0=Low, 1=Medium, 2=High): ");
            TaskPriority? priority = null;
            if (Enum.TryParse(Console.ReadLine(), out TaskPriority parsedPriority))
            {
                priority = parsedPriority;
            }
            return (title, desc, priority);
        }

    }
}
       
 
                
                   
              
         