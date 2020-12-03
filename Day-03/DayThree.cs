using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class DayThree : IDay
    {
        public int NumberOfExecutions { get; set; }

        public async Task<string> Execute()
        {
            var data = await ((IDay)this).LoadData();
            var dataLines = data.Split(Environment.NewLine).ToArray();
            Console.WriteLine($"Found {dataLines.Length} lines");
            int laneWidth = dataLines[0].Length;
            var occurences = new List<SlopeRide>()
            {
                new SlopeRide(1, 1),
                new SlopeRide(3, 1),
                new SlopeRide(5, 1),
                new SlopeRide(7, 1),
                new SlopeRide() { yPositionIncrease = 2, xPositionIncrease = 1, xPosition = 1
                 }
            };
            Console.WriteLine($"   Lane width is {laneWidth}");
            
            for(var first = 1; first < dataLines.Length; first++)
            {
                foreach(var occurence in occurences)
                {
                    if(first % occurence.yPositionIncrease == 0)
                    {
                        if(dataLines[first][occurence.xPosition % laneWidth] == '#')
                        {
                            occurence.NumberOfTreesHit++;
                        }
                        occurence.xPosition += occurence.xPositionIncrease;
                    }
                }                
            }
                
            occurences.ForEach(x => Console.WriteLine($"Hit {x.NumberOfTreesHit} trees with [{x.xPositionIncrease}, {x.yPositionIncrease}]"));  
            double result = ((double)occurences[0].NumberOfTreesHit)
                * ((double)occurences[1].NumberOfTreesHit)
                * ((double)occurences[2].NumberOfTreesHit)
                * ((double)occurences[3].NumberOfTreesHit)
                * ((double)occurences[4].NumberOfTreesHit);

            return $"Result is {result}";
        }

        class SlopeRide
        {
            public SlopeRide() {}
            public SlopeRide(int x, int y)
            {
                xPosition = x;
                xPositionIncrease = x;
                yPositionIncrease = y;
            }

            public int xPositionIncrease { get; set; }
            public int yPositionIncrease { get; set;}
            public int xPosition { get; set; }

            public int NumberOfTreesHit { get; set; } = 0;
        }
    }
}