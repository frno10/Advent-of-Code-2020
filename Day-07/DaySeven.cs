using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class DaySeven : IDay
    {
        public int NumberOfExecutions { get; set; }

        private Dictionary<string, List<string>> BagsInBags = new Dictionary<string, List<string>>();
        private List<string> AllMatchingBags = new List<string>() { "shiny gold" };

        public async Task<string> Execute()
        {
            var data = await ((IDay)this).LoadData();
            var dataLines = data.Split($"{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries).ToArray();

            var rules = dataLines.Select(x => GetRule(x)).ToList();
            // rules.ForEach(x => Logger.Information($"Master bag '{x.Bag}', inner " + string.Join("|", x.InnerBags.Select(y => $"{y.Count}-{y.Bag}"))));
            rules.ForEach(x => AddBagAssociation(x));        
            
            int currentIndex = 0;
            while(currentIndex < AllMatchingBags.Count)
            {
                if(BagsInBags.ContainsKey(AllMatchingBags[currentIndex]))
                {
                    BagsInBags[AllMatchingBags[currentIndex]].ForEach(x => AddIfDoesNotContain(x));
                }
                currentIndex++;
            }

            currentIndex = 0;
            List<string> innerBags = new List<string>();
            innerBags.Add("shiny gold");

            while(currentIndex < innerBags.Count)
            {
                Logger.Debug($"Traversing through index {currentIndex} with value {innerBags[currentIndex]}");
                if(rules.Any(x => x.Bag == innerBags[currentIndex]))
                {
                    innerBags.AddRange(GetContainingBags(rules.Single(x => x.Bag == innerBags[currentIndex]).InnerBags));
                }
                currentIndex++;
            }

            Logger.Information($"shiny gold - can appear in {AllMatchingBags.Count - 1} different bags");
            Logger.Information($"shiny gold - contains {innerBags.Count - 1} bags");

            return "done";
        }

        private void AddIfDoesNotContain(string bag)
        {
            if(!AllMatchingBags.Contains(bag))
            {
                AllMatchingBags.Add(bag);
            }
        }

        private (string Bag, IEnumerable<BagAmounts> InnerBags) GetRule(string wholeRule)
        {
            var bagContains = wholeRule
                .Replace(" bags", "")
                .Replace(" bag", "")
                .Split(" contain ", StringSplitOptions.RemoveEmptyEntries);

            var bagType = bagContains[0];
            var containsBags = bagContains[1]
                .Replace(".", "")
                .Replace("no other", "0");
            var containingBags = containsBags
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => GetBag(x));

            return (bagType, containingBags);
        }

        private BagAmounts GetBag(string bagFullText)
        {
            var manipulationString = new ReadOnlySpan<char>(bagFullText.ToCharArray()); 
            var count = int.Parse(manipulationString.Slice(0, 1));
            Logger.Debug($"-- its '{bagFullText}'");
            var bag = bagFullText.Length > 1 
                ? (bagFullText[1] == ' ' ? bagFullText.Substring(2, bagFullText.Length - 2) : bagFullText.Substring(1, bagFullText.Length - 1))
                : "";

            return new BagAmounts() { Count = count, Bag = bag};
        }
    
        private void AddBagAssociation((string Bag, IEnumerable<BagAmounts> InnerBags) x)
        {
            foreach(var innerBag in x.InnerBags)
            {
                if(string.IsNullOrWhiteSpace(innerBag.Bag))
                    continue;

                if(BagsInBags.ContainsKey(innerBag.Bag))
                    BagsInBags[innerBag.Bag].Add(x.Bag);
                else
                    BagsInBags[innerBag.Bag] = new List<string> { x.Bag };
            }
        }
    
        private IEnumerable<string> GetContainingBags(IEnumerable<BagAmounts> innerBagTypes)
        {
            var innerBags = new List<string>();
            foreach(var innerBagType in innerBagTypes)
            {
                if(innerBagType.Count > 0)
                {
                    var inerBagsSplit = Enumerable.Range(0, innerBagType.Count).Select(x => innerBagType.Bag);
                    innerBags.AddRange(inerBagsSplit);
                }
            }
            Logger.Debug($"- contains {string.Join("-", innerBags)}");
            return innerBags;
        }
    }

    class BagAmounts
    {
        public int Count { get; set; }
        public string Bag { get; set; }
    }
}