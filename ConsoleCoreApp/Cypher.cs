using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleCoreApp
{
    public class Cypher
    {
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyz0123456789' ";
        private const char separator = '#';

        public static string GetAnswer(string data)
        {
            if (Regex.IsMatch(data, "[R, r]everse")) return ReverseStringBuilder(data);
            if (data.StartsWith("Vigenere's code")) return GetVigenereCode(data);
            if (data.StartsWith("Caesar's code")) return GetCaesarCode(data);
            if (data.StartsWith("prime multiplicator")) return GetMultiplicatorCode(data);
            return GetFirstLongestWorld(data);
        }

        public static string ReverseStringBuilder(string str)
        {
            var sb = new StringBuilder();
            var text = str.Split(separator);
            for (var i = text[1].Length - 1; i >= 0; i--)
                sb.Append(text[1][i]);
            return sb.ToString();
        }
        private static string GetMultiplicatorCode(string str)
        {
            var sb = new StringBuilder();
            var parsedString = str.Split(separator);
            var data = parsedString[1];
            var task = parsedString[0];
            var dic = new Dictionary<char, char>();

            var multiplier = int.Parse(task.Split('=')[1].Split(' ')[0]);
            for (var i = 0; i < Alphabet.Length; i++)
            {
                var newIndex = multiplier * (i + 1) % (Alphabet.Length + 1) - 1;
                dic.Add(Alphabet[newIndex], Alphabet[i]);
            }

            foreach (var symbol in data) sb.Append(dic[symbol]);

            return sb.ToString();
        }
        public static string GetCaesarCode(string str)
        {
            var sb = new StringBuilder();
            var parsedString = str.Split(separator);
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

        public static string GetVigenereCode(string inputStr)
        {
            var squareVigenere = new Dictionary<char, string>();
            var stringBuilder = new StringBuilder();
            foreach (var symbol in Alphabet) stringBuilder.Append(symbol);
            foreach (var symbol in Alphabet)
            {
                squareVigenere.Add(symbol, stringBuilder.ToString());
                stringBuilder.Remove(0, 1);
                stringBuilder.Append(symbol);
            }

            stringBuilder.Clear();
            var keyWord = inputStr.Split(separator)[0].Split('=')[1];
            var data = inputStr.Split(separator)[1];

            for (var i = 0; i < data.Length; i++)
            {
                var key = keyWord[i % keyWord.Length];
                var valueStr = squareVigenere[key];
                var indexInValueStr = valueStr.IndexOf(data[i]);
                stringBuilder.Append(Alphabet[indexInValueStr]);
            }

            return stringBuilder.ToString();
        }
    
        public static string GetFirstLongestWorld(string str)
        {
            return "";
        }
    }
}