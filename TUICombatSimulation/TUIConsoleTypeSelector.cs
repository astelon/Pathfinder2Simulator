using System;
using System.Collections.Generic;

namespace TUI
{
    class ConsoleTypeSelector
    {
        public static string SelectLine(List<string> options_str, string prompt = null, ConsoleColor optionsColor = ConsoleColor.Gray, ConsoleColor promptColor = ConsoleColor.Gray, ConsoleColor typeColor = ConsoleColor.Gray, ConsoleColor predictionColor = ConsoleColor.Gray)
        {
            Console.ForegroundColor = ConsoleColor.White;
            string written = "";
            string predicted = null;
            List<string> matches = null;
            int prevPredLength = 0;
            //Draw the string
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            Console.Clear();
            PrintOptionsWithColor(options_str, optionsColor);
            PrintWithColor(prompt, promptColor);

            ConsoleKeyInfo info = Console.ReadKey(true);

            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    if (info.Key == ConsoleKey.Tab)
                    {
                        if (predicted != null) written = predicted;
                    }
                    else if (info.Key == ConsoleKey.Escape)
                    {
                        //Cancel prediction
                        predicted = null;
                        matches = null;
                    }
                    else
                    {
                        written += info.KeyChar;
                        matches = Find(written, options_str);
                        if (matches.Count > 0)
                        {
                            predicted = matches[0];
                        }
                    }
                }
                else
                {
                    if (written.Length > 0)
                    {
                        written = written.Remove(written.Length - 1); //Remove the last character
                        if (written.Length > 0)
                        {
                            matches = Find(written, options_str);
                            if (matches.Count > 0)
                            {
                                predicted = matches[0];
                            }
                        }
                        else
                        {
                            predicted = null;
                            matches = null;
                        }
                    }
                }
                //Draw the string
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                Console.Clear();
                //Print options or matches
                if ((matches != null) && (matches.Count > 0))
                {
                    PrintOptionsWithColor(matches, optionsColor);
                }
                else
                {
                    PrintOptionsWithColor(options_str, optionsColor);
                }
                int promLength = 0;
                if (prompt != null) promLength = prompt.Length;
                if (predicted != null)
                {
                    prevPredLength = predicted.Length;
                }
                else
                {
                    prevPredLength = 0;
                }
                PrintWithColor(prompt, promptColor);
                PrintWithColor(written, typeColor);
                (int left, int top) = Console.GetCursorPosition();
                if (predicted != null)
                {
                    if (written.Length < predicted.Length)
                    {
                        PrintWithColor(predicted.Substring(written.Length), predictionColor);
                    }
                }
                Console.SetCursorPosition(left, top);
                Console.ResetColor();
                //Read next key:
                info = Console.ReadKey();
            }
            if (predicted != null) written = predicted;
            Console.ResetColor();
            Console.WriteLine(""); //Goto next line
            return written;
        }
        private static void PrintOptionsWithColor(List<string> options, ConsoleColor color)
        {
            foreach (string option in options)
            {
                int level = PF2.MonsterFactory.GetDataByName(option).level;
                string level_str = "";
                level_str += level;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(level_str.PadLeft(3,' ') + " ");
                Console.ForegroundColor = color;
                Console.WriteLine(option);
            }
        }
        private static List<string> Find(string what, List<string> where)
        {
            List<string> matching = new List<string>();
            foreach (string str in where)
            {
                if (str.ToLower().StartsWith(what.ToLower()))
                {
                    matching.Add(str);
                }
            }
            return matching;
        }

        private static void PrintWithColor(string text, ConsoleColor color)
        {
            if (text != null)
            {
                Console.ForegroundColor = color;
                Console.Write(text);
            }
        }
    }
}