using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;

namespace AdventOfCode
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int number = args.Length == 1 && int.TryParse(args[0], System.Globalization.NumberStyles.None, null, out int result)
                ? result
                : DateTime.Now.Day;
            
            string name = IntToNameConverter.Convert(number);
            string className = "Day" + name.ToTitleCase().ToClassname();

            try
            {
                var day = (IDay)Activator.CreateInstance(null, $"AdventOfCode.{className}")?.Unwrap();

                // BenchmarkRunner.Run(day.GetType());

                var dayName = day?.GetType().Name ?? "was null";
                Console.WriteLine($"Today trying : ({className}) {dayName}{Environment.NewLine}");
                Console.WriteLine($"Execution returned: {Environment.NewLine}   {await day.Execute()}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Day not found{Environment.NewLine}{e.Message}");
            }            
        }
    }
}
