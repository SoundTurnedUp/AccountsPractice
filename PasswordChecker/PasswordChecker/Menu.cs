using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountPractice
{
    class Menu
    {
        public void AccountMenu()
        {
            List<string> options = new List<string>()
            {
                "Log in",
                "Sign up",
            };

            int selectionMade = ConsoleUI.SelectionMenu(options, "Log in or Sign up", 1, 1);

            switch (selectionMade)
            {
                case 0: //log in
                    LogInMenu();
                    break;
                case 1: //sign up
                    SignUpMenu();
                    break;
            }
        }

        public void LogInMenu()
        {

            Console.Clear();
            ConsoleUI.DrawInputBox("Enter username", 1);
            ConsoleUI.DrawInputBox("Enter password", 5);

            ConsoleKey key = Console.ReadKey(true).Key;
            string username = string.Empty;
            string password = string.Empty;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    Console.SetCursorPosition(3 + username.Length, 4);
                    break;
                case ConsoleKey.DownArrow:
                    Console.SetCursorPosition(3 + password.Length, 8);
                    break;
                case ConsoleKey.Backspace:
                    break;
                case ConsoleKey.Enter:
                    break;
                default:
                    break;
            }
        }

        public void SignUpMenu()
        {
            Console.Clear();

            string username = ConsoleUI.GetInput("Enter username", 1, 1);

            string password = ConsoleUI.GetInput("Enter password", 1, 6, true);

            string confirmPassword = ConsoleUI.GetInput("Confirm password", 1, 11, true);
        }
    }
}
