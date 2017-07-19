using System;
using System.Collections.Generic;

namespace GoogleFont_To_Sass
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = @"@import url('https://fonts.googleapis.com/css?family=Lato:900|Roboto:400,500i,700');";

            GetFontName(input);
        }

        public static void GetFontName(string input)
        {
            var trimmedStart = input.Substring(input.IndexOf("=") + 1);
            var trimmedEnd = trimmedStart.Substring(0, trimmedStart.LastIndexOf(")") - 1);

            string[] splitted = trimmedEnd.Split('|');

            foreach (var split in splitted)
            {
                string[] splittedWeightAndFont = split.Split(':');

                string font = splittedWeightAndFont[0];
                string weights = splittedWeightAndFont[1];
                string[] weight = weights.Split(',');

                Font finalFont = new Font();
                finalFont.FontName = font;

                foreach (var weightItem in weight)
                {
                    finalFont.FontWeights.Add(weightItem);
                }
            }

        }

        public static void GenerateSass(int number, string name, string[] weight)
        {
            string variableFontName = $"$Font-{number}";
            string variableFont = $"$Font-{number}:'{name}';";

            string variableWeightName = $"$Font-{number}-{weight}";
            string variableWeight = $"$Font-{number}-{weight}:{weight}';";

            string mixin = @"@mixin Mixin-Font" + number + "-" + weight + "{" +
                           "font-family:" + variableFontName + ";" +
                           "font-weight:" + variableWeightName + ";"
                           + "}";
        }

        public class Font
        {
            public string FontName { get; set; }

            public List<string> FontWeights { get; set; }
        }
    }
}