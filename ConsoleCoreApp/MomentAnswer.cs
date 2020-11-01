namespace ConsoleCoreApp
{
    public class MomentAnswer
    {
        public static string GetAnswer(string task)
        {
            var months = new[]
            {
                "января", "февраля", "марта", "апреля", "мая", "июня",
                "июля", "августа", "сентября", "октября", "ноября", "декабря"
            };
            var parts = task.Split(' ');
            var time = parts[0].Split(':');
            var data = parts[1].Split('.');
            var result = int.Parse(data[0]) + " " + months[int.Parse(data[1]) - 1] + " " + time[0] + ":" + time[1];
            return result;
        }
    }
}