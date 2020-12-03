using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;
using Serilog;

namespace AdventOfCode
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            int number = args.Length == 1 && int.TryParse(args[0], System.Globalization.NumberStyles.None, null, out int result)
                ? result
                : DateTime.Now.Day;
            
            string name = IntToNameConverter.Convert(number);
            string className = "Day" + name.ToTitleCase().ToClassname();

            Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File($"Logs/{className}.txt")
                    .CreateLogger();

            try
            {
                var day = (IDay)Activator.CreateInstance(null, $"AdventOfCode.{className}")?.Unwrap();

                // BenchmarkRunner.Run(day.GetType());

                var dayName = day?.GetType().Name ?? "was null";
                Logger.Information($"Today trying : ({className}) {dayName}");
                Logger.Information($"Execution returned: {Environment.NewLine}   {await day.Execute()}{Environment.NewLine}");

                
            }
            catch (Exception e)
            {
                Logger.Information($"Day not found{Environment.NewLine}{e.Message}");
            }            
        }
    }
}
