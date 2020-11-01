using System;

namespace ConsoleCoreApp
{
    public class Matrix
    {
        public static int GetAnswer(string str)
        {
            var matrix = GetMatrix(str);
            if (matrix.GetLength(0) == 2) return GetDeterminant2(matrix);
            if (matrix.GetLength(0) == 3) return GetDeterminant3(matrix);
            return GetDeterminant4(matrix);
        }


        private static int[,] GetMatrix(string task)
        {
            var strInMatrix = task.Split(new[] {@" \\ "}, StringSplitOptions.None);
            var outputData = new int[strInMatrix.Length, strInMatrix.Length];
            for (var i = 0; i < strInMatrix.Length; i++)
            {
                var elementsInStr = strInMatrix[i].Split(new[] {" & "}, StringSplitOptions.None);
                for (var j = 0; j < elementsInStr.Length; j++) outputData[i, j] = int.Parse(elementsInStr[j]);
            }

            return outputData;
        }

        private static int GetDeterminant(int[,] matrix)
        {
            var determinant = 0;
            var len = matrix.GetLength(0);
            for (var i = 0; i < len; i++)
            {
                var k = i;
                var prod = 1;
                for (var j = 0; j < len; j++)
                    prod *= matrix[k++ % len, j % len];

                determinant += prod;
            }


            for (var i = 0; i < len; i++)
            {
                var x = i;
                var prod = 1;
                for (var j = 0; j > -len; j--)
                    prod *= matrix[x++ % len, (j + len) % len];

                determinant -= prod;
            }

            return determinant;
        }

        private static int GetDeterminant3(int[,] matrix)
        {
            return matrix[0, 0] * matrix[1, 1] * matrix[2, 2] +
                   matrix[0, 1] * matrix[1, 2] * matrix[2, 0] +
                   matrix[0, 2] * matrix[1, 0] * matrix[2, 1] -
                   matrix[0, 2] * matrix[1, 1] * matrix[2, 0] -
                   matrix[0, 1] * matrix[1, 0] * matrix[2, 2] -
                   matrix[0, 0] * matrix[1, 2] * matrix[2, 1];
        }

        private static int GetDeterminant2(int[,] matrix)
        {
            return matrix[0, 0] * matrix[1, 1] -
                   matrix[0, 1] * matrix[1, 0];
        }

        private static int GetDeterminant4(int[,] matrix)
        {
            return matrix[0,0] * GetDeterminant3(new [,] {{matrix[1,1], matrix[1,2], matrix[1,3]}, {matrix[2,1], matrix[2,2], matrix[2,3]}, {matrix[3,1], matrix[3,2], matrix[3,3]}}) 
                - matrix[0,1] * GetDeterminant3(new [,] {{matrix[1,0], matrix[1,2], matrix[1,3]}, {matrix[2,0], matrix[2,2], matrix[2,3]}, {matrix[3,0], matrix[3,2], matrix[3,3]}}) 
                + matrix[0,2] * GetDeterminant3(new [,] {{matrix[1,0], matrix[1,1], matrix[1,3]}, {matrix[2,0], matrix[2,1], matrix[2,3]}, {matrix[3,0], matrix[3,1], matrix[3,3]}})
                - matrix[0,3] * GetDeterminant3(new [,] {{matrix[1,0], matrix[1,1], matrix[1,2]}, {matrix[2,0], matrix[2,1], matrix[2,2]}, {matrix[3,0], matrix[3,1], matrix[3,2]}});
        }
    }
}