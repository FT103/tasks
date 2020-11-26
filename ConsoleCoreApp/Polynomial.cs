using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace ConsoleCoreApp
{
    public class Polynomial
    {
        public static string GetRoot(string task)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            
            var stringArray = task.Split(" + " );
            var arraySize = stringArray.Length;
            var coefficientArray = new double [arraySize];
            for (var i = 0; i < stringArray.Length; i++)
            {
                var coefficient = double.Parse(stringArray[i].Split('*')[0].Trim('(', ')'), 
                    CultureInfo.InvariantCulture);
                coefficientArray[i] = coefficient;
            }
            
            var processInfo = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = @"ConsoleCoreApp/Polynomial-root.py" + " " 
                    + String.Join(" ", coefficientArray),
                RedirectStandardInput = false,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            string result;

            using (var process = Process.Start(processInfo))
            {
                using (var reader = process.StandardOutput)
                {
                    result = reader.ReadToEnd();
                }
            }

            var answer = result.Trim('[', ']', '\r', '\n').Split(' ', 
                StringSplitOptions.RemoveEmptyEntries);
            double root = Double.MinValue;
            foreach (var lol in answer)
            {
                if (lol.Contains("0.j") || !lol.Contains('j') || lol.Contains("+0j"))
                {
                    root = double.Parse(lol.Split('+')[0]);
                    break;
                }
            }

            return Math.Abs(root - double.MinValue) < 1e-5 ? "no roots" : root.ToString();
        }

        private static string DeleteBrackets(string koef)
        {
            if (koef[0] == '(')
                return koef.Substring(1, koef.Length - 2);
            return koef;
        }
    }
}