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
            var alphabet = "abcdefghijklmnopqrstuvwxyz";
            var numbers = "0123456789";
            var sb = new StringBuilder();
            var parsedString = str.Split('|');
            var data = parsedString[1];
            var task = parsedString[0];
            var code = int.Parse(task.Split('=')[1]);

            for (var i = 0; i < data.Length; i++)
                if (char.IsLetter(data[i]))
                {
                    var nextIndex = alphabet.IndexOf(data[i]) + code % alphabet.Length;
                    if (nextIndex < 0)
                        sb.Append(alphabet[alphabet.Length + nextIndex]);
                    else
                        sb.Append(alphabet[nextIndex % alphabet.Length]);
                }
                else if (char.IsDigit(data[i]))
                {
                    var nextIndex = numbers.IndexOf(data[i]) + code % numbers.Length;
                    if (nextIndex < 0)
                        sb.Append(numbers[numbers.Length + nextIndex]);
                    else
                        sb.Append(numbers[nextIndex % numbers.Length]);
                }
                else
                {
                    sb.Append(data[i]);
                }

            return sb.ToString();
        }
    }
}