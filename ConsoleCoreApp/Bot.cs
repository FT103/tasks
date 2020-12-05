using System;
using System.Collections.Generic;
using ConsoleCoreApp;
using NUnit.Framework;

namespace ConsoleCoreApp
{
    public class Bot
    {
        public static string GetAnswer(string question)
        {
            var answer = new List<string>();
            var lineParser = new LineParser();
            var tasksList = lineParser.ParseLine(question);
            foreach (var task in tasksList)
            {
                var parts = task.Split('|');
                var answerByType = GetAnswerByType(parts[0], parts[1]);
                answer.Add($"\"{answerByType}\"");
            }
            return string.Join(" ", answer);
        }
        public static string GetAnswerByType(string questionType, string question)
        {
            if (questionType == "math") return MathTask.GetAnswer(question);
            if (questionType == "polynomial-root") return Polynomial.GetRoot(question);
            if (questionType == "determinant") return Matrix.GetAnswer(question).ToString();
            if (questionType == "moment") return MomentAnswer.GetAnswer(question);
            if (questionType == "cypher") return Cypher.GetAnswer(question);
            if (questionType == "statistics") return Statistics.GetAnswer(question);
            if (questionType == "string-number") return StringNumber.GetNumberFromString(question).ToString();
            if (questionType == "inverse-matrix") return InverseMatrix.InverseMatrix1(question).ToString();
            if (questionType == "json") return JsonParser.GetValuesSum(question).ToString();
            return Console.ReadLine();
        }
    }
}

[TestFixture]
class BotTests
{
    [TestCase("\"some text\" \"another text\"", new []{"some text", "another text"})]
    [TestCase("\"string-number|one hundred eighteen million four hundred fifty-four thousand four hundred seventy-six\" \"string-number|five hundred fifty-one million two hundred sixty-two thousand eight hundred eighty-five\"", 
        new []{"string-number|one hundred eighteen million four hundred fifty-four thousand four hundred seventy-six", "string-number|five hundred fifty-one million two hundred sixty-two thousand eight hundred eighty-five"})]
    public void ParserTest(string text, string[] expected)
    {
        var lineParser = new LineParser();
        var actual = lineParser.ParseLine(text);
        CollectionAssert.AreEqual(expected, actual);
    }
    
    [TestCase("\"string-number|one hundred eighteen million four hundred fifty-four thousand four hundred seventy-six\" \"string-number|five hundred fifty-one million two hundred sixty-two thousand eight hundred eighty-five\"", 
         "\"118454476\" \"551262885\"")]
    public void ResultTest(string text, string expected)
    {
        var lineParser = new LineParser();
        var actual = Bot.GetAnswer(text); 
        Assert.AreEqual(expected, actual);
    }
}