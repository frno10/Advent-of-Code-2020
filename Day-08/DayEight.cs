using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class DayEight : IDay
    {
        public int NumberOfExecutions { get; set; }

        public async Task<string> Execute()
        {
            var data = await ((IDay)this).LoadData();
            var dataLines = data.Split($"{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries).ToArray();
            bool isAttamptingSwitch = false;

            var visitedIndexed = new List<int>();
            var currentIndex = 0;
            long accValue = 0;
            (int Index, long Value, List<int> VisitedIndexes) lastSwitchLocation = (0, 0, new List<int>());

            while(true)
            {
                visitedIndexed.Add(currentIndex);
                var lineSplit = dataLines[currentIndex].Split(" ");
                (string instruction, int value) = (lineSplit[0], int.Parse(lineSplit[1]));

                if(!isAttamptingSwitch && (instruction == "jmp" || instruction == "nop" ))
                {
                    isAttamptingSwitch = true;
                    lastSwitchLocation = (currentIndex, accValue, visitedIndexed.Select(x => x).ToList());
                    instruction = instruction == "jmp" ? "nop" : "jmp";
                    Logger.Information($"switching to {instruction} on index {currentIndex}");
                }
                else if(isAttamptingSwitch && currentIndex == lastSwitchLocation.Index)
                {
                    Logger.Information($"- resetting {isAttamptingSwitch} -");
                    isAttamptingSwitch = false;
                }

                Logger.Information($"{instruction} {value} | {accValue} | {visitedIndexed.Count} | {currentIndex}");
                if(instruction == "acc")
                {
                    accValue += value;
                    currentIndex++;
                }
                if(instruction == "jmp")
                {
                    currentIndex += value;
                }
                if(instruction == "nop")
                {
                    currentIndex++;
                }

                if(visitedIndexed.Contains(currentIndex))
                {
                    Logger.Information($"existing index {currentIndex}!!!");
                    currentIndex = lastSwitchLocation.Index;
                    accValue = lastSwitchLocation.Value;
                    visitedIndexed = lastSwitchLocation.VisitedIndexes;
                }

                if(currentIndex >= dataLines.Length)
                {
                    break;
                }
            }

            return $"Before repeating instruction acc equals to {accValue}";            
        }
    }
}