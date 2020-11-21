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
            var operators = GetOperators(expression);
            return null;
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

        public (string rz, string im) ParseComplexNumber(string expression)
        {
            var rz = "0+0";
            var im = "0+0";
            var current = new StringBuilder();
            var isIm = false;
            foreach (var character in expression + '-')
            {
                if (character == '+' || character == '-')
                {
                    if (isIm) im += current.ToString();
                    else rz += current.ToString();
                    isIm = false;
                    current.Clear();
                    current.Append(character);
                    continue;
                }

                if (character == 'i')
                    isIm = true;
                current.Append(character);
            }

            return (rz, im);
        }

        public Complex GetComplex(string expression)
        {
            var complex = ParseComplexNumber(expression);
            var rz = math.Calculate(complex.rz);
            var im = math.Calculate(complex.im.Replace("i", ""));
            return new Complex(rz, im);
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
    }

    [TestFixture]
    public class ExpressionParserTest
    {
        [TestCase("(4+3i)-(4+3i)+(6+6-4i*5)+(0-1)", new[] {"4+3i", "4+3i", "6+6-4i*5", "0-1"})]
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
        public void TestGetOperators(string input, char[] output)
        {
            var calc = new ComplexNumberCalculator();
            var actualList = calc.GetOperators(input);
            CollectionAssert.AreEqual(output.ToList(), actualList);
        }

        [TestCase("4+3i", "04", "0+3i")]
        [TestCase("6+6-4i*5", "06+6", "0-4i*5")]
        [TestCase("0-1", "00-1", "0")]
        [TestCase("0+6+4i-0", "00+6-0", "0+4i")]
        [TestCase("8i+6-4i+0i", "0+6", "08i-4i+0i")]
        public void TestParseComplexNumber(string input, string rz, string im)
        {
            var calc = new ComplexNumberCalculator();
            var actual = calc.ParseComplexNumber(input);
            Assert.AreEqual(rz, actual.rz);
            Assert.AreEqual(im, actual.im);
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

        [TestCase("(4+3i)-(4+3i)+(6+6-4i*5)+(0-1)", 4)]
        public void TestGetComplexes(string expr, int amount)
        {
            var calc = new ComplexNumberCalculator();
            var complexes = new List<Complex>();
            complexes.Add(new Complex(4, 3));
            complexes.Add(new Complex(4, 3));
            complexes.Add(new Complex(12, -20));
            complexes.Add(new Complex(-1, 0));
            var actual = calc.GetComplexes(expr);
            CollectionAssert.AreEqual(complexes, actual);
        }
    }
}