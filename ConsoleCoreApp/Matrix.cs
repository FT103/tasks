using System.Text;

namespace ConsoleCoreApp
{
    public class Matrix
    {
        public static int GetAnswer(string str) =>
            GetDeterminant2(GetMatrix(str));
        
        

        public static int[,] GetMatrix(string task)
        {
            var builder = new StringBuilder();
            var isNegativeNumber = false;
            var outputData = new int[3, 3];
            var xIndex = 0;
            var yIndex = 0;
            var firstSlash = false;
            foreach (var symbol in task)
            {
                if (firstSlash)
                {
                    firstSlash = false;
                    continue;
                }

                if (symbol == '-')
                {
                    isNegativeNumber = true;
                    continue;
                }

                if (symbol == ' ')
                {
                    continue;
                }

                if (symbol == '&')
                {
                    outputData[yIndex, xIndex] = isNegativeNumber
                        ? -1 * int.Parse(builder.ToString())
                        : int.Parse(builder.ToString());
                    isNegativeNumber = false;
                    builder.Clear();
                    xIndex++;
                    continue;
                }

                if (symbol == '\\')
                {
                    outputData[yIndex, xIndex] = isNegativeNumber
                        ? -1 * int.Parse(builder.ToString())
                        : int.Parse(builder.ToString());
                    isNegativeNumber = false;
                    builder.Clear();
                    firstSlash = true;
                    yIndex++;
                    xIndex = 0;
                    continue;
                }

                if (char.IsDigit(symbol))
                {
                    builder.Append(symbol);
                }
            }

            outputData[yIndex, xIndex] = isNegativeNumber
                ? -1 * int.Parse(builder.ToString())
                : int.Parse(builder.ToString());
            return outputData;
        }

        public static int GetDeterminant(int[,] matrix)
        {
            var fDeterminant = 0;
            var sDeterminant = 0;
            for (var i = 0; i < 3; i++)
            {
                var line = 1;
                for (var j = 0; j < 3; j++)
                {
                    line *= matrix[j, (i + j) % 3];
                }

                fDeterminant += line;
            }

            for (var i = 3 - 1; i >= 0; i--)
            {
                var line = 1;
                for (var j = 0; j < 3; j++)
                {
                    var p = (i - j) % 3;
                    if (p < 0) p += 3;
                    line *= matrix[j, p];
                }

                sDeterminant -= line;
            }

            return sDeterminant;
        }

        public static int GetDeterminant2(int[,] matrix)
        {
            return matrix[0, 0] * matrix[1, 1] * matrix[2, 2] +
                   matrix[0, 1] * matrix[1, 2] * matrix[2, 0] +
                   matrix[0, 2] * matrix[1, 0] * matrix[2, 1] -
                   matrix[0, 2] * matrix[1, 1] * matrix[2, 0] -
                   matrix[0, 1] * matrix[1, 0] * matrix[2, 2] -
                   matrix[0, 0] * matrix[1, 2] * matrix[2, 1];
        }
    }
}