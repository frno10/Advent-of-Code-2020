using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class DayFour : IDay
    {
        public int NumberOfExecutions { get; set; }

        public async Task<string> Execute()
        {
            var data = await ((IDay)this).LoadData();
            var dataLines = data.Split($"{Environment.NewLine}{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries).ToArray();
            Logger.Information($"Found {dataLines.Length} passports");

            var passports = dataLines.Select(x => GetPassport(x));    
            Logger.Information($"Parsed {passports.Count()} passports");  

            var validPassports = passports.Count(x => x.ContainsKey("byr")
                && x.ContainsKey("iyr")
                && x.ContainsKey("eyr")
                && x.ContainsKey("hgt")
                && x.ContainsKey("hcl")
                && x.ContainsKey("ecl")
                && x.ContainsKey("pid")
                //&& x.ContainsKey("cid")
                );

            var strictlyValidPasswords = passports.Count(x => IsPassportValidAccordingToStrictRules(x));

            return $"{validPassports} valid passports; {strictlyValidPasswords} strictly valid passwords";
        }

        private Dictionary<string, string> GetPassport(string wholePassportString)
        {
            Logger.Debug($"Passport whole string is: {wholePassportString}");
            string[] fieldsWithValues = wholePassportString.Split(new string[] {Environment.NewLine, " "}, StringSplitOptions.RemoveEmptyEntries);
            Logger.Debug($"Passport fields detected: {string.Join("-", fieldsWithValues)}");
            var passport = new Dictionary<string, string>();
            fieldsWithValues.ToList().ForEach(x => passport.Add(x.Split(":")[0], x.Split(":")[1]));

            return passport;
        }

        private bool IsPassportValidAccordingToStrictRules(Dictionary<string, string> passportData)
        {
            var byrIsValid = IsPasswordFieldExistingAndValidDigit(passportData, "byr", 1920, 2002);
            var iyrIsValid = IsPasswordFieldExistingAndValidDigit(passportData, "iyr", 2010, 2020);
            var eyrIsValid = IsPasswordFieldExistingAndValidDigit(passportData, "eyr", 2020, 2030);
            var hgtIsValid = passportData.ContainsKey("hgt") 
                && (
                    (passportData["hgt"].Contains("cm") && IsPassportFieldValidDigit(passportData["hgt"].Replace("cm", ""), 150, 2030))
                    ||
                    (passportData["hgt"].Contains("in") && IsPassportFieldValidDigit(passportData["hgt"].Replace("in", ""), 59, 76)));

            var hclIsValid = passportData.ContainsKey("hcl") && passportData["hcl"].Length == 7 
                && Regex.Matches(passportData["hcl"], "#[a-f0-9]{6}").Any();
            var eclIsValid = passportData.ContainsKey("ecl") && new [] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(passportData["ecl"]); 
            var pidIsValid = passportData.ContainsKey("pid") && passportData["pid"].Length == 9 
                && long.TryParse(passportData["pid"], System.Globalization.NumberStyles.None, null, out long pid);
            
            return byrIsValid && iyrIsValid && eyrIsValid && hgtIsValid && hclIsValid && eclIsValid && pidIsValid;
        }

        private bool IsPasswordFieldExistingAndValidDigit(Dictionary<string, string> data, string fieldName, int minValue, int maxValue)
        {
            return data.ContainsKey(fieldName) && IsPassportFieldValidDigit(data[fieldName], minValue, maxValue);
        }

        private bool IsPassportFieldValidDigit(string data, int minValue, int maxValue)
        {
            return int.TryParse(data, System.Globalization.NumberStyles.None, null, out int fieldNameValue)
                && fieldNameValue >= minValue
                && fieldNameValue <= maxValue;
        }
    }
}