using System.Collections.Generic;
using System.Text;

namespace ConsoleCoreApp
{
    public static class JsonParser
    {
        private static List<string> GetValues(string expression)
        {
            var list = new List<string>();
            var i = 0;
            var value = new StringBuilder();
            while (i < expression.Length)
            {
                if (expression[i++] == '"')
                {
                    while (expression[i] != '"' && i < expression.Length)
                        value.Append(expression[i++]);
                    var el = value.ToString();
                    if (!char.IsLetter(el[0]))
                        list.Add(value.ToString());
                    value.Clear();
                    i++;
                }
            }
            return list;
        }

        public static int GetValuesSum(string expression)
        {
            var sum = 0;
            var values = GetValues(expression);
            foreach (var value in values)
            {
                sum += int.Parse(value);
            }

            return sum;
        }
    }
}