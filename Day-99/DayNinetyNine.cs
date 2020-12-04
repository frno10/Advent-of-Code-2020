using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AdventOfCode
{
    public class DayNinetyNine : IDay
    {
        public int NumberOfExecutions { get; set; }

        public async Task<string> Execute()
        {
            var data = await ((IDay)this).LoadData();
            var jsonData = JsonConvert.DeserializeObject<List<FeatureSwitchSetting>>(data);
            var newJsonData = jsonData.Where(x => x.Enabled).Select(x => new NewFeatureSwitchSetting()
            {
                FeatureName = x.Name,
                ReleaseState = x.ReleaseState
            }).ToList();
            var mainNewJsonData = new MainFeatureSwitchObject()
            {
                FeatureSwitches = newJsonData
            };
            var newJsonString = JsonConvert.SerializeObject(mainNewJsonData);
            File.WriteAllText("newFeatureSwitchSettings.json", newJsonString);

            return $"Number of settings: {jsonData.Count()}";
        }

        class FeatureSwitchSetting
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public int ReleaseState { get; set; }
            public bool Enabled { get; set; }
        }

        class NewFeatureSwitchSetting 
        {
            public string FeatureName { get; set; }
            public int ReleaseState { get; set; }
        }
        
        class MainFeatureSwitchObject
        {
            public List<NewFeatureSwitchSetting> FeatureSwitches { get; set; }
        }
    }
}