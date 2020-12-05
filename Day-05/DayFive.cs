using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class DayFive : IDay
    {
        public int NumberOfExecutions { get; set; }

        public async Task<string> Execute()
        {
            var data = await ((IDay)this).LoadData();
            var dataLines = data.Split($"{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries).ToArray();
            Logger.Information($"Found {dataLines.Length} seat tickets");
            var biggestResult = 0;
            Dictionary<int, List<int>> allSeats = new Dictionary<int, List<int>>();
            
            //Enumerable.Range(1, 128).ToList().ForEach(x => allSeats.Add(x, Enumerable.Range(1, 8).ToList()));
            // foreach(var dataLine in dataLines)
            // {
            //     var planeRows = new Tuple<int, int>(1, 128);
            //     var planeSeats = new Tuple<int, int>(1, 8);

            //     for(int rowEliminations = 0; rowEliminations < 6; rowEliminations++)
            //     {
            //         if(dataLine[rowEliminations] == 'F')
            //         {
            //             planeRows = new Tuple<int, int>(planeRows.Item1, (planeRows.Item1 + planeRows.Item2) / 2);
            //         }
            //         if(dataLine[rowEliminations] == 'B')
            //         {
            //             planeRows = new Tuple<int, int>((planeRows.Item1 + planeRows.Item2) / 2, planeRows.Item2);
            //         }
            //     }
            //     for(int seatEliminations = 6; seatEliminations < dataLine.Length; seatEliminations++)
            //     {
            //         if(dataLine[seatEliminations] == 'L')
            //         {
            //             planeSeats = new Tuple<int, int>(planeSeats.Item1, (planeSeats.Item1 + planeSeats.Item2) / 2);
            //         }
            //         if(dataLine[seatEliminations] == 'R')
            //         {
            //             planeSeats = new Tuple<int, int>((planeSeats.Item1 + planeSeats.Item2) / 2, planeSeats.Item2);
            //         }
            //     }
            //     var result = planeRows.Item1 * 8 + planeSeats.Item1;
            //     Logger.Debug($"Ticket {dataLine} is row {planeRows.Item1} and " +
            //         $"seat {planeSeats.Item1} , seatID {result}");

            //     allSeats[planeRows.Item1].Remove(planeSeats.Item1);

            //     if(result > biggestResult)
            //     {
            //         biggestResult = result;
            //     }
            // }

            // foreach(var seats in allSeats)
            // {
            //     Logger.Information($"Remaining - row {seats.Key} and seat(s) {string.Join(',', seats.Value)}");
            // }

            // Logger.Information("-----------");

            dataLines = dataLines.Select(x => x
                .Replace('B', '1')
                .Replace('R', '1')
                .Replace('F', '0')
                .Replace('L', '0')
                )
                .OrderBy(x => x)
                .ToArray();

            var maxRow = Convert.ToInt32(dataLines.Last().Substring(0, 7), 2);
            var maxSeat = Convert.ToInt32(dataLines.Last().Substring(7, 3), 2);
            Logger.Information($"Biggest seatID, new way, is {maxRow * 8 + maxSeat}");
            
            allSeats.Clear();
            Enumerable.Range(0, 128).ToList().ForEach(x => allSeats.Add(x, Enumerable.Range(0, 8).ToList()));

            foreach(var dataLine in dataLines)
            {
                (int Row, int Seat) = ( Convert.ToInt32(dataLine.Substring(0, 7), 2), Convert.ToInt32(dataLine.Substring(7, 3), 2));
                allSeats[Row].Remove(Seat);
            }

            foreach(var seats in allSeats)
            {
                Logger.Information($"Remaining - row {seats.Key} and seat(s) {string.Join(',', seats.Value)}");
            }
            
            return $"Biggest seatID is {biggestResult}";
        }
    }
}