using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Killer1
{
    public class FontParser
    {
        static int HeaderSize = 4;

        //Get the value after an equal sign and converts it from string to integer
        private static int GetValue(string s)
        {
            string value = s.Substring(s.IndexOf('=') + 1);
            return int.Parse(value);
        }

        public static Dictionary<char, CharacterData> Parse(string filepath)
        {
            Dictionary<char, CharacterData> charDictionary = new Dictionary<char, CharacterData>();
            string[] lines = File.ReadAllLines(filepath);
            for (int i = HeaderSize; i < lines.Length; i += 1)
            {
                string firstLine = lines[i];
                string[] typesAndVales = firstLine.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                CharacterData charData = new CharacterData
                {
                    Id = GetValue(typesAndVales[1]),
                    X = GetValue(typesAndVales[2]),
                    Y = GetValue(typesAndVales[3]),
                    Width = GetValue(typesAndVales[4]),
                    Height = GetValue(typesAndVales[5]),
                    XOffset = GetValue(typesAndVales[6]),
                    YOffset = GetValue(typesAndVales[7]),
                    XAdvance = GetValue(typesAndVales[8])
                };
                charDictionary.Add((char)charData.Id, charData);
            }
            return charDictionary;
        }
    }
}
