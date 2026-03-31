using System.Globalization;

Console.WriteLine("Simple Task List (v1)");
Console.WriteLine("Commands: add <text>, list, help, exit");

var tasks = new List<string>();
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
            Console.WriteLine("Commands: add <text>, list, help, exit");
            break;
        case "add":
            if (parts.Length < 2 || string.IsNullOrWhiteSpace(parts[1]))
            {
                Console.WriteLine("Usage: add <text>");
                break;
            }

            tasks.Add(parts[1]);
            Console.WriteLine($"Added task #{tasks.Count}.");
            break;
        case "list":
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks yet.");
                break;
            }

            for (var i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i]}");
            }
            break;
        case "exit":
            isRunning = false;
            break;
        default:
            Console.WriteLine("Unknown command. Type 'help' to see available commands.");
            break;
    }
}
