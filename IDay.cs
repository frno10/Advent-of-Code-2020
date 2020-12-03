using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AdventOfCode
{
    public interface IDay
    {
        [Benchmark]
        Task<string> Execute();

        [Params(1000, 10000)]
        int NumberOfExecutions { get; set; }

        string DataFileName => $"{this.GetType().Name}Data.txt";

        async Task<string> LoadData()
        {
            var dataFilePath = DataFileName;
            if(!File.Exists(DataFileName))
            {
                var files = Directory.GetFiles(Environment.CurrentDirectory, DataFileName, new EnumerationOptions() 
                {
                    RecurseSubdirectories = true
                });
                dataFilePath = (bool)(files?.Any()) ? files.First() : DataFileName;
            }
            if(File.Exists(dataFilePath))
            {
                Console.WriteLine($"Found data file at {dataFilePath}");
                return await File.ReadAllTextAsync(dataFilePath);
            }
            return "";
        } 
    }
}
