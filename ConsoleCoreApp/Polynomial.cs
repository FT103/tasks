using System;
using System.Globalization;
using Challenge.DataContracts;

namespace ConsoleCoreApp
{
    public class Polynomial
    {
        public static string GetRoot(string task)
        {
            var parts = new string[2];
            var otherKoef = new string[2];
            task = task.Replace(" ", "");
            if (!IsQuadraticPolynomial(task))
            {
                otherKoef = task.Split("*x+");
                if (otherKoef[0] == "x") otherKoef[0] = "1";
            }
            else
            {
                parts = task.Split("*x^2+");
                if (parts[0] == task) parts[0] = "1";
                parts[0] = SearchZeros(parts[0]);
                if (IsSecondCoef(task)) otherKoef = parts[1].Split("*x+");
                else
                {
                    otherKoef[0] = "0";
                    otherKoef[1] = task.Split('+')[1];
                }
            }
            otherKoef[0] = SearchZeros(otherKoef[0]);
            otherKoef[1] = SearchZeros(otherKoef[1]);
            double a=0; double b=0; double c=0;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            if (IsQuadraticPolynomial(task)) a = double.Parse(parts[0]);
            b = double.Parse(otherKoef[0]);
            c = double.Parse(otherKoef[1]);
            if (!IsQuadraticPolynomial(task)) return (-c / b).ToString(CultureInfo.CurrentCulture); //root = -c / b
            if (b * b - 4 * a * c < 0) return "no roots"; // return
            return ((-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a)).ToString(CultureInfo.CurrentCulture);
        }

        private static string DeleteBrackets(string koef)
        {
            if (koef[0] == '(')
                return koef.Substring(1, koef.Length - 2);
            return koef;           
        }

        private static string SearchZeros (string koef)
        {
            if (koef.Length != 0) return DeleteBrackets(koef);
            else return "0";
        }

        private static bool IsQuadraticPolynomial (string task)
        {
            foreach (var symbol in task)
                if (symbol == '^') return true;
            return false;
        }
        
        private static bool IsSecondCoef (string task)
        {
            for (var i = 0; i < task.Length - 1; i++)
                if ((task[i] == 'x') && (task[i + 1] == '+')) return true;
            return false;
        }
    }
}