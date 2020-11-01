using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleCoreApp
{
    public class Reverse
    {
        public static string GetAnswer(string data)
        {
            if (Regex.IsMatch(data, "[R, r]everse")) return ReverseStringBuilder(data);
            return CaesarCode(data);
        }

        private static string ReverseStringBuilder(string str)
        {
            var sb = new StringBuilder();
            var text = str.Split('|');
            for (var i = text[1].Length - 1; i >= 0; i--)
                sb.Append(text[1][i]);
            return sb.ToString();
        }

        private static string CaesarCode(string str)
        {
            var allSymbols = "abcdefghijklmnopqrstuvwxyz0123456789' ";
            var sb = new StringBuilder();
            var parsedString = str.Split('|');
            var data = parsedString[1];
            var task = parsedString[0];
            var code = int.Parse(task.Split('=')[1]);
            for (var i = 0; i < data.Length; i++)
                if (char.IsLetter(data[i]))
                {
                    var nextIndex = allSymbols.IndexOf(data[i]) - code % allSymbols.Length;
                    if (nextIndex < 0)
                        sb.Append(allSymbols[allSymbols.Length + nextIndex]);
                    else
                        sb.Append(allSymbols[nextIndex % allSymbols.Length]);
                }
                else if (char.IsDigit(data[i]))
                {
                    var nextIndex = allSymbols.IndexOf(data[i]) - code % allSymbols.Length;
                    if (nextIndex < 0)
                        sb.Append(allSymbols[allSymbols.Length + nextIndex]);
                    else
                        sb.Append(allSymbols[nextIndex % allSymbols.Length]);
                }
                else
                {
                    sb.Append(data[i]);
                }
            return sb.ToString();
        }
    }
}