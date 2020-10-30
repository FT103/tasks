using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleCoreApp
{
    public static class Statistics
    {
        public static string GetAnswer(string question)
        {
            var parsedQuestion = question.Split('|');
            var fun = parsedQuestion[0];
            var stringArray = parsedQuestion[1];
            var list = GetSortedIntegerList(stringArray);
            if (list.Count != 0)
            {
                if (fun == "min") return list[0].ToString();
                if (fun == "max") return list[new Index(1, true)].ToString();
            }

            return string.Empty;
        }

        private static List<int> GetSortedIntegerList(string input)
        {
            var digit = new StringBuilder();
            var list = new List<int>();
            for (var i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]))
                    digit.Append(input[i]);
                if (char.IsWhiteSpace(input[i]))
                {
                    list.Add(int.Parse(digit.ToString()));
                }
            }
            list.Sort();
            return list;
        }
    }
}