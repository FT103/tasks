using System.Text;

namespace ConsoleCoreApp
{
    public static class Reverse
    {
        public static string ReverseStringBuilder(string str)
        {
            StringBuilder sb = new StringBuilder();
            var text = str.Split('|');
            for (var i = text[1].Length - 1; i >= 0; i--)
                sb.Append(text[1][i]);
            return sb.ToString();
        }
    }
}