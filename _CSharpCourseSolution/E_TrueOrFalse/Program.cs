using System;

namespace G_TrueOrFalse
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game("Questions.csv");
            game.EndOfGame += (sender, e) =>
              {
                  Console.WriteLine($"Questions asked:{e.QuestionsPassed}. Mistakes made:{e.MistakesMade}");
                  Console.WriteLine(e.IsWin ? "You won!" : "You lost!");
              };

            while(game.GameStatus == GameStatus.GameInProgress)
            {
                Question q = game.GetNextQuestion();
                Console.WriteLine("Dou you believe in the next statement or question? Enter 'y' or 'n'");
                Console.WriteLine(q.Text);

                string answer = Console.ReadLine();
                bool boolAnswer = answer == "y";

                if(q.CorrectAnswer == boolAnswer)
                {
                    Console.WriteLine("Good job. You're right!");
                }
                else
                {
                    Console.WriteLine("Ooops, actually you're mistaken. Here is the commentary:");
                    Console.WriteLine(q.Explanation);
                }

                game.GiveAnswer(boolAnswer);
            }

            Console.ReadLine();
        }
    }
}
