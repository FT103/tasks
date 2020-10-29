namespace ConsoleCoreApp
{
    public class Bot
    {
        public string GetAnswer(string questionType, string question)
        {
            if (questionType == "math") return MathParser.GetAnswer(question);
            if (questionType == "polynomial-root") return Polynomial.GetRoot(question);
            if (questionType == "determinant") return Matrix.GetAnswer(question).ToString();
            if (questionType == "moment") return MomentAnswer.GetAnswer(question);
            return Reverse.ReverseStringBuilder(question);
        }
    }
}