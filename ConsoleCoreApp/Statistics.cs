using System;
using System.Collections.Generic;

namespace ConsoleCoreApp
{
    public static class Statistics
    {
        public static string GetAnswer(string question)
        {
            var parsedQuestion = question.Split('|');
            var fun = parsedQuestion[0];
            var stringArray = parsedQuestion[1];
            var list = GetSortedList(stringArray);
            if (list.Count != 0)
            {
                if (fun == "min") return list[0].ToString();
                if (fun == "max") return list[new Index(1, true)].ToString();
            }

            return string.Empty;
        }

        private static List<int> GetSortedList(string input)
        {
            var arr = input.Split(' ');
            var list = new List<int>();
            foreach (var num in arr)
            {
                list.Add(int.Parse(num));
            }
            
            list.Sort();
            return list;
        }
    }
}