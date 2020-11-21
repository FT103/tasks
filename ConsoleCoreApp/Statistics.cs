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
                if (fun == "sum") return GetSum(list).ToString();
                if (fun == "median") return GetMedian(list).ToString();
            }

            return string.Empty;
        }

        private static int GetSum(List<int> list)
        {
            var sum = 0;
            foreach (var num in list) sum += num;

            return sum;
        }

        private static int GetMedian(List<int> list)
        {
            if (list.Count % 2 == 1) return list[list.Count / 2];
            return (list[list.Count / 2] + list[list.Count / 2 + 1]) / 2;
        }

        private static List<int> GetSortedList(string input)
        {
            var arr = input.Split(' ');
            var list = new List<int>();
            foreach (var num in arr) list.Add(int.Parse(num));

            list.Sort();
            return list;
        }
    }
}