using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using NUnit.Framework;


namespace ConsoleCoreApp
{
    public class ComplexNumberCalculator
    {
        private MathParser math;

        public ComplexNumberCalculator()
        {
            math = new MathParser();
        }

        public string GetAnswer(string expression)
        {
            var complex = GetResult(expression);
            if (complex.Real == 0 && complex.Imaginary == 0) return "0";
            if (complex.Real == 0) return $"{complex.Imaginary}i";
            if (complex.Imaginary == 0) return $"{complex.Real}";
            var rz = $"{complex.Real}";
            var im = complex.Imaginary > 0 ? $"+{complex.Imaginary}i" : $"{complex.Imaginary}i";
            return $"{rz}{im}";
        }

        public List<string> ParseExpression(string input)
        {
            var expression = new StringBuilder();
            var expressionList = new List<string>();
            var i = 0;
            while (i < input.Length)
            {
                if (input[i++] == '(')
                {
                    while (input[i] != ')' && i < input.Length)
                    {
                        expression.Append(input[i++]);
                    }

                    expressionList.Add(expression.ToString());
                    expression.Clear();
                }
            }

            return expressionList;
        }

        public List<char> GetOperators(string expression)
        {
            var operators = new List<char>();
            var i = 0;
            while (i + 1 < expression.Length)
            {
                if (expression[i] == ')')
                    operators.Add(expression[i + 1]);
                i++;
            }
            
            return operators;
        }
        
        public static List<char> GetOperatorsInBox(string expression)
        {
            var operators = new List<char>();
            foreach (var character in expression)
            {
                if (character == '*' || character == '/' ||
                    character == '+' || character == '-')
                    operators.Add(character);
            }
            
            return operators;
        }

        public Complex GetComplex(string expression)
        {
            var operators = GetOperatorsInBox(expression);
            var values = expression.Split(operators.ToArray());
            var complexes = new List<Complex>();
            foreach (var value in values)
            {
                if (value.IndexOf('i') == -1) complexes.Add(new Complex(int.Parse(value), 0));
                else
                {
                    var im = value.Replace("i", "");
                    complexes.Add(new Complex(0, int.Parse(im)));
                }
            }
            return Calculate(GetIndexesString(operators), complexes);
        }

        public List<Complex> GetComplexes(string expression)
        {
            var exprList = ParseExpression(expression);
            var complexes = new List<Complex>();
            foreach (var expr in exprList)
            {
                complexes.Add(GetComplex(expr));
            }

            return complexes;
        }

        public Complex GetResult(string expression)
        {
            var complexes = GetComplexes(expression);
            var operators = GetOperators(expression);
            var result = Calculate(GetIndexesString(operators), complexes);
            return result;
        }

        public Complex Calculate(string input, List<Complex> complexes)
        {
            var str = math.GetReversePolishNotation(input);
            var operand = string.Empty;
            var stack = new Stack<Complex>();
            for (var i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ') continue;

                if (math.GetCharPriority(str[i]) == 0)
                {
                    while (str[i] != ' ' && math.GetCharPriority(str[i]) == 0)
                    {
                        operand += str[i++];
                        if (i == str.Length) break;
                    }

                    stack.Push(complexes[int.Parse(operand)]);
                    operand = string.Empty;
                }

                if (math.GetCharPriority(str[i]) > 1)
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
                    }
                }
            }

            return stack.Pop();
        }

        private string GetIndexesString(List<char> operators)
        {
            var i = 1;
            var result = "0";
            foreach (var o in operators)
            {
                result += o;
                result += i++;
            }

            return result;
        }
    }

    [TestFixture]
    public class ExpressionParserTest
    {
        [TestCase("(4+3i)-(4+3i)+(6+6-4i*5)+(0-1)", new[] {"4+3i", "4+3i", "6+6-4i*5", "0-1"})]
        [TestCase("(3+9i+11i)+(7+4i+7i*3i)-(5i-0i+9i)", new[] {"3+9i+11i", "7+4i+7i*3i", "5i-0i+9i"})]
        [TestCase("(3+10i)+(1+3)+(7i-12-3)", new[] {"3+10i", "1+3", "7i-12-3"})]
        [TestCase("(0+6+4i-0)+(2i+6*5-4)*(4i+5-2)-(8i+6-4i+0i)",
            new[] {"0+6+4i-0", "2i+6*5-4", "4i+5-2", "8i+6-4i+0i"})]
        public void TestParseExpression(string input, string[] output)
        {
            var calc = new ComplexNumberCalculator();
            var actualList = calc.ParseExpression(input);
            CollectionAssert.AreEqual(output.ToList(), actualList);
        }

        [TestCase("(4+3i)-(4+3i)+(6+6-4i*5)+(0-1)", new[] {'-', '+', '+'})]
        [TestCase("(0+6+4i-0)+(2i+6*5-4)*(4i+5-2)-(8i+6-4i+0i)", new[] {'+', '*', '-'})]
        [TestCase("(3+9i+11i)+(7+4i+7i*3i)-(5i-0i+9i)", new[] {'+', '-'})]
        [TestCase("(3+10i)+(1+3)+(7i-12-3)", new[] {'+', '+'})]
        public void TestGetOperators(string input, char[] output)
        {
            var calc = new ComplexNumberCalculator();
            var actualList = calc.GetOperators(input);
            CollectionAssert.AreEqual(output.ToList(), actualList);
        }

        [TestCase("4+3i", 4, 3)]
        [TestCase("6+6-4i*5", 12, -20)]
        [TestCase("0-1", -1, 0)]
        [TestCase("0+6+4i-0", 6, 4)]
        [TestCase("8i+6-4i+0i", 6, 4)]
        public void TestGetComplexNumber(string expr, double real, double imaginary)
        {
            var calc = new ComplexNumberCalculator();
            var actual = calc.GetComplex(expr);
            Assert.AreEqual(real, actual.Real);
            Assert.AreEqual(imaginary, actual.Imaginary);
        }

        [TestCase("(5i+2i)+(0i-6*8)", 2)]
        [TestCase("(4+3i)-(4+3i)+(6+6-4i*5)+(0-1)", 4)]
        [TestCase("(5+3i+6i)-(0-9*5i*8)+(3+1)", 3)]
        public void TestGetComplexes(string expr, int amount)
        {
            var calc = new ComplexNumberCalculator();
            var actual = calc.GetComplexes(expr);
            Assert.AreEqual(amount, actual.Count);
        }

        [TestCase("(4+3i)-(4+3i)+(6+6-4i*5)+(0-1)", 11, -20)]
        [TestCase("(5+3i+6i)-(0-9*5i*8)+(3+1)", 9, 369)]
        [TestCase("(1i+1i+4)+(0i*4i+3i)", 4, 5)]
        public void TestResult(string expr, double rz, double im)
        {
            var calc = new ComplexNumberCalculator();
            var result = calc.GetResult(expr);
            Assert.AreEqual(rz, result.Real);
            Assert.AreEqual(im, result.Imaginary);
        }
        
        [TestCase("(4+3i)-(4+3i)+(6+6-4i*5)+(0-1)", "11-20i")]
        [TestCase("(5+3i+6i)-(0-9*5i*8)+(3+1)", "9+369i")]
        [TestCase("(1i+1i+4)+(0i*4i+3i)", "4+5i")]
        [TestCase("(1i+1i+4)-(1i+1i+4)", "0")]
        [TestCase("(1i+1i+4)-(1i+1i)", "4")]
        [TestCase("(3+9i+11i)+(7+4i+7i*3i)-(5i-0i+9i)", "-11+10i")]
        public void TestAnswer(string expr, string expected)
        {
            var calc = new ComplexNumberCalculator();
            var result = calc.GetAnswer(expr);
            Assert.AreEqual(expected, result);
        }
    }
}