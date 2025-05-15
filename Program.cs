namespace TaskManagerDB
{
    class Program
    {
        static void Main(string[] args)
        {
            var taskManager = new TaskManager();
            var consoleInterface = new ConsoleInterface();
            var inputHandler = new InputHandler(taskManager, consoleInterface);

            while (true)
            {
                consoleInterface.DisplayMenu();
                string choice = consoleInterface.GetUserInput();
                consoleInterface.ClearConsole();

                if (!inputHandler.HandleInput(choice))
                    break;
            }
        }
    }
}