using System;

namespace MovieStreaming
{
    public static class ColorConsole
    {
        public static void WriteLineInColor(string message, ConsoleColor color)
        {
            var beforeColor = Console.ForegroundColor;

            Console.ForegroundColor = color;

            Console.WriteLine(message);

            Console.ForegroundColor = beforeColor;
        }
    }
}
