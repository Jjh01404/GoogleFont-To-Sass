using System;
using System.Collections.Generic;

namespace GoogleFont_To_Sass
{
    class Program
    {
        static void Main()
        {
            //  @import url('https://fonts.googleapis.com/css?family=Lato:900|Roboto:400,500i,700');
            //  C:\Users\[USER]\Desktop\_typography.scss

            Console.WriteLine("Please input google fonts import URL:");
            var input = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("Please input destination to save file e.g. c:\\example.scss");
            var filePath = Console.ReadLine();

            var fonts = GetFontName(input);
            var sass = GenerateSass(fonts);
            SaveSassFile(sass, filePath);
        }

        public static List<Font> GetFontName(string input)
        {
            var trimmedStart = input.Substring(input.IndexOf("=") + 1);
            var trimmedEnd = trimmedStart.Substring(0, trimmedStart.LastIndexOf(")") - 1);

            string[] splitted = trimmedEnd.Split('|');

            List<Font> fonts = new List<Font>();

            foreach (var split in splitted)
            {
                string[] splittedWeightAndFont = split.Split(':');

                string font = splittedWeightAndFont[0];
                string weights = splittedWeightAndFont[1];
                string[] weight = weights.Split(',');

                Font finalFont = new Font
                {
                    FontName = font
                };

                foreach (var weightItem in weight)
                {
                    finalFont.FontWeights.Add(weightItem);
                }

                fonts.Add(finalFont);
            }
            return fonts;
        }

        public static string GenerateSass(List<Font> fonts)
        {
            int number = 1;
            string sassToWrite = "";
            foreach (var font in fonts)
            {
                sassToWrite += $"$Font-{number}:{font.FontName};";
                sassToWrite += "\r\n";
                foreach (var weight in font.FontWeights)
                {
                    sassToWrite += $"$Font-{number}-{weight}:{weight};";
                    sassToWrite += "\r\n";
                    sassToWrite += $"@mixin Mixin-Font-{number}-{weight}{{\r\nfont-family:{font.FontName};\r\nfont-weight:{weight};\r\n}}";
                    sassToWrite += "\r\n";
                }
                sassToWrite += "\r\n";
                number++;
            }
            return sassToWrite;
        }

        public static void SaveSassFile(string inputSass, string filePath)
        {
            System.IO.File.WriteAllText(filePath, inputSass);
        }

        public class Font
        {
            public string FontName { get; set; }

            public List<string> FontWeights { get; set; }

            public Font()
            {
                FontName = FontName;
                FontWeights = new List<string>();
            }
        }
    }
}