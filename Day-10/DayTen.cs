using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class DayTen : IDay
    {
        public int NumberOfExecutions { get; set; }

        public async Task<string> Execute()
        {
            var data = await ((IDay)this).LoadData();
            var dataLines = data.Split($"{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries).ToArray();

            var orderedAndParsedData = dataLines.Select(x => int.Parse(x)).OrderBy(x => x).ToList();
            Logger.Information(string.Join("-", orderedAndParsedData));
            List<int> amounts = new List<int>(3) { 0, 0, 0 } ;

            for(int i = 0; i < orderedAndParsedData.Count - 1; i++)
            {
                amounts[orderedAndParsedData[i+1] - orderedAndParsedData[i] - 1]++;
            }

            Logger.Information($"Adjusted with outlet and device  - {amounts[0]+1}, 3 - {amounts[2]+1}, multiplied - {((amounts[0]+1) * (amounts[2]+1))}"); 

            orderedAndParsedData.Insert(0, 0);
            var multipliers = new List<int>();
            var removableNumbersList = orderedAndParsedData.Select((x, index) => 
                (x > 0 && x < orderedAndParsedData.Last() && x + 1 == orderedAndParsedData[index+1] && x - 1 == orderedAndParsedData[index-1]) ? 1 : 0)
                .ToList();

            Logger.Information(string.Join("-", removableNumbersList));
            
            List<int> groupedRemovableNumbersList = new List<int>() { 0 };
            for(int i = 0; i < removableNumbersList.Count(); i++)
            {
                if(removableNumbersList[i] == 1)
                    groupedRemovableNumbersList[groupedRemovableNumbersList.Count - 1]++;
                else 
                    groupedRemovableNumbersList.Add(0);
            }
            Logger.Information(string.Join("-", groupedRemovableNumbersList));
            double result = 1;
            groupedRemovableNumbersList
                .Select(x => (int)Math.Pow(2, x))
                .Select(x => x == 8 ? 7 : x)
                .Where(x => x != 0)
                .ToList()
                .ForEach(x => result = result * x);

            return $"done - result is {result}";
        }
    }
}