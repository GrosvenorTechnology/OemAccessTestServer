using System;

namespace Itac.OemAccess.TestingServer
{
    public static class Log
    {
        public static void Trace(string message)
        {
            lock (Console.Out)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{DateTime.Now:G}] Trace : {message}");
                Console.ResetColor();
            }
        }
        public static void Trace<T>(T variable)
        {   
            Console.WriteLine($"[{DateTime.Now:G}] {variable.GetType()} : {variable.ToString()}");
        }

        public static void Hub(string message)
        {
            lock (Console.Out)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"[{DateTime.Now:G}] Hub : {message}");
                Console.ResetColor();
            }
        }

        public static void ConfigUpdate(string message)
        {
            Console.WriteLine($"[{DateTime.Now:G}] Configuration Update : {message}");
        }

        public static void ConnectionEven(string message)
        {
            lock (Console.Out)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[{DateTime.Now:G}] Connection Event : {message}");
                Console.ResetColor();
            }
        }
        public static void Error(string message)
        {
            lock (Console.Out)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"[{DateTime.Now:G}] ERROR : {message}");
                Console.ResetColor();
            }
        }

    }
}
