using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode
{
    public class DayOne : IDay
    {
        [Params(1000, 10000)]
        public int NumberOfExecutions { get;set; }

        [Benchmark]
        public async Task<string> Execute()
        {
            var data = await ((IDay)this).LoadData();
            var dataLines = data.Split(Environment.NewLine).Select(x => int.Parse(x)).ToArray();
            Console.WriteLine($"Found {dataLines.Length} lines");
            int resultForTwo = 0, resultForThree = 0;

            for(var first = 0; first < dataLines.Length; first++)
            {
                // Console.WriteLine($"   Comparing {dataLines[first]} with the rest");

                for(var second = first + 1; second < dataLines.Length; second++)
                {
                    if(dataLines[first] + dataLines[second] == 2020)
                    {
                        Console.WriteLine($"Found result for 2: {dataLines[first]} and {dataLines[second]} ");
                        resultForTwo = dataLines[first] * dataLines[second];

                        if(resultForTwo > 0 && resultForThree > 0)
                        {
                            return $"Results are: for 2: {resultForTwo}, for 3: {resultForThree}";
                        }
                    }

                    for(var third = second + 1; third < dataLines.Length; third++)
                    {
                        if(dataLines[first] + dataLines[second] + dataLines[third] == 2020)
                        {
                            Console.WriteLine($"Found result for 3: {dataLines[first]} and {dataLines[second]} and {dataLines[third]}");
                            resultForThree = dataLines[first] * dataLines[second] * dataLines[third];

                            if(resultForTwo > 0 && resultForThree > 0)
                        {
                            return $"Results are: for 2: {resultForTwo}, for 3: {resultForThree}";
                        }
                        }
                    }
                }    
            }
                        
            return "some issue occured :/";
        }
    }
}
