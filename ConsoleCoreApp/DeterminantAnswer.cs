using System;

namespace ConsoleCoreApp
{
    public class DeterminantAnswer
    {
        public static int[,] GetAnswer(string task)
        {
            var outputData = new int[3, 3];
            var strInMatrix = task.Split(new[] {@" \\ "}, StringSplitOptions.None);
            for (var i = 0; i < strInMatrix.Length; i++)
            {
                var elementsInStr = strInMatrix[i].Split(new[] {" & "}, StringSplitOptions.None);
                for (var j = 0; j < elementsInStr.Length; j++) outputData[i, j] = int.Parse(elementsInStr[j]);
            }

            return outputData;
        }
    }
}