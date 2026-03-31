using System.Globalization;

Console.WriteLine("Simple Task List (v1 - merged conflict practice)");
Console.WriteLine("Commands: add <text>, list|ls, complete <number>, help|?, exit|quit");

var tasks = new List<TaskItem>();
const int MaxTaskLength = 120;
var isRunning = true;
while (isRunning)
{
    Console.Write("> ");
    var input = Console.ReadLine()?.Trim() ?? string.Empty;

    if (string.IsNullOrWhiteSpace(input))
    {
        continue;
    }

    var parts = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    var command = parts[0].ToLower(CultureInfo.InvariantCulture);

    switch (command)
    {
        case "help":
        case "?":
            Console.WriteLine("Commands: add <text>, list|ls, complete <number>, help|?, exit|quit");
            break;
        case "add":
            if (parts.Length < 2 || string.IsNullOrWhiteSpace(parts[1]))
            {
                Console.WriteLine("Usage: add <text>");
                break;
            }

            var taskText = parts[1].Trim();
            if (taskText.Length > MaxTaskLength)
            {
                Console.WriteLine($"Task is too long. Keep it under {MaxTaskLength} characters.");
                break;
            }

            tasks.Add(new TaskItem(taskText));
            Console.WriteLine($"Added task #{tasks.Count}.");
            break;
        case "list":
        case "ls":
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks yet.");
                break;
            }

            for (var i = 0; i < tasks.Count; i++)
            {
                var marker = tasks[i].IsDone ? "[x]" : "[ ]";
                Console.WriteLine($"{i + 1}. {marker} {tasks[i].Text}");
            }
            break;
        case "complete":
            if (parts.Length < 2 || !int.TryParse(parts[1], out var taskNumber))
            {
                Console.WriteLine("Usage: complete <number>");
                break;
            }

            if (taskNumber < 1 || taskNumber > tasks.Count)
            {
                Console.WriteLine("Task number is out of range.");
                break;
            }

            tasks[taskNumber - 1].IsDone = true;
            Console.WriteLine($"Completed task #{taskNumber}.");
            break;
        case "exit":
        case "quit":
            isRunning = false;
            break;
        default:
            Console.WriteLine("Unknown command. Type 'help' to see available commands.");
            break;
    }
}

internal sealed class TaskItem
{
    public TaskItem(string text)
    {
        Text = text;
    }

    public string Text { get; }

    public bool IsDone { get; set; }
}
