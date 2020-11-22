using System;
using System.Threading.Tasks;
using Challenge;
using TaskStatus = Challenge.DataContracts.TaskStatus;

namespace ConsoleCoreApp
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            const string teamSecret = "I6o8UqNPbYCzxbWGNz3gzDbFXLjtyh0K";
            if (string.IsNullOrEmpty(teamSecret))
            {
                Console.WriteLine("Задай секрет своей команды, чтобы можно было делать запросы от ее имени");
                return;
            }

            var challengeClient = new ChallengeClient(teamSecret);

            const string challengeId = "projects-course";
            //Console.WriteLine($"Нажми ВВОД, чтобы получить информацию о соревновании {challengeId}");
            //Console.ReadLine();
            //Console.WriteLine("Ожидание...");
            var challenge = await challengeClient.GetChallengeAsync(challengeId);
            Console.WriteLine(challenge.Description);
            Console.WriteLine();
            Console.WriteLine("----------------");
            Console.WriteLine();
            const string taskType = "cypher"; //название задачи

            var utcNow = DateTime.UtcNow;
            string currentRound = null;
            foreach (var round in challenge.Rounds)
                if (round.StartTimestamp < utcNow && utcNow < round.EndTimestamp)
                    currentRound = round.Id;

            // Console.WriteLine(
            //     $"Нажми ВВОД, чтобы получить первые 50 взятых командой задач типа {taskType} в раунде {currentRound}");
            // Console.ReadLine();
            // Console.WriteLine("Ожидание...");
            var firstTasks = await challengeClient.GetTasksAsync(currentRound, taskType, TaskStatus.Pending);
            for (var i = 0; i < firstTasks.Count; i++)
            {
                var task = firstTasks[i];

                Console.WriteLine($"  Задание {i + 1}, статус {task.Status}");
                Console.WriteLine($"  Формулировка: {task.UserHint}");
                Console.WriteLine($"                {task.Question}");
                Console.WriteLine();
            }

            Console.WriteLine("----------------");
            Console.WriteLine();

            Console.WriteLine($"Нажми ВВОД, чтобы получить задачу типа {taskType} в раунде {currentRound}");
            Console.ReadLine();
            Console.WriteLine("Ожидание...");


            while (true)
            {
                var newTask = await challengeClient.AskNewTaskAsync(currentRound, taskType);
                Console.WriteLine($"  Новое задание, статус {newTask.Status}");
                Console.WriteLine($"  Формулировка: {newTask.UserHint}");
                Console.WriteLine($"                {newTask.Question}");
                Console.WriteLine();
                Console.WriteLine("----------------");
                Console.WriteLine($"Task id: {newTask.TypeId}");
                
                var answer = Bot.GetAnswer(newTask.TypeId, newTask.Question);
                Console.WriteLine(
                    $"Нажми ВВОД, чтобы ответить на полученную задачу самым правильным ответом: {answer}");
                //Console.ReadLine();
                Console.WriteLine("Ожидание...");
                var updatedTask = await challengeClient.CheckTaskAnswerAsync(newTask.Id, answer);
                Console.WriteLine($"  Новое задание, статус {updatedTask.Status}");
                Console.WriteLine($"  Формулировка:  {updatedTask.UserHint}");
                Console.WriteLine($"                 {updatedTask.Question}");
                Console.WriteLine($"  Ответ команды: {updatedTask.TeamAnswer}");
                Console.WriteLine();
                if (updatedTask.Status == TaskStatus.Success)
                {
                    Console.WriteLine("Ура! Ответ угадан!");
                }
                else if (updatedTask.Status == TaskStatus.Failed)
                {
                    Console.WriteLine("Похоже ответ не подошел и задачу больше сдать нельзя...");
                    //Console.ReadLine();
                    break;
                }

                Console.WriteLine();
                Console.WriteLine("----------------");
                Console.WriteLine();
            }
        }
    }
}