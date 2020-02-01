using System;


namespace GC_CS_Capstone_2_TaskList
{
    class MyMethods
    {

        public static string TryAgain(string message)
        {
            // Method for running program again.  Passes back to do while loop in main.
            string userChoice = GetUserInput(message).Trim().ToLower();
            while ((userChoice != "y") && (userChoice != "n"))
            {
                userChoice = GetUserInput("Please enter 'y' or 'n'.  [y/n]").Trim().ToLower();
            }
            return userChoice;
        }
        public static string GetUserInput(string message)
        {
            // Allows for program prompt and user input (string)
            SetOutputColor();
            Console.WriteLine(message);
            SetInputColor();
            return Console.ReadLine();
        }

        public static void ShowTitle(string title)
        {
            // This method simply shows the title
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"{title} \n\n");
        }

        public static void SetInputColor()
        {
            // Method for setting colors for user inputted text
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void SetOutputColor()
        {
            // Method for setting colors for default display text
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }

}
