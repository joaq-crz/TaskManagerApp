using System;                    // namespace
using System.Collections.Generic; // generic collections

namespace TaskManagerApp         // namespace
{
    // TASKITEM CLASS
    // holds the data for one task.
    class TaskItem              // class
    {
        public int Id;                  
        public string CourseTitle = "";  
        public string TaskType = "";    
        public string Label = "";          
        public string DueDate = "N/A";   
        public bool IsCompleted;           
    }

    class Program               // class
    {
        // List of all tasks
        static List<TaskItem> tasks = new List<TaskItem>(); // static field, generic List<TaskItem>, object instantiation

        // Counter for assigning IDs
        static int nextId = 1;  

        static void Main()      // static method, entry point
        {
            while (true)        // while loop
            {
                Console.Clear(); // console I/O

                // show tasks 
                ShowAllTasks();  // method call

                // show menu commands
                Console.WriteLine();       // console I/O
                Console.WriteLine("==== COMMANDS ====");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. Delete Task");
                Console.WriteLine("3. Mark/Unmark Completed");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine() ?? ""; // string, Console.ReadLine, null-coalescing operator (return empty string instead of null)

                switch (choice)  // switch statement, selection
                {
                    case "1":
                        AddTask();        // method call
                        break;
                    case "2":
                        DeleteTask();     // method call
                        break;
                    case "3":
                        ToggleCompleted(); // method call
                        break;
                    case "0":
                        return; // return statement, program termination
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey(); // console I/O
                        break;
                }
            }
        }

        // SHOW ALL TASKS METHOD
        static void ShowAllTasks()        // static method, void return type
        {
            Console.WriteLine("==== TASKS ====\n");

            if (tasks.Count == 0)         // if statement, selection
            {
                Console.WriteLine("No tasks yet.");
                return;                   // early return
            }

            foreach (var task in tasks)   // foreach loop, iteration
            {
                PrintTaskDetails(task);   // method call with parameter
                Console.WriteLine(new string('-', 40)); // string constructor, expression
            }
        }

        // Print one task
        static void PrintTaskDetails(TaskItem task) // method parameter, pass-by-value, reference type
        {
            string status = task.IsCompleted ? "Completed" : "Pending"; // string, ternary operator (conditional operator)
            Console.WriteLine($"ID: {task.Id} [{status}]");             // string interpolation
            Console.WriteLine($"Course: {task.CourseTitle}");
            Console.WriteLine($"Type:   {task.TaskType}");
            Console.WriteLine($"Label:  {task.Label}");
            Console.WriteLine($"Due:    {task.DueDate}");
        }

        // ADD TASK
        static void AddTask()             // method
        {
            Console.Clear();
            Console.WriteLine("== Add Task ==");

            Console.Write("Course title (e.g., Programming Languages): ");
            string course = Console.ReadLine() ?? ""; // string, null-coalescing operator

            Console.Write("Task Type (Quiz, Project, Exam, etc.): ");
            string type = Console.ReadLine() ?? "";

            Console.Write("Label (short name, e.g., MP4, Quiz 1): ");
            string label = Console.ReadLine() ?? "";

            Console.Write("Due date (MM-DD-YYYY) or leave empty: ");
            string due = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(due))       // static method call, string helper
            {
                due = "N/A";                          // assignment
            }

            TaskItem task = new TaskItem             // object initialization, new object
            {
                Id = nextId++,                       // assignment, post-increment
                CourseTitle = course,
                TaskType = type,
                Label = label,
                DueDate = due,
                IsCompleted = false                  // bool literal
            };

            tasks.Add(task);                         // List<T> method

            Console.WriteLine("\nTask added successfully. Press any key to continue...");
            Console.ReadKey();
        }

        // DELETE TASK
        static void DeleteTask()                     // method
        {
            Console.Clear();
            Console.WriteLine("== Delete Task ==");

            if (tasks.Count == 0)                    // if statement
            {
                Console.WriteLine("No tasks available to delete.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            PrintTasksShort();                       // method call

            Console.Write("\nEnter Task ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) // int.TryParse, out parameter, negation operator
            {
                Console.WriteLine("Invalid ID. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            TaskItem? task = FindTaskById(id);       // nullable reference type, method call
            if (task == null)                        // null check
            {
                Console.WriteLine("Task not found. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.Write($"Are you sure you want to delete \"{task.Label}\"? (Y/N): ");
            string confirm = (Console.ReadLine() ?? "").Trim().ToUpper(); // string methods, method chaining

            if (confirm == "Y")                      // equality operator
            {
                tasks.Remove(task);                  // List<T>.Remove
                Console.WriteLine("Task deleted successfully.");
            }
            else
            {
                Console.WriteLine("Delete cancelled.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        // MARK / UNMARK COMPLETED
        static void ToggleCompleted()                // method
        {
            Console.Clear();
            Console.WriteLine("== Mark/Unmark Completed ==");

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            PrintTasksWithStatus();                  // method call

            Console.Write("\nEnter Task ID to change status: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            TaskItem? task = FindTaskById(id);       // nullable reference type
            if (task == null)
            {
                Console.WriteLine("Task not found.");
            }
            else
            {
                task.IsCompleted = !task.IsCompleted; // logical negation, assignment
                Console.WriteLine("Task status changed.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        // HELPER METHODS
        static TaskItem? FindTaskById(int id)        // method, return type TaskItem?, parameter int
        {
            foreach (var t in tasks)                 // foreach loop
            {
                if (t.Id == id)                      // comparison operator
                    return t;                        // return reference
            }
            return null;                             // null literal
        }

        static void PrintTasksShort()                // method
        {
            foreach (var task in tasks)
            {
                Console.WriteLine($"ID: {task.Id} | Label: {task.Label}");
            }
        }

        static void PrintTasksWithStatus()           // method
        {
            foreach (var task in tasks)
            {
                string status = task.IsCompleted ? "Completed" : "Pending"; // ternary operator
                Console.WriteLine($"ID: {task.Id} | [{status}] | {task.Label}");
            }
        }
    }
}