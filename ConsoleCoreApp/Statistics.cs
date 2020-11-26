using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConsoleCoreApp
{
    public static class Statistics
    {
        public static string GetAnswer(string question)
        {
            var parsedQuestion = question.Split('|');
            var fun = parsedQuestion[0];
            var stringArray = parsedQuestion[1];
            var sortedList = GetSortedList(stringArray);
            var list = GetList(stringArray);
            if (sortedList.Count != 0)
            {
                if (fun == "min") return sortedList[0].ToString();
                if (fun == "max") return sortedList[new Index(1, true)].ToString();
                if (fun == "sum") return GetSum(sortedList).ToString();
                if (fun == "median") return GetMedian(sortedList).ToString();
                if (fun == "firstmostfrequent") return GetFirstFrequent(list).ToString();
            }

            return string.Empty;
        }

        private static int GetFirstFrequent(List<int> list)
        {
            var values = new Dictionary<int, int>();
            foreach (var num in list)
            {
                if (!values.ContainsKey(num))
                {
                    values.Add(num, 1);
                }
                
                else values[num]++;
            }

            var mostFrequent = 0;
            var maxFrequency = 0;
            foreach (var key in values.Keys)
            {
                if (values[key] > maxFrequency)
                {
                    mostFrequent = key;
                    maxFrequency = values[key];
                }
            }
            
            return mostFrequent;
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
        
        private static List<int> GetList(string input)
        {
            var arr = input.Split(' ');
            var list = new List<int>();
            foreach (var num in arr)
            {
                list.Add(int.Parse(num));
            }
            
            return list;
        }
    }
}