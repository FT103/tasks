using System;
using System.Collections.Generic;
using System.Linq;
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
            var list = GetSortedList(stringArray);
            if (list.Count != 0)
            {
                if (fun == "min") return list[0];
                if (fun == "max") return list[new Index(1, true)];
            }

            return string.Empty;
        }

        private static List<string> GetSortedList(string input)
        {
            var list = input.Split(' ').ToList();
            list.Sort();
            return list;
        }
    }
}