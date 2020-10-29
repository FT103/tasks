using System;
using System.Text;

namespace ConsoleCoreApp
{
    public class Matrix
    {
        public static int GetAnswer(string str) =>
            GetDeterminant2(GetMatrix(str));
        
        

        private static int[,] GetMatrix(string task) 
        { 
            var outputData = new int[3, 3]; 
            var strInMatrix = task.Split(new string[] {@" \\ "},StringSplitOptions.None); 
            for (int i = 0; i < strInMatrix.Length; i++) 
            { 
                var elementsInStr = strInMatrix[i].Split(new string[]{" & "}, StringSplitOptions.None); 
                for (int j = 0; j < elementsInStr.Length; j++) 
                { 
                    outputData[i, j] = int.Parse(elementsInStr[j]); 
                } 
            } 
            return outputData; 
        }
        private static int GetDeterminant(int[,] matrix)
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

        private static int GetDeterminant2(int[,] matrix)
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
