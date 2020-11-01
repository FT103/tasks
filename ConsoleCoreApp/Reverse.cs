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
            StringBuilder sb = new StringBuilder();
            var parsedString = str.Split('|');
            var data = parsedString[1];
            var task = parsedString[0];
            for (var i = 0; i < task.Length; i++)
            {
                if (char.IsDigit(task[i]))
                    sb.Append(task[i]);
            }

            var code = int.Parse(sb.ToString()) % 26;
            sb.Clear();
            for (var i = 0; i < data.Length; i++)
            {
                sb.Append((char) (data[i] - code));
            }

            return sb.ToString();
        }
    }
}