using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace ConsoleCoreApp
{
    public class MathParser
    {
        public string GetAnswer(string str)
        {
            var answers = new StringBuilder();
            var tasks = ParseLine(str);
            for (var i = 0; i < tasks.Count; i++)
            {
                answers.Append(Calculate(tasks[i]));
                if (i < tasks.Count - 1)
                    answers.Append(" ");
            }

            return answers.ToString();
        }
            

        private string GetReversePolishNotation(string input)
        {
            Stack<char> operationStack = new Stack<char>();
            StringBuilder output = new StringBuilder();
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

        private int GetCharPriority(char ch)
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

        public List<string> ParseLine(string line)
        {
            var list = new List<string>();
            var i = 0;
            while (i < line.Length)
            {
                if (line[i] == ' ')
                {
                    i++;
                    continue;
                }

                string str = string.Empty;

                if (line[i] == '\"' || line[i] == '\'')
                {
                    str = ReadQuotedField(line, i);
                    i += 2;
                }
                else if (line[i] != ' ')
                    str = ReadField(line, i);

                i += str.Length;
                list.Add(str);
            }

            return list;
        }

        private string ReadField(string line, int startIndex)
        {
            var value = new StringBuilder();
            var i = startIndex;
            while (i < line.Length)
            {
                if (line[i] == ' ' || line[i] == '\'' || line[i] == '\"') break;
                value.Append(line[i++]);
            }

            return value.ToString();
        }

        public string ReadQuotedField(string line, int startIndex)
        {
            var tokenEnd = line[startIndex];
            var value = new StringBuilder();
            var i = startIndex + 1;
            while (i < line.Length)
            {
                if (line[i] == '\\')
                    i++;
                else if (line[i] == tokenEnd)
                {
                    i++;
                    break;
                }

                value.Append(line[i++]);
            }

            return value.ToString();
        }
    }

    [TestFixture]
    public class MathParserTests
    {
        [TestCase("0+6", "6")]
        [TestCase("\"17 + 7 - 13 / 11\"", "23")]
        [TestCase("\"7 + 3 / 5 * 1\" 0+2/6+7 \"0 / 10 % 10\"", "7 7 0")]

        public void ResultTest(string expression, string expected)
        {
            var math = new MathParser();
            var result = math.GetAnswer(expression);
            Assert.AreEqual(expected, result);
        }

        [TestCase("\"7 + 3 / 5 * 1\" 0+2/6+7 \"0 / 10 % 10\"", new []{"7 + 3 / 5 * 1", "0+2/6+7", "0 / 10 % 10"})]
        public void ParserTest(string input, string[] exprs)
        {
            var math = new MathParser();
            var actualList = math.ParseLine(input);
            CollectionAssert.AreEqual(exprs, actualList);
        }
    }
}