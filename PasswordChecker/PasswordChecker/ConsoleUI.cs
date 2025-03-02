using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace AccountPractice
{
    class ConsoleUI
    {
        public static void SetColour(ConsoleColor foreground, ConsoleColor background = ConsoleColor.Black)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
        }

        public static void DrawBox(string content, bool centered = false, bool clearConsole = true, int startX = 1, int startY = 1, int padding = 2, ConsoleColor contentForegroundColor = ConsoleColor.White)
        {
            if (clearConsole)
                Console.Clear();

            int width = Console.WindowWidth - 4;

            string[] lines = content.Split('\n');
            int maxLineLength = lines.Max(line => line.Length);
            int boxWidth = Math.Max(Math.Min(width, maxLineLength + padding * 2), maxLineLength);

            Console.SetCursorPosition(startX, startY);
            SetColour(ConsoleColor.DarkCyan);
            Console.Write("╔");
            for (int i = 0; i < boxWidth; i++)
                Console.Write("═");
            Console.WriteLine("╗");

            for (int i = 0; i < lines.Length; i++)
            {
                Console.SetCursorPosition(startX, startY + 1 + i);
                SetColour(ConsoleColor.DarkCyan);
                Console.Write("║");

                SetColour(contentForegroundColor);

                if (centered)
                {
                    int spaces = Math.Max(0, (boxWidth - lines[i].Length) / 2);
                    Console.Write(new string(' ', spaces) + lines[i] + new string(' ', Math.Max(0, boxWidth - spaces - lines[i].Length)));
                }
                else
                    Console.Write(new string(' ', padding) + lines[i].PadRight(boxWidth - padding * 2) + new string(' ', padding));

                Console.ResetColor();
                SetColour(ConsoleColor.DarkCyan);
                Console.WriteLine("║");
            }

            Console.SetCursorPosition(startX, startY + 1 + lines.Length);
            SetColour(ConsoleColor.DarkCyan);
            Console.Write("╚");
            for (int i = 0; i < boxWidth; i++)
                Console.Write("═");
            Console.Write("╝");

            Console.ResetColor();
            Console.WriteLine();
        }

        public static void DisplayMessageBox(string message, int displayMilliseconds = 1500, ConsoleColor foregroundColor = ConsoleColor.White)
        {
            Console.Clear();

            DrawBox(message, true, true, 1, 1, 2, foregroundColor);

            Thread.Sleep(displayMilliseconds);
            Console.Clear();
        }

        public static void Pause()
        {
            Console.WriteLine();
            SetColour(ConsoleColor.White);
            Console.WriteLine("Press any key to continue...");
            Console.ResetColor();
            Console.ReadKey(true);
            Console.Clear();
        }

        public static string GetInput(string prompt = "", int startX = 1, int startY = 1, bool sensitive = false)
        {
            string input = string.Empty;

            if (!string.IsNullOrEmpty(prompt))
                DrawBox(prompt, true, false, startX, startY, 1, ConsoleColor.White);
            Console.SetCursorPosition(startX, startY + 3);
            SetColour(ConsoleColor.Green);
            Console.Write("> ");
            Console.ResetColor();
            Console.CursorVisible = true;

            if (sensitive)
            {
                ConsoleKeyInfo key;
                while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
                {
                    if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                    {
                        input = input[..^1];
                        Console.Write("\b \b");
                    }

                    else if (!char.IsControl(key.KeyChar))
                    {
                        input += key.KeyChar;
                        Console.Write("*");
                    }
                }
            }

            else
            {
                do
                {
                    Console.SetCursorPosition(startX + 3, startY + 3);
                    input = Console.ReadLine()!;
                } while (string.IsNullOrEmpty(input));
            }

            Console.CursorVisible = false;
            return input;
        }

        public static int SelectionMenu(List<string> options, string title = "", int startX = 2, int startY = 3)
        {
            int currentSelection = 0;
            bool selectionMade = false;
            ConsoleKey key;

            while (!selectionMade)
            {
                if (!string.IsNullOrEmpty(title))
                    DrawBox(title, false, true, startX, startY, 2, ConsoleColor.White);
                Console.ResetColor();

                for (int i = 0; i < options.Count; i++)
                {
                    Console.SetCursorPosition(startX + 1, startY + i + 3);

                    if (i == currentSelection)
                    {
                        SetColour(ConsoleColor.Black, ConsoleColor.Blue);
                        Console.Write(" > " + options[i].PadRight(Console.WindowWidth - 6) + " ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.Write("   " + options[i]);
                    }
                }

                Console.CursorVisible = false;
                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        currentSelection = Math.Max(0, currentSelection - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        currentSelection = Math.Min(options.Count - 1, currentSelection + 1);
                        break;
                    case ConsoleKey.Enter:
                        selectionMade = true;
                        break;
                }
            }

            return currentSelection;
        }

        public static string ProgressBar(double percentage, int length = 20)
        {
            int filledLength = (int)Math.Round(length * percentage / 100);
            return "[" + new string('█', filledLength) + new string('░', length - filledLength) + "]";
        }

        public static void DrawInputBox(string prompt, int startY)
        {
            DrawBox(prompt, false, false, 1, startY);
            Console.SetCursorPosition(1, startY + 3);
            SetColour(ConsoleColor.Green);
            Console.Write("> ");
            Console.ResetColor();
            Console.CursorVisible = true;
        }
    }
}
