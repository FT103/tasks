using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ConsoleCoreApp;
using NUnit.Framework;

namespace ConsoleCoreApp
{
    public static class Statistics
    {
        public static string GetAnswer(string question)
        {
            var parsedQuestion = question.Split('|');
            var operationStack = GetOperationsStack(parsedQuestion[0]);
            var stringArray = parsedQuestion[1];

            var list = GetList(stringArray);
            if (list.Count != 0)
            {
                while (operationStack.Count > 0)
                {
                    var fun = operationStack.Pop();

                    if (fun == "increment")
                        Increment(list);

                    if (fun == "decrement")
                        Decrement(list);

                    if (fun == "double")
                        Double(list);

                    if (fun == "odd")
                        Odd(list);

                    if (fun == "even")
                        Even(list);

                    if (fun.StartsWith("take"))
                    {
                        foreach (var character in fun)
                        {
                        }
                    }
                        

                    if (fun == "min")
                    {
                        if (list.Count == 0)
                            return string.Empty;
                        list.Sort();
                        return list[0].ToString();
                    }

                    if (fun == "max")
                    {
                        if (list.Count == 0)
                            return string.Empty;
                        list.Sort();
                        return list[^1].ToString();
                    }

                    if (fun == "sum")
                    {
                        return GetSum(list).ToString();
                    }

                    if (fun == "median")
                    {
                        list.Sort();
                        return GetMedian(list).ToString();
                    }

                    if (fun == "firstmostfrequent")
                    {
                        return GetFirstFrequent(list).ToString();
                    }
                }

                return string.Join(" ", list);
            }

            return string.Empty;
        }

        private static void Increment(List<int> list)
        {
            for (var i = 0; i < list.Count; i++)
                list[i]++;
        }

        private static void Decrement(List<int> list)
        {
            for (var i = 0; i < list.Count; i++)
                list[i]--;
        }

        private static void Double(List<int> list)
        {
            for (var i = 0; i < list.Count; i++)
                list[i] *= 2;
        }

        private static void Odd(List<int> list)
        {
            var i = 0;
            while (i < list.Count)
            {
                if (list[i] % 2 == 0)
                {
                    list.RemoveAt(i);
                    continue;
                }

                i++;
            }
        }

        private static void Even(List<int> list)
        {
            var i = 0;
            while (i < list.Count)
            {
                if (list[i] % 2 == 1)
                {
                    list.RemoveAt(i);
                    continue;
                }

                i++;
            }
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

        public static Stack<string> GetOperationsStack(string operations)
        {
            var operation = new StringBuilder();
            var operationStack = new Stack<string>();
            foreach (var character in operations + '.')
            {
                if (character == '.')
                {
                    operationStack.Push(operation.ToString());
                    operation.Clear();
                    continue;
                }

                operation.Append(character);
            }

            return operationStack;
        }
    }
}

[TestFixture]
class StatisticsTests
{
    [TestCase(
        "sum.increment.increment|16 72 61 44 42 64 85 86 10 31 66 38 73 59 95 3 31 23 60 66 35 92 94 75 64 86 65 80 55",
        "1729")]
    [TestCase("min.increment.even.even.even.decrement.even.even.double.increment.decrement.even.decrement.double.double.increment.even.increment.increment.even.double.even.even|43 19 77 52 82 40 38 94 38 7 95 93 92 82 93 68 7 5 " +
    "71 99 80 58 44 44 88 30 61 43 42 55 35 73 48 20 37 80", "")]
    [TestCase("firstmostfrequent.decrement.double|5 6 3 5 9 3 4 9 3 0 6 1 7 9 2 4 8 2 2 3 1", "5")]
    [TestCase("max.decrement|34 18 79 72 58", "78")]
    [TestCase("firstmostfrequent|6 7 2 8 5 3 3 0 8 3 1 9 4 8 4 5 1 8 7 3 0 2 0 3 8 2 7 8 7 5 9", "8")]
    [TestCase("min.decrement.decrement.increment.double|34 18 79 72 58", "35")]
    [TestCase("min.odd|34 18 79 72 58", "79")]
    [TestCase("min.even|34 79 72 58 1", "34")]
    public void ResultTest(string task, string result)
    {
        var actualResult = Statistics.GetAnswer(task);
        Assert.AreEqual(result, actualResult);
    }

    [TestCase(
        "double.increment.decrement.decrement.increment.double.increment.decrement.double.decrement.increment.double.decrement.decrement.decrement.increment.double.double.double.double.decrement.decrement.double")]
    public void GetOperationsStackTest(string operations)
    {
        var stack = Statistics.GetOperationsStack(operations);
        var operationsArray = operations.Split('.');
        for (var i = operationsArray.Length - 1; i >= 0; i--)
        {
            Assert.AreEqual(stack.Pop(), operationsArray[i]);
        }
    }
}