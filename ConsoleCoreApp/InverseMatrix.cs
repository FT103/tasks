using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace ConsoleCoreApp
{
    public class InverseMatrix
    {
        public static string InverseMatrix1(string task)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var processInfo = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = @"ConsoleCoreApp/InverseMatrix.py" + " " + task,
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

            return result;
        }
    }
}