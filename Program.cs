using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;
using Serilog;
using Serilog.Core;

namespace AdventOfCode
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            int number = args.Length > 0 && int.TryParse(args[0], System.Globalization.NumberStyles.None, null, out int result)
                ? result
                : DateTime.Now.Day;
            
            string name = IntToNameConverter.Convert(number);
            string className = "Day" + name.ToTitleCase().ToClassname();

            SetupLogging(args, className);

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

        private static void SetupLogging(string[] args, string className)
        {
            var levelSwitch = new LoggingLevelSwitch();
            switch(args.Length > 1 ? args[1].ToLower() : "log:information")
            {
                case "log:verbose" : 
                    levelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Verbose;
                    break;
                case "log:debug" : 
                    levelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Debug;
                    break;
                case "log:information" : 
                    levelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Information;
                    break;
                case "log:warning" : 
                    levelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Warning;
                    break;
                case "log:error" : 
                    levelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Error;
                    break;
                case "log:fatal" : 
                    levelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Fatal;
                    break;
            }
            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.ControlledBy(levelSwitch)
                    .WriteTo.Console()
                    .WriteTo.File($"Logs/{className}.txt")
                    .CreateLogger();
        }
    }
}
