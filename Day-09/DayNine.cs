using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class DayNine : IDay
    {
        public int NumberOfExecutions { get; set; }

        public async Task<string> Execute()
        {
            var data = await ((IDay)this).LoadData();
            var dataLines = data.Split($"{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries).ToArray();

            var preableLength = 25;
            var currentIndex = preableLength;
            var dataValues = dataLines.Select(x => long.Parse(x)).ToList();
            Logger.Information($"Found {dataValues.Count()} numbers");

            while(IsSumOfTwoPreviousNumbers(currentIndex, preableLength, dataValues))
            { 
                currentIndex++;
            }
            Logger.Information($"First number that does not have this property is {dataValues[currentIndex]}");
            
            var numberOfContiguousNumbers = 2;
            while(true)
            {
                (bool IsSum, int Index) = IsSumOfContiguousNumbersMatch(0, dataValues, numberOfContiguousNumbers, 26134589);

                if(IsSum)
                {
                    var selectedValues = dataValues.Skip(Index).Take(numberOfContiguousNumbers);
                    Logger.Debug($"Contiguous set is {string.Join("-", selectedValues)}");
                    Logger.Debug($"Sum of contiguous set is {selectedValues.Sum()}");
                    Logger.Information($"From contiguoug set min is {selectedValues.Min()} and max is {selectedValues.Max()} and sum is {selectedValues.Min() + selectedValues.Max()}");
                    break;
                }
                else
                {
                    numberOfContiguousNumbers++;
                }
            }


            return "done";            
        }

        private bool IsSumOfTwoPreviousNumbers(int currentIndex, int preableLength, List<long> data)
        {
            for(int i = currentIndex - preableLength; i < data.Count - 1; i++)
            {
                for(int j = currentIndex - preableLength + 1; j < data.Count; j++)
                {
                    if(data[i] + data[j] == data[currentIndex])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private (bool IsSum, int Index) IsSumOfContiguousNumbersMatch(int startingIndex, List<long> data, int numberOfContiguousNumbers, long numberToLookFor)
        {

            var selectedNumbers = data.Skip(startingIndex).Take(numberOfContiguousNumbers);
            
            while(!selectedNumbers.Contains(numberToLookFor) && selectedNumbers.Sum() != numberToLookFor)
            {
                startingIndex++;
                selectedNumbers = data.Skip(startingIndex).Take(numberOfContiguousNumbers);
            }
            
            return (selectedNumbers.Sum() == numberToLookFor, startingIndex);
        }
    }
}