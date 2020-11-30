using System;
using System.Collections.Generic;
using ConsoleCoreApp;
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

[TestFixture]
class StatisticsTests
{
    [TestCase("sum|16 72 61 44 42 64 85 86 10 31 66 38 73 59 95 3 31 23 60 66 35 92 94 75 64 86 65 80 55", "1671")]
    [TestCase("firstmostfrequent|5 6 3 5 9 3 4 9 3 0 6 1 7 9 2 4 8 2 2 3 1", "3")]
    [TestCase("max|34 18 79 72 58", "79")]
    [TestCase("firstmostfrequent|6 7 2 8 5 3 3 0 8 3 1 9 4 8 4 5 1 8 7 3 0 2 0 3 8 2 7 8 7 5 9", "8")]
    [TestCase("median|12 1 78 37 65 67 44 81 71 45 77 0 54 78 91 19 57 25 83 24 62 67 1 4 84 56 46 0 66 80 75 51 92 11 74 82 50 66 11 47 81 25 23 74 45 32 12 99 1 52 69 64 17 12 27 45 99 74 53 9 21 43 15 91 69 25 7 42 4 8 68 90 14 35 42 36 43 27 58 51 43 46 99 13 10 10 34 80 84 28 83 76 12 1 25 38 10 31 4 20 24 70 95 93 83 78 50 81 70 91 1 72 16 2 65 75 44 1 80 58 90 27 58 76 25 92 77 12 10", "46")]
    [TestCase("min|34 18 79 72 58", "18")]
    
    
    public void ResultTest(string task, string result)
    {
        var actualResult = Statistics.GetAnswer(task);
        Assert.AreEqual(result, actualResult);
    }
}