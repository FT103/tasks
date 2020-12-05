using System.Collections.Generic;
using System.Text;
using ConsoleCoreApp;
using NUnit.Framework;

namespace ConsoleCoreApp
{
    public class StringNumber
    {
        public static long GetNumberFromString(string data)
        {
            var numbersStr = "zero one two three four five six seven eight nine ten eleven twelve thirteen " +
                             "fourteen fifteen sixteen seventeen eighteen nineteen twenty twenty-one twenty-two " +
                             "twenty-three twenty-four twenty-five twenty-six twenty-seven twenty-eight twenty-nine " +
                             "thirty thirty-one thirty-two thirty-three thirty-four thirty-five thirty-six thirty-seven " +
                             "thirty-eight thirty-nine forty forty-one forty-two forty-three forty-four forty-five " +
                             "forty-six forty-seven forty-eight forty-nine fifty fifty-one fifty-two fifty-three " +
                             "fifty-four fifty-five fifty-six fifty-seven fifty-eight fifty-nine sixty sixty-one " +
                             "sixty-two sixty-three sixty-four sixty-five sixty-six sixty-seven sixty-eight sixty-nine " +
                             "seventy seventy-one seventy-two seventy-three seventy-four seventy-five seventy-six " +
                             "seventy-seven seventy-eight seventy-nine eighty eighty-one eighty-two eighty-three " +
                             "eighty-four eighty-five eighty-six eighty-seven eighty-eight eighty-nine ninety ninety-one " +
                             "ninety-two ninety-three ninety-four ninety-five ninety-six ninety-seven ninety-eight " +
                             "ninety-nine hundred";
            var numbers = numbersStr.Split(' ');
            var dic = new Dictionary<string, int>();
            for (var i = 0; i < numbers.Length; i++)
                dic.Add(numbers[i], i);
            //Console.WriteLine($"{numbers[i]}: {i}");
            var sb = new StringBuilder();
            var words = data.Split(' ');
            var numNow = 0;
            var stringFormat = "000";
            for (var i = 0; i < words.Length; i++)
                if (words[i] == "million" || words[i] == "thousand" || words[i] == "billion")
                {
                    sb.Append(numNow.ToString(stringFormat));
                    numNow = 0;
                }
                else if (words[i] == "hundred")
                {
                    numNow -= dic[words[i - 1]];
                    numNow += dic[words[i - 1]] * dic[words[i]];
                }
                else
                {
                    numNow += dic[words[i]];
                }

            sb.Append(numNow.ToString());
            var result = long.Parse(sb.ToString());
            return result;
        }
    }
}

[TestFixture]
class StringNumberTests
{
    [TestCase("sixty-nine million nine hundred ninety-three thousand one hundred forty-two", "69993142")]
    [TestCase("one billion three hundred nine million two hundred thirty-five thousand two hundred forty-five", "1309235245")]
    [TestCase("one billion seven hundred twelve million five hundred ninety-one thousand one hundred two", "1712591102")]
    [TestCase("one billion seven hundred twenty-five million six hundred twenty thousand two hundred twenty-seven", "1725620227")]
    [TestCase("five hundred fifty-five million four hundred fifty-three thousand six hundred eighty", "555453680")]
    [TestCase("one billion seven hundred thirty-three million five hundred seventy-five thousand six hundred sixty-three", "1735575663")]
    [TestCase("seven hundred ninety million five hundred fifty-seven thousand two hundred thirty-two", "790557232")]
    [TestCase("nine hundred eighty-five million forty-six thousand seven hundred eight", "985046708")]
    
    [TestCase("eight hundred thirty-six million two hundred sixty-nine thousand eighty", "836269080")]
    [TestCase("five hundred fifty-five million four hundred fifty-three thousand six hundred eighty", "555453680")]
    [TestCase("five hundred fifty-five million four hundred fifty-three thousand six hundred eighty", "555453680")]
    [TestCase("five hundred fifty-five million four hundred fifty-three thousand six hundred eighty", "555453680")]
    
    public void ResultTest(string task, string expected)
    {
        var actual = StringNumber.GetNumberFromString(task).ToString();
        Assert.AreEqual(expected, actual);
    }
}