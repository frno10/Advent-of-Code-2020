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
            var list2 = orderedAndParsedData.Select((x, index) => 
                (x > 0 && x < orderedAndParsedData.Last() && x + 1 == orderedAndParsedData[index+1] && x - 1 == orderedAndParsedData[index-1]) ? 1 : 0)
                .ToList();

            Logger.Information(string.Join("-", list2));
            
            List<int> list3 = new List<int>() { 0 };
            for(int i = 0; i < list2.Count(); i++)
            {
                if(list2[i] == 1)
                    list3[list3.Count - 1]++;
                else 
                    list3.Add(0);
            }
            Logger.Information(string.Join("-", list3));
            double result = 1;
            list3
                .Select(x => (int)Math.Pow(2, x))
                .Select(x => x == 8 ? 7 : x)
                .Where(x => x != 0)
                .ToList()
                .ForEach(x => result = result * x);

            return $"done - result is {result}";
        }
    }
}