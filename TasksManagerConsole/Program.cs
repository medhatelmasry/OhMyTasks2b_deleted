using TasksManagerConsole;

Console.WriteLine("Welcome to the Task Manager Demo!");
var taskManager = new TaskManager();

while (true)
{
    ShowMenu();
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            AddTask(taskManager);
            break;
        case "2":
            ListTasks(taskManager);
            break;
        case "3":
            MarkTaskComplete(taskManager);
            break;
        case "4":
            DeleteTask(taskManager);
            break;
        case "5":
            return;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}

static void ShowMenu()
{
    Console.WriteLine("\n" + new string('=', 25));
    Console.WriteLine("Please select an option:");
    Console.WriteLine("1. Add new task");
    Console.WriteLine("2. List all tasks");
    Console.WriteLine("3. Mark task as complete");
    Console.WriteLine("4. Delete task");
    Console.WriteLine("5. Exit");
    Console.WriteLine(new string('=', 25));
}

static void AddTask(TaskManager manager)
{
    Console.Write("Enter task description: ");
    var description = Console.ReadLine();
    manager.AddTask(description!);
    Console.WriteLine("\nTask added successfully!");
}

static void ListTasks(TaskManager manager)
{
    var tasks = manager.GetTasks();
    if (!tasks.Any())
    {
        Console.WriteLine("No tasks found.");
        return;
    }

    for (int i = 0; i < tasks.Count; i++)
    {
        Console.WriteLine($"{i + 1}. [{(tasks[i].IsCompleted ? "X" : " ")}] {tasks[i].Description}");
    }
}

static void MarkTaskComplete(TaskManager manager)
{
    var tasks = manager.GetTasks();
    if (!tasks.Any())
    {
        Console.WriteLine("No tasks available to mark as complete.");
        return;
    }

    ListTasks(manager);
    Console.Write("Enter task number to mark as complete: ");
    if (int.TryParse(Console.ReadLine(), out int taskNumber))
    {
        if (taskNumber >= 1 && taskNumber <= tasks.Count)
        {
            manager.MarkTaskComplete(taskNumber - 1);
            Console.WriteLine("\nTask marked as complete!");
        }
        else
        {
            Console.WriteLine("Invalid task number.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid number.");
    }
}

static void DeleteTask(TaskManager manager)
{
    var tasks = manager.GetTasks();
    if (!tasks.Any())
    {
        Console.WriteLine("No tasks available to delete.");
        return;
    }

    ListTasks(manager);
    Console.Write("Enter task number to delete: ");
    if (int.TryParse(Console.ReadLine(), out int taskNumber))
    {
        if (taskNumber >= 1 && taskNumber <= tasks.Count)
        {
            var taskToDelete = tasks[taskNumber - 1];
            Console.Write($"Are you sure you want to delete '{taskToDelete.Description}'? (y/N): ");
            var confirmation = Console.ReadLine()?.ToLower();
            
            if (confirmation == "y" || confirmation == "yes")
            {
                bool success = manager.DeleteTask(taskNumber - 1);
                if (success)
                {
                    Console.WriteLine("\nTask deleted successfully!");
                    Console.WriteLine("Press 'u' to undo deletion or any other key to continue...");
                    var undoChoice = Console.ReadKey(true).KeyChar;
                    if (undoChoice == 'u' || undoChoice == 'U')
                    {
                        manager.AddTask(taskToDelete.Description);
                        if (taskToDelete.IsCompleted)
                        {
                            manager.MarkTaskComplete(manager.GetTaskCount() - 1);
                        }
                        Console.WriteLine("Task deletion undone!");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to delete task.");
                }
            }
            else
            {
                Console.WriteLine("Task deletion cancelled.");
            }
        }
        else
        {
            Console.WriteLine("Invalid task number.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid number.");
    }
}