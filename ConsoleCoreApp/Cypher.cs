using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleCoreApp
{
    public class Cypher
    {
        const string Alphabet = "abcdefghijklmnopqrstuvwxyz0123456789' ";
        public static string GetAnswer(string data)
        {
            if (Regex.IsMatch(data, "[R, r]everse")) return ReverseStringBuilder(data);
            if(data == "Vigenere's code") return GetVigenereCode(data);
            if(data == "Caesar's code") return GetCaesarCode(data);
            //if(data == "first longest word") 
                return GetFirstLongestWorld(data);
        }

        private static string ReverseStringBuilder(string str)
        {
            var sb = new StringBuilder();
            var text = str.Split('|');
            for (var i = text[1].Length - 1; i >= 0; i--)
                sb.Append(text[1][i]);
            return sb.ToString();
        }

        private static string GetCaesarCode(string str)
        {
            var sb = new StringBuilder();
            var parsedString = str.Split('|');
            var data = parsedString[1];
            var task = parsedString[0];
            var code = int.Parse(task.Split('=')[1]);
            for (var i = 0; i < data.Length; i++)
            {
                var nextIndex = Alphabet.IndexOf(data[i]) - code % Alphabet.Length;
                if (nextIndex < 0)
                    sb.Append(Alphabet[Alphabet.Length + nextIndex]);
                else
                    sb.Append(Alphabet[nextIndex % Alphabet.Length]);
            }
            return sb.ToString();
        }
        
        public static string GetVigenereCode(string str)
        {
            var squareVigenere = new Dictionary<char, string>();
            var stringBuilder = new StringBuilder();
            foreach (var symbol in Alphabet)
            {
                stringBuilder.Append(symbol);
            }
            foreach (var symbol in Alphabet)
            {
                squareVigenere.Add(symbol, stringBuilder.ToString());
                stringBuilder.Remove(0, 1);
            }
            return null;
        }
        public static string GetFirstLongestWorld(string str)
        {
            return null;
        }
    }
}