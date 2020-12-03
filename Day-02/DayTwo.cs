using System;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class DayTwo : IDay
    {
        public int NumberOfExecutions { get; set; }

        public async Task<string> Execute()
        {
            var data = await ((IDay)this).LoadData();
            var dataLines = data.Split(Environment.NewLine).ToArray();
            Console.WriteLine($"Found {dataLines.Length} lines");
            int numberOfCorrectPasswords = 0, numberOfCorrectNewPasswords = 0;

            for(var first = 0; first < dataLines.Length; first++)
            {
                Console.WriteLine($"Considering following: {dataLines[first]}");
                var (Min, Max) = GetMinMaxOccurences(dataLines[first]);
                var letterToFind = GetLetterToFind(dataLines[first]);

                var valueToConsider = dataLines[first].Split(' ')[2];
                var occurences = valueToConsider.Count(x => x == letterToFind);
                if(occurences >= Min && occurences <= Max)
                {
                    Console.WriteLine($"   Its a correct password !");
                    numberOfCorrectPasswords++;
                }
                if((valueToConsider[Min - 1] == letterToFind && valueToConsider[Max - 1] != letterToFind)
                || (valueToConsider[Min - 1] != letterToFind && valueToConsider[Max - 1] == letterToFind))
                {
                    numberOfCorrectNewPasswords++; 
                }
            }
                        
            return $"Found {numberOfCorrectPasswords} correct passwords and {numberOfCorrectNewPasswords} correct new passwords";
        }

        private (int Min,int Max) GetMinMaxOccurences(string inputString)
        {
            string[] dataSplit = inputString.Split(' ')[0].Split('-');
            Console.WriteLine($"   Found range: from {dataSplit[0]} to {dataSplit[1]}");
            return (int.Parse(dataSplit[0]), int.Parse(dataSplit[1]));
        }

        private char GetLetterToFind(string inputString)
        {
            char foundChar = inputString.Split(' ')[1][0];
            Console.WriteLine($"   Found character: {foundChar}");
            return foundChar;
        }
    }
}