using System.Transactions;
using NUnit.Framework;

namespace ConsoleCoreApp
{
    public static class MathTask
    {
        public static string GetAnswer(string expression)
        {
            var isComplex = false;
            foreach (var character in expression)
            {
                if (character == 'i')
                {
                    isComplex = true;
                    break;
                }
            }
            if (isComplex) return new ComplexNumberCalculator().GetAnswer(expression);
            return new MathParser().GetAnswer(expression);
        }
    }

    [TestFixture]
    public class MathTests
    {
        [TestCase("(1i+1i+4)+(0i*4i+3i)", "4+5i")]
        [TestCase("15-2+7-2 2+4 \"15 * 7 + 10 * 13\" \"6 - 1\" 1/10-13", "18 6 235 5 -13")]
        [TestCase("\"12 / 9 + 8 + 13 - 2\" \"8 - 4 + 13 - 8 + 13\" \"7 / 2 - 1\"", "20 22 2")]
        [TestCase("\"7 + 3 / 5 * 1\" 0+2/6+7 \"0 / 10 % 10\"", "7 7 0")]
        [TestCase("(4+3i)-(4+3i)+(6+6-4i*5)+(0-1)", "11-20i")]
        [TestCase("(3+9i+11i)+(7+4i+7i*3i)-(5i-0i+9i)", "-11+10i")]
        public void ResultTest(string expression, string expected)
        {
            var result = MathTask.GetAnswer(expression);
            Assert.AreEqual(expected, result);
        }
    }
}