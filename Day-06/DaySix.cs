using System;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class DaySix : IDay
    {
        public int NumberOfExecutions { get; set; }

        public async Task<string> Execute()
        {
            var data = await ((IDay)this).LoadData();
            var dataLines = data.Split($"{Environment.NewLine}{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries).ToArray();
            int sum = 0, sum2 = 0;

            foreach(var group in dataLines)
            {
                var groupChars = group.Replace(Environment.NewLine, "").Replace(" ", "").ToCharArray().Distinct().ToArray();
                Logger.Debug($"Unique chars in group - {groupChars}");
                sum+= groupChars.Length;
            }

            foreach(var group in dataLines)
            {
                var groupMembers = group.Split(Environment.NewLine);
                var groupA = group.Replace(Environment.NewLine, "").Replace(" ", "");
                Logger.Debug($"merged into - {groupA}");

                while(groupA.Length > 0)
                {
                    var originalLength = groupA.Length;
                    groupA = groupA.Replace(groupA.Substring(0, 1), "");
                    var newLength = groupA.Length;
                    if(originalLength - newLength == groupMembers.Length)
                    {
                        sum2++;
                    }
                }
            }

            return $"Sum of unique group letters is {sum}, new sum is {sum2}";
        }
    }
}