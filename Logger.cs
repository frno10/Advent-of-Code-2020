using Serilog;

namespace AdventOfCode
{
    public static class Logger
    {
        public static void Verbose(string message) => Log.Verbose(message);

        public static void Debug(string message) => Log.Debug(message);

        public static void Information(string message) => Log.Information(message);

        public static void Warning(string message) => Log.Warning(message);

        public static void Error(string message) => Log.Error(message);

        public static void Fatal(string message) => Log.Fatal(message);
    }
}
