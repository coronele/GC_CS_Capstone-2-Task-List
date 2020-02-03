using System;
using System.Collections.Generic;
using static GC_CS_Capstone_2_TaskList.MyMethods;

namespace GC_CS_Capstone_2_TaskList
{
    class Program
    {
        static void Main(string[] args)
        {
            // Base task list
            List<Task> taskList = new List<Task>
            {
                new Task($"Erwin","Complete Capstone", DateTime.Parse("2/2/2020"),false),
                new Task($"Erwin","Present at demo day",DateTime.Parse("3/27/2020"),false),
                new Task($"Bob","Attend demo day",DateTime.Parse("3/27/2020"), false)
            };
            
            int progOption = 0;

            // Program, identify yourself
            ShowTitle("Welcome to the Task Manager!");

            do
            {
                DisplayMenu();
                progOption = UserChoice("What would you like to do?","This is not a valid choice. Enter a number from [1-6]\n",6);
                runOption(progOption, taskList);
            }
            while (progOption != 6);

        }

        public static void DisplayMenu()
        {
            // Displays program optiopns

            SetOutputColor();
            Console.WriteLine("__________________________________________________");
            Console.WriteLine($"1)\tList Tasks");
            Console.WriteLine($"2)\tAdd Task");
            Console.WriteLine($"3)\tDelete Task");
            Console.WriteLine($"4)\tMark Task Complete");
            Console.WriteLine($"5)\tEdit a task");
            Console.WriteLine($"6)\tQuit");
            Console.WriteLine("__________________________________________________\n");

        }

        public static void runOption(int programOption, List<Task> taskListToRun)
        {
            // Runs program option based on user selection

            switch (programOption)
            {
                case 1:
                    ListTasks(taskListToRun);
                    break;
                case 2:
                    AddTasks(taskListToRun);
                    break;
                case 3:
                    DeleteTasks(taskListToRun);
                    break;
                case 4:
                    CompleteTasks(taskListToRun);
                    break;
                case 5:
                    EditTasks(taskListToRun);
                    break;
                default:
                    break;
            }
        }

        public static void PrintListOptions()
        {
            // If list is selected, presents user with all listing options

            List<string> ListOptions = new List<string>
            {
                "List all tasks", "List all tasks assigned to a specific person","List all tasks due before a user-specified date"
            };

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            for (int i = 0; i < ListOptions.Count; i++)
            {
                Console.WriteLine($"{(i + 1)})\t{ListOptions[i]}");
            }
            Console.WriteLine();
            Console.ResetColor();
        }
        
        public static void ListTasks(List<Task> taskListToList)
        {
            // Runs listing option based on user input provided
            
            int ListChoice;
            
            PrintListOptions();
            ListChoice = UserChoice($"Please select a listing option: [1-{taskListToList.Count}]", "That is not a valid choice.", taskListToList.Count);

            switch (ListChoice)
            {
                case 1:
                    ListAllTasks(taskListToList);
                    break;
                case 2:
                    ListSingleTask(taskListToList);
                    break;
                case 3:
                    ListTaskFromDate(taskListToList);
                    break;
                default:
                    Console.WriteLine("Not a valid option");
                    break;
            }
        }

        public static void ListTaskFromDate(List<Task> taskListToListFromDate)
        {
            // Prints the list of tasks before the user specified date

            string selectDate;
            DateTime fromDate;
            int taskCount = 0;

            selectDate = GetUserInput("Please enter date. All tasks due before this date will be listed. [mm/dd/yyyy] ");
            if (!DateTime.TryParse(selectDate, out fromDate))
            {
                SetOutputColor();
                Console.WriteLine("This is not a valid date.");
                ListTaskFromDate(taskListToListFromDate);
            }
            else
            {
                for (int i = 0; i < taskListToListFromDate.Count; i++)
                {
                    SetOutputColor();
                    if (taskListToListFromDate[i].DueDate <= fromDate)
                    {
                        taskCount++;
                        if (taskCount == 1)
                        {
                            Console.WriteLine($"\nTask #\tMember Name\tDue Date\tTask Completed?\t\tTask Description");
                        }
                        Console.WriteLine($"{taskCount})\t{taskListToListFromDate[i].MemberName}\t\t{taskListToListFromDate[i].DueDate.ToShortDateString()}\t\t{taskListToListFromDate[i].TaskCompleted}\t\t{taskListToListFromDate[i].TaskDescription}");
                    }
                }
                if (taskCount == 0)
                {
                    Console.WriteLine($"There are no tasks due before {fromDate.ToShortDateString()}.");
                }

            }
        }

        public static void ListSingleTask(List<Task> taskListToListOne)
        {

            // Prompts user to enter a name. Prints out a list of tasks assigned to that name.

            string selectName;
            int nameCount = 0;

            selectName = GetUserInput("Please enter name of person whose tasks you'd like to list: ");
            for (int i = 0; i < taskListToListOne.Count; i++)
            {
                SetOutputColor();
                if (taskListToListOne[i].MemberName == selectName)
                {
                    nameCount++;
                    if (nameCount == 1)
                    {
                        Console.WriteLine($"\nTask #\tMember Name\tDue Date\tTask Completed?\t\tTask Description");
                    }
                    Console.WriteLine($"{nameCount})\t{taskListToListOne[i].MemberName}\t\t{taskListToListOne[i].DueDate.ToShortDateString()}\t\t{taskListToListOne[i].TaskCompleted}\t\t{taskListToListOne[i].TaskDescription}");
                }
            }
            if (nameCount == 0)
            {
                Console.WriteLine($"There are no tasks listed for {selectName}.");
            }

        }
        public static void ListAllTasks(List<Task> taskListToListAll)
        {
            // Prints list of all tasks entered

            SetOutputColor();
            Console.WriteLine($"\nTask #\tMember Name\tDue Date\tTask Completed?\t\tTask Description");
            for (int i = 0; i < taskListToListAll.Count; i++)
            {
                Console.WriteLine($"{i + 1})\t{taskListToListAll[i].MemberName}\t\t{taskListToListAll[i].DueDate.ToShortDateString()}\t\t{taskListToListAll[i].TaskCompleted}\t\t{taskListToListAll[i].TaskDescription}");
            }
        }

        public static void AddTasks(List<Task> taskListToAdd)
        {
            // Allows user to enter a new task into the task list
            
            DateTime newDueDate = DateTime.Now;
            bool dateGood = false;

            string newTaskDescription = GetUserInput("Please enter a description for this task: ");
            string newTaskName = GetUserInput("Please enter a name to assign task to: ");
            while(!dateGood)
            {
                dateGood=DateTime.TryParse(GetUserInput("Please enter due date for this task: "), out newDueDate);
                if(!dateGood)
                {
                    SetOutputColor();
                    Console.WriteLine("That is an invalid date.  Please try again.\n");
                }
            }
            taskListToAdd.Add(new Task(newTaskName, newTaskDescription, newDueDate));
        }

        public static void DeleteTasks(List<Task> taskListToDelete)
        {
            // Allows user to delete a task from the task list

            string deleteForSure;
            
            Console.Clear();
            SetOutputColor();
            ListAllTasks(taskListToDelete);
            int deleteUserNum = UserChoice($"Which task would you like to delete? [1-{taskListToDelete.Count}]","Invalid input. ", taskListToDelete.Count);
            SetOutputColor();
            Console.WriteLine($"\nTask #\tMember Name\tDue Date\tTask Completed?\t\tTask Description");
            Console.WriteLine($"{deleteUserNum})\t{taskListToDelete[deleteUserNum - 1].MemberName}\t\t{taskListToDelete[deleteUserNum - 1].DueDate.ToShortDateString()}\t\t{taskListToDelete[deleteUserNum - 1].TaskCompleted}\t\t{taskListToDelete[deleteUserNum - 1].TaskDescription}\n");
            deleteForSure = YN_UserChoice("Are you sure you want to delete this user? [y/n]", "Please enter [y/n]");
            if (deleteForSure == "y")
            {
                taskListToDelete.RemoveAt(deleteUserNum - 1);
                Console.WriteLine("\nTask Deleted.");
                ListAllTasks(taskListToDelete);
            }
        }

        public static void CompleteTasks(List<Task> taskListToComplete)
        {
            // Allows user to mark task complete

            string completeForSure;

            Console.Clear();
            SetOutputColor();
            ListAllTasks(taskListToComplete);
            int completeUserNum = UserChoice($"Which task would you like to mark complete? [1-{taskListToComplete.Count}]", "Invalid input. ", taskListToComplete.Count);
            SetOutputColor();
            Console.WriteLine($"\nTask #\tMember Name\tDue Date\tTask Completed?\t\tTask Description");
            Console.WriteLine($"{completeUserNum})\t{taskListToComplete[completeUserNum - 1].MemberName}\t\t{taskListToComplete[completeUserNum - 1].DueDate.ToShortDateString()}\t\t{taskListToComplete[completeUserNum - 1].TaskCompleted}\t\t{taskListToComplete[completeUserNum - 1].TaskDescription}\n");
            completeForSure = YN_UserChoice("Are you sure you want to mark this task complete? [y/n]", "Please enter [y/n]");
            if (completeForSure == "y")
            {
                taskListToComplete[completeUserNum - 1].TaskCompleted = true;
                Console.WriteLine("\nTask mark completed.");
                ListAllTasks(taskListToComplete);
            }
        }

        public static void EditTasks(List<Task> taskListToEdit)
        {
            // Allows user to edit all properties of a specific task
            string editForSure;
            bool dateGood = false;
            DateTime newDueDate = DateTime.Now;

            Console.Clear();
            SetOutputColor();
            ListAllTasks(taskListToEdit);
            int editTaskNum = UserChoice($"Which task would you like to edit? [1-{taskListToEdit.Count}]", "Invalid input. ", taskListToEdit.Count);
            SetOutputColor();
            Console.WriteLine($"\nTask #\tMember Name\tDue Date\tTask Completed?\t\tTask Description");
            Console.WriteLine($"{editTaskNum})\t{taskListToEdit[editTaskNum - 1].MemberName}\t\t{taskListToEdit[editTaskNum - 1].DueDate.ToShortDateString()}\t\t{taskListToEdit[editTaskNum - 1].TaskCompleted}\t\t{taskListToEdit[editTaskNum - 1].TaskDescription}\n");
            editForSure = YN_UserChoice("Are you sure you want to edit this task? [y/n]", "Please enter [y/n]");
            if (editForSure == "y")
            {
                if ((YN_UserChoice($"Would you like to edit the assigned name [{taskListToEdit[editTaskNum - 1].MemberName}]?", "Please enter [y/n]")) == "y")
                {
                    taskListToEdit[editTaskNum - 1].MemberName = GetUserInput("Please enter a new name");
                }
                if ((YN_UserChoice($"Would you like to edit the task description [{taskListToEdit[editTaskNum - 1].TaskDescription}]?", "Please enter [y/n]")) == "y")
                {
                    taskListToEdit[editTaskNum - 1].TaskDescription = GetUserInput("Please enter a new task description");
                }
                if ((YN_UserChoice($"Would you like to modify the due date [{taskListToEdit[editTaskNum - 1].DueDate.ToShortDateString()}]?", "Please enter [y/n]")) == "y")
                {
                    while (!dateGood)
                    {
                        dateGood = DateTime.TryParse(GetUserInput("Please enter due date for this task: "), out newDueDate);
                        if (!dateGood)
                        {
                            SetOutputColor();
                            Console.WriteLine("That is an invalid date.  Please try again.\n");
                        }
                    }
                    taskListToEdit[editTaskNum - 1].DueDate = newDueDate;
                }
                if ((YN_UserChoice($"Would you like to change the completion status [{taskListToEdit[editTaskNum - 1].TaskCompleted}]?", "Please enter [y/n]")) == "y")
                {
                    if (taskListToEdit[editTaskNum - 1].TaskCompleted == true)
                    {
                        taskListToEdit[editTaskNum - 1].TaskCompleted = false;
                    }
                    else
                    {
                        taskListToEdit[editTaskNum - 1].TaskCompleted = true;
                    }
                }
                Console.WriteLine("\nTask edit completed.\n");
                ListAllTasks(taskListToEdit);
            }
        }
    }
}
