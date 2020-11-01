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
            
            StringBuilder sb = new StringBuilder();
            var text = str.Split('|');
            for (var i = text[1].Length - 1; i >= 0; i--)
                sb.Append(text[1][i]);
            return sb.ToString();
        }

        private static string CaesarCode(string str)
        {
            var alphabet = "abcdefghijklmnopqrstuvwxyz";
            var numbers = "0123456789";
            StringBuilder sb = new StringBuilder();
            var parsedString = str.Split('|');
            var data = parsedString[1];
            var task = parsedString[0];
            var code = int.Parse(task.Split('=')[1]) % 26;
            
            for (var i = 0; i < data.Length; i++)
            {
                if (char.IsLetter(data[i]))
                    sb.Append(alphabet[(alphabet.IndexOf(data[i]) + code) % alphabet.Length]);
                if (char.IsDigit(data[i]))
                    sb.Append(alphabet[(numbers.IndexOf(data[i]) + code) % numbers.Length]);
            }

            return sb.ToString();
        }
    }
}