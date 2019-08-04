using System;
using static System.Console;

namespace AkkaApp
{
    public static class ColorConsole
    {
        public static void WriteLineGreen(string message)
        {
            var beforeColor = ForegroundColor;

            ForegroundColor = ConsoleColor.Green;

            WriteLine(message);

            ForegroundColor = beforeColor;
        }

        public static void WriteLineYellow(string message)
        {
            var beforeColor = ForegroundColor;

            ForegroundColor = ConsoleColor.Yellow;

            WriteLine(message);

            ForegroundColor = beforeColor;
        }

        public static void WriteLineRed(string message)
        {
            var beforeColor = ForegroundColor;

            ForegroundColor = ConsoleColor.Red;

            WriteLine(message);

            ForegroundColor = beforeColor;
        }

        public static void WriteLineCyan(string message)
        {
            var beforeColor = ForegroundColor;

            ForegroundColor = ConsoleColor.Cyan;

            WriteLine(message);

            ForegroundColor = beforeColor;
        }
    }
}