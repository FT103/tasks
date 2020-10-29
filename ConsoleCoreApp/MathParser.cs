using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleCoreApp
{
    public static class MathParser
    {
        public static string GetAnswer(string str) =>
            Calculate(GetReversePolishNotation(str)).ToString();

        private static string GetReversePolishNotation(string input)
        {
            Stack<Char> operationStack = new Stack<char>();
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
                    {
                        if (GetCharPriority(operationStack.Peek()) >= priority)
                            output.Append(operationStack.Pop());
                        else break;
                    }

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
        
        private static int Calculate(string str)
        {
            var operand = string.Empty;
            var stack = new Stack<int>();
            for (var i = 0; i < str.Length; i++)
            {
                if(str[i] == ' ') continue;
                
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
                            stack.Push( b * a);
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

        private static int GetCharPriority(char ch)
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
    }
}