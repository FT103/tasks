using System.Collections.Generic;
using System.Text;
using ConsoleCoreApp;
using NUnit.Framework;

namespace ConsoleCoreApp
{
    class LineParser
    {
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

                var str = string.Empty;

                if (line[i] == '\"' || line[i] == '\'')
                {
                    str = ReadQuotedField(line, i);
                    i += 2;
                }
                else if (line[i] != ' ')
                {
                    str = ReadField(line, i);
                }

                i += str.Length;
                list.Add(str);
            }

            return list;
        }

        public string ReadField(string line, int startIndex)
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
                {
                    i++;
                }
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
}

[TestFixture]
public class LineParserTests
{
    [TestCase("''", 0, "", 2)]
    [TestCase("'a'", 0, "a", 3)]
    [TestCase("\"\"", 0, "", 2)]
    [TestCase("\"\\\\\" b", 0, "\\", 4)]
    [TestCase("'a\\\' b'", 0, "a' b", 7)]
    [TestCase("\"a \\\"c\\\"\"", 0, "a \"c\"", 9)]
    [TestCase("a 'world'", 2, "world", 7)]
    [TestCase("asd\"bad\"ds", 3, "bad", 5)]
    [TestCase("''", 0, "", 2)]
    [TestCase("'a'", 0, "a", 3)]
    [TestCase("'1+1/2' 2*3", 0, "1+1/2", 5)]
    [TestCase("\"1+1/2\" 2*3", 0, "1+1/2", 5)]
    public void ReadQuotedFieldTest(string line, int startIndex, string expectedValue, int expectedLength)
    {
        var lineParser = new LineParser();
        var actualValue = lineParser.ReadQuotedField(line, startIndex);
        Assert.AreEqual(expectedValue, actualValue);
        Assert.AreEqual(expectedLength, actualValue.Length);
    }

    [TestCase("text", new[] {"text"})]
    [TestCase("hello world", new[] {"hello", "world"})]
    [TestCase("hello 'world'", new[] {"hello", "world"})]
    [TestCase("'hello' world", new[] {"hello", "world"})]
    [TestCase("hello ''", new[] {"hello", ""})]
    [TestCase("hello   world", new[] {"hello", "world"})]
    [TestCase("'hello world'", new[] {"hello world"})]
    [TestCase(" 'hello   world'", new[] {"hello   world"})]
    [TestCase(@"\ a", new[] {@"\", "a"})]
    [TestCase("'", new[] {""})]
    [TestCase("' ", new[] {" "})]
    [TestCase("'a\"'", new[] {"a\""})]
    [TestCase("\"'hello world'\"", new[] {"'hello world'"})]
    [TestCase("hello'world'", new[] {"hello", "world"})]
    [TestCase(@"'\\'", new[] {@"\"})]
    [TestCase(@"'\'", new[] {@"'"})]
    [TestCase(@"\\", new[] {@"\\"})]
    [TestCase("", new string[0])]
    [TestCase("'\"'", new[] {"\""})]
    [TestCase("\"\\\"a\\\"\"", new[] {"\"a\""})]
    [TestCase(@"'\''", new[] {"'"})]
    [TestCase("'\''", new[] {"", ""})]
    [TestCase("\"7 + 3 / 5 * 1\" 0+2/6+7 \"0 / 10 % 10\"", new[] {"7 + 3 / 5 * 1", "0+2/6+7", "0 / 10 % 10"})]
    public void ParseLineTest(string input, string[] expectedResult)
    {
        var lineParser = new LineParser();
        var actualResult = lineParser.ParseLine(input);
        Assert.AreEqual(expectedResult.Length, actualResult.Count);
        CollectionAssert.AreEqual(expectedResult, actualResult);
    }

    [TestCase("1+1/2 2*3", 0, "1+1/2", 5)]
    [TestCase("1+1/2 2*3", 6, "2*3", 3)]
    [TestCase("2+2*2 ", 0, "2+2*2", 5)]
    [TestCase("1 2 3", 0, "1", 1)]
    [TestCase("1 2 3", 2, "2", 1)]
    [TestCase("1 2 3", 4, "3", 1)]
    public void ReadFieldTest(string line, int startIndex, string expectedValue, int expectedLength)
    {
        var lineParser = new LineParser();
        var actualValue = lineParser.ReadField(line, startIndex);
        Assert.AreEqual(expectedValue, actualValue);
        Assert.AreEqual(expectedLength, actualValue.Length);
    }
}