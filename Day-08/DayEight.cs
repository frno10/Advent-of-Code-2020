using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class DayEight : IDay
    {
        public int NumberOfExecutions { get; set; }

        private Processor processor = null;
        private int firstPartValue = 0;

        public async Task<string> Execute()
        {
            var data = await ((IDay)this).LoadData();
            var DataLines = data.Split($"{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries).ToArray();

            for(int i = 0; i < 2; i++)
            {
                processor = new Processor();
                while(true)
                {
                    processor.MoveToNextStep(DataLines);
                    CheckIfCurrentInstructionShouldSwitch(processor.Current.Instruction);

                    Logger.Debug($"[{firstPartValue}] {processor.Current.Instruction} {processor.Current.Value} | {processor.AccValue} | {processor.VisitedIndexed.Count} | {processor.CurrentIndex}");
                    if(processor.Current.Instruction == "acc")
                    {
                        processor.AccValue += processor.Current.Value;
                        processor.CurrentIndex++;
                    }
                    if(processor.Current.Instruction == "jmp")
                    {
                        processor.CurrentIndex += processor.Current.Value;
                    }
                    if(processor.Current.Instruction == "nop")
                    {
                        processor.CurrentIndex++;
                    }

                    if(processor.VisitedIndexed.Contains(processor.CurrentIndex))
                    {
                        if(firstPartValue == 0)
                        {
                            firstPartValue = (int)processor.AccValue;
                            break;
                        }
                        ResetToLastKnownSwitchLocation();
                    }

                    if(processor.CurrentIndex >= DataLines.Length)
                    {
                        break;
                    }
                }
            }
            return $"Before repeating instruction acc equals to {firstPartValue}, value after program fix is {processor.AccValue}";            
        }

        private void CheckIfCurrentInstructionShouldSwitch(string instruction)
        {
            if(firstPartValue > 0)
            {
                if(!processor.IsAttamptingSwitch && (instruction == "jmp" || instruction == "nop" ))
                {
                    processor.IsAttamptingSwitch = true;
                    processor.LastSwitchLocation = (processor.CurrentIndex, processor.AccValue, processor.VisitedIndexed.Select(x => x).ToList());
                    processor.Current = ((processor.Current.Instruction == "jmp" ? "nop" : "jmp"), processor.Current.Value);
                    Logger.Debug($"switching to {instruction} on index {processor.CurrentIndex}");
                }
                else if(processor.IsAttamptingSwitch && processor.CurrentIndex == processor.LastSwitchLocation.Index)
                {
                    Logger.Debug($"- resetting {processor.IsAttamptingSwitch} -");
                    processor.IsAttamptingSwitch = false;
                }
            }
        }

        private void ResetToLastKnownSwitchLocation()
        {
            Logger.Debug($"existing index {processor.CurrentIndex}!!!");
            processor.CurrentIndex = processor.LastSwitchLocation.Index;
            processor.AccValue = processor.LastSwitchLocation.Value;
            processor.VisitedIndexed = processor.LastSwitchLocation.VisitedIndexes;
        }

        public class Processor 
        {
            public (string Instruction, int Value) Current { get; set; }
            public bool IsAttamptingSwitch { get; set; }
            public List<int> VisitedIndexed { get; set; } = new List<int>();
            public int CurrentIndex { get; set; } = 0;
            public long AccValue { get; set; } = 0;
            public (int Index, long Value, List<int> VisitedIndexes) LastSwitchLocation { get; set; } = (0, 0, new List<int>());

            public void MoveToNextStep(string[] dataLines)
            {
                VisitedIndexed.Add(CurrentIndex);
                var lineSplit = dataLines[CurrentIndex].Split(" ");
                Current = (lineSplit[0], int.Parse(lineSplit[1]));
            }
        }
    }
}