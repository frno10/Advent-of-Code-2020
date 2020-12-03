using System;
using System.Globalization;

namespace AdventOfCode
{
    public class IntToNameConverter
    {
        public static string Convert(int number)
        {
            var name = "";
            var hundreds = (int)(number % 1000 / 100);

            if(hundreds > 0)
            {
                name += AdjustForMinimum(hundreds);
                name += " hundred ";
            }

            name += AdjustForTeen(number);
            if(number % 100 > 19)
            {
                name += AdjustForDecade(number);
            }
            name += AdjustForMinimum(number % 10);

            return name;
        }

        private static string AdjustForDecade(int number)
        {
            var result = "";
            switch((int)(number % 100 / 10))
            {
                case 2:
                    result = "twenty";
                    break;
                case 3:
                    result = "thirty";
                    break;
                case 4:
                    result = "fourty";
                    break;
                case 5:
                    result = "fifty";
                    break;
                case 6:
                    result = "sixty";
                    break;
                case 7:
                    result = "seventy";
                    break;
                case 8:
                    result = "eighty";
                    break;
                case 9:
                    result = "ninety";
                    break;
            }
            if(number % 100 / 10 > 1 && number % 10 > 0)
            {
                result += "-";
            }
            return result;
        }

        private static string AdjustForTeen(int number)
        {
            var result = "";
            var numberProcessed = (int)(number % 100);
            
            if(numberProcessed < 10 || numberProcessed > 19)
                return "";

            switch(numberProcessed)
            {
                case 11:
                    result = "eleven";
                    break;
                case 12:
                    result = "twelve";
                    break;
                case 13:
                    result = "thirteen";
                    break;
                case 14:
                    result = "fourteen";
                    break;
                case 15:
                    result = "fifteen";
                    break;
                case 16:
                    result = "sixteen";
                    break;
                case 17:
                    result = "seventeen";
                    break;
                case 18:
                    result = "eighteen";
                    break;
                case 19:
                    result = "nineteen";
                    break;
            }

            return result;
        }

        private static string AdjustForMinimum(int number)
        {
            var result = "";
            switch(number)
                {
                    case 0:
                        break;
                    case 1:
                        result = "one";
                        break;
                    case 2:
                        result = "two";
                        break;
                    case 3:
                        result = "three";
                        break;
                    case 4:
                        result = "four";
                        break;
                    case 5:
                        result = "five";
                        break;
                    case 6:
                        result = "six";
                        break;
                    case 7:
                        result = "seven";
                        break;
                    case 8:
                        result = "eight";
                        break;
                    case 9:
                        result = "nine";
                        break;
                }
                return result;
        }
    }

    public static class StringExtentions
    {
        public static string ToTitleCase(this string wordToCapitalize)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(wordToCapitalize);
        }

        public static string ToClassname(this string wordToNormalize)
        { 
            return wordToNormalize.Replace(" ", "").Replace("-", "");
        }
    }
}