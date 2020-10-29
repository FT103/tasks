namespace ConsoleCoreApp
{
    public class Bot
    {
        public static object GetAnswer(string questionType, string question)
        {
            if (questionType == "math") return MathParser.GetAnswer(question);
            if (questionType == "polynomial-root") return Polynomial.GetRoot(question);
            if (questionType == "determinant") return Matrix.GetAnswer(question);
            if (questionType == "moment") return MomentAnswer.GetAnswer(question);
            return 0;
        }
    }
}