using System;
using System.Collections.Generic;
using static GC_CS_Capstone_2_TaskList.MyMethods;

namespace GC_CS_Capstone_2_TaskList
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Task> taskList = new List<Task>
            {
                new Task($"Erwin","Complete Capstone", DateTime.Parse("2/2/2020"),false),
                new Task($"Erwin","Present at demo day",DateTime.Parse("3/27/2020"),false),
                new Task($"Bob","Attend demo day",DateTime.Parse("3/27/2020"), false)
            };
            
            int progOption = 0;

            ShowTitle("Welcome to the Task Manager!");

            do
            {
                DisplayMenu();
                progOption = UserChoice("What would you like to do?","This is not a valid choice. Enter a number from [1-5]\n",5);
                runOption(progOption, taskList);
            }
            while (progOption != 5);

        }

        public static void DisplayMenu()
        {
            SetOutputColor();
            Console.WriteLine("__________________________________________________");
            Console.WriteLine($"1)\tList Tasks");
            Console.WriteLine($"2)\tAdd Task");
            Console.WriteLine($"3)\tDelete Task");
            Console.WriteLine($"4)\tMark Task Complete");
            Console.WriteLine($"5)\tQuit");
            Console.WriteLine("__________________________________________________\n");
        }

        public static int UserChoice(string msg, string errorMsg, int limit)
        {
            int userOption;

            // check if input is a valid integer
            if (int.TryParse(GetUserInput(msg), out userOption))
            {
                // check if integer input is valid
                if ((userOption <= 0) || (userOption > limit))
                {
                    SetOutputColor();
                    Console.WriteLine(errorMsg);
                    return UserChoice(msg, errorMsg, limit);
                }
                return userOption;
            }
            else
            {
                // if option is NOT an integer
                SetOutputColor();
                Console.WriteLine(errorMsg);
                return UserChoice(msg, errorMsg, limit);
            }
        }

        public static string YN_UserChoice(string msg, string errorMsg)
        {
            SetOutputColor();
            string userOption = GetUserInput(msg).ToLower();
            // check if "Y" input is valid
            if ((userOption != "y") && (userOption != "n"))
            {
                SetOutputColor();
                Console.WriteLine(errorMsg);
                return YN_UserChoice(msg, errorMsg);
            }
            return userOption;
        }

        public static void runOption(int programOption, List<Task> taskListToRun)
        {
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
                default:
                    break;
            }

        }

        public static void ListTasks(List<Task> taskListToList)
        {
            SetOutputColor();
            Console.WriteLine($"\nTask #\tMember Name\tDue Date\tTask Completed?\t\tTask Description");
            for (int i = 0; i < taskListToList.Count; i++)
            {
                Console.WriteLine($"{i + 1})\t{taskListToList[i].MemberName}\t\t{taskListToList[i].DueDate.ToShortDateString()}\t\t{taskListToList[i].TaskCompleted}\t\t{taskListToList[i].TaskDescription}");
            }
        }

        public static void AddTasks(List<Task> taskListToAdd)
        {
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
            string deleteForSure;
            
            Console.Clear();
            ListTasks(taskListToDelete);
            int deleteUserNum = UserChoice($"Which task would you like to delete? [1-{taskListToDelete.Count}]","Invalid input. ", taskListToDelete.Count);
            SetOutputColor();
            Console.WriteLine($"\nTask #\tMember Name\tDue Date\tTask Completed?\t\tTask Description");
            Console.WriteLine($"{deleteUserNum - 1})\t{taskListToDelete[deleteUserNum - 1].MemberName}\t\t{taskListToDelete[deleteUserNum - 1].DueDate.ToShortDateString()}\t\t{taskListToDelete[deleteUserNum - 1].TaskCompleted}\t\t{taskListToDelete[deleteUserNum - 1].TaskDescription}\n");
            deleteForSure = YN_UserChoice("Are you sure you want to delete this user? [y/n]", "Please enter [y/n]");
            if (deleteForSure == "y")
            {
                taskListToDelete.RemoveAt(deleteUserNum - 1);
                Console.WriteLine("\nTask Deleted.");
                ListTasks(taskListToDelete);
            }
        }

        public static void CompleteTasks(List<Task> taskListToComplete)
        {
            string completeForSure;

            Console.Clear();
            ListTasks(taskListToComplete);
            int completeUserNum = UserChoice($"Which task would you like to mark complete? [1-{taskListToComplete.Count}]", "Invalid input. ", taskListToComplete.Count);
            SetOutputColor();
            Console.WriteLine($"\nTask #\tMember Name\tDue Date\tTask Completed?\t\tTask Description");
            Console.WriteLine($"{completeUserNum - 1})\t{taskListToComplete[completeUserNum - 1].MemberName}\t\t{taskListToComplete[completeUserNum - 1].DueDate.ToShortDateString()}\t\t{taskListToComplete[completeUserNum - 1].TaskCompleted}\t\t{taskListToComplete[completeUserNum - 1].TaskDescription}\n");
            completeForSure = YN_UserChoice("Are you sure you want to mark this task complete? [y/n]", "Please enter [y/n]");
            if (completeForSure == "y")
            {
                taskListToComplete[completeUserNum - 1].TaskCompleted = true;
                Console.WriteLine("\nTask mark completed.");
                ListTasks(taskListToComplete);
            }
        }

    }
}
