using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace ConsoleCoreApp
{
    public class MathParser
    {
        private LineParser lineParser = new LineParser();
        public string GetAnswer(string str)
        {
            var answers = new StringBuilder();
            if (!IsExpression(str)) return str;
            var tasks = lineParser.ParseLine(str);
            for (var i = 0; i < tasks.Count; i++)
            {
                answers.Append(Calculate(tasks[i]));
                if (i < tasks.Count - 1)
                    answers.Append(" ");
            }

            return answers.ToString();
        }


        public string GetReversePolishNotation(string input)
        {
            var operationStack = new Stack<char>();
            var output = new StringBuilder();
            for (var i = 0; i < input.Length; i++)
            {
                var priority = GetCharPriority(input[i]);
                if (priority == 0)
                    output.Append(input[i]);
                if (priority == 1)
                    operationStack.Push(input[i]);
                if (priority > 1)
                {
                    output.Append(' ');

                    while (operationStack.Count > 0)
                        if (GetCharPriority(operationStack.Peek()) >= priority)
                            output.Append(operationStack.Pop());
                        else break;

                    operationStack.Push(input[i]);
                }

                if (priority == -1)
                {
                    output.Append(' ');
                    while (GetCharPriority(operationStack.Peek()) != 1)
                        output.Append(operationStack.Pop());
                    operationStack.Pop();
                }
            }

            while (operationStack.Count > 0)
                output.Append(operationStack.Pop());
            {
            }
            return output.ToString();
        }


        public int Calculate(string input)
        {
            var str = GetReversePolishNotation(input);
            var operand = string.Empty;
            var stack = new Stack<int>();
            for (var i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ') continue;

                if (GetCharPriority(str[i]) == 0)
                {
                    while (str[i] != ' ' && GetCharPriority(str[i]) == 0)
                    {
                        operand += str[i++];
                        if (i == str.Length) break;
                    }

                    stack.Push(int.Parse(operand));
                    operand = string.Empty;
                }

                if (GetCharPriority(str[i]) > 1)
                {
                    var a = stack.Pop();
                    var b = stack.Pop();
                    switch (str[i])
                    {
                        case '*':
                            stack.Push(b * a);
                            break;
                        case '/':
                            stack.Push(b / a);
                            break;
                        case '+':
                            stack.Push(b + a);
                            break;
                        case '-':
                            stack.Push(b - a);
                            break;
                        case '%':
                            stack.Push(b % a);
                            break;
                    }
                }
            }

            return stack.Pop();
        }

        public int GetCharPriority(char ch)
        {
            if (ch == '*' || ch == '/' || ch == '%')
                return 3;
            if (ch == '+' || ch == '-')
                return 2;
            if (ch == '(')
                return 1;
            if (ch == ')')
                return -1;
            return 0;
        }

        public bool IsExpression(string input)
        {
            foreach (var character in input)
            {
                if (GetCharPriority(character) >= 2)
                    return true;
                
            }

            return false;
        }
    }

    [TestFixture]
    public class MathParserTests
    {
        [TestCase("6", "6")]
        [TestCase("6-1", "5")]
        [TestCase("6+1", "7")]
        [TestCase("6/2", "3")]
        [TestCase("6*3", "18")]
        [TestCase("6%2", "0")]
        [TestCase("'6 - 1'", "5")]
        [TestCase("'6 + 1'", "7")]
        [TestCase("'6 / 2'", "3")]
        [TestCase("'6 * 3'", "18")]
        [TestCase("'6 % 2'", "0")]
        [TestCase("(2+2)*2", "8")]
        [TestCase("2+2*2", "6")]
        [TestCase("\"17 + 7 - 13 / 11\"", "23")]
        [TestCase("\"7 + 3 / 5 * 1\" 0+2/6+7 \"0 / 10 % 10\"", "7 7 0")]
    
        public void ResultTest(string expression, string expected)
        {
            var math = new MathParser();
            var result = math.GetAnswer(expression);
            Assert.AreEqual(expected, result);
        }
    }
}