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
            Logger.Information($"Looking for data file begins");

            if(!File.Exists(DataFileName))
            {
                var files = Directory.GetFiles(Environment.CurrentDirectory, DataFileName, new EnumerationOptions() 
                {
                    RecurseSubdirectories = true
                });
                dataFilePath = (bool)(files?.Any()) ? files.First() : DataFileName;
            }
            if(!File.Exists(dataFilePath))
            {
                var dirPath = Directory.GetCurrentDirectory();
                var dirInfo = new DirectoryInfo(dirPath);

                while(!File.Exists(dataFilePath) && dirInfo.Parent != null)
                {
                    Logger.Information($"Trying to load data in {dirPath}");

                    var files = Directory.GetFiles(dirPath, DataFileName);
                    if(files.Any())
                    {
                        dataFilePath = files.First();
                    }
                    else
                    {
                        dirPath = dirInfo.Parent?.FullName;
                        dirInfo = new DirectoryInfo(dirPath);
                    }
                }
            }
            if(File.Exists(dataFilePath))
            {
                Logger.Information($"Found data file at {dataFilePath}");
                return await File.ReadAllTextAsync(dataFilePath);
            }
            return ""; //$"1{Environment.NewLine}2";
        } 
    }
}
