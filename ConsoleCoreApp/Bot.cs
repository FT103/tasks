using System;

namespace ConsoleCoreApp
{
    public class Bot
    {
        public static string GetAnswer(string questionType, string question)
        {
            if (questionType == "math") return MathParser.GetAnswer(question);
            if (questionType == "polynomial-root") return Polynomial.GetRoot(question);
            if (questionType == "determinant") return Matrix.GetAnswer(question).ToString();
            if (questionType == "moment") return MomentAnswer.GetAnswer(question);
            if (questionType == "cypher") return Cypher.GetAnswer(question);
            if (questionType == "statistics") return Statistics.GetAnswer(question);
            return Console.ReadLine();
        }
    }
}