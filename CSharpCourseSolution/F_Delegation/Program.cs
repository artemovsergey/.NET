using F_Delegation.Sticks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace F_Delegation
{
    class Program
    {        
        static void Main(string[] args)
        {
            
        }

        static void HW_FindRusChessPlayers()
        {
            var players = File.ReadAllLines(@"Top100ChessPlayers.csv")
                              .Skip(1)
                              .Select(ChessPlayer.ParseFideCsvLine)
                              .Where(player => player.Country == "RUS")
                              .OrderBy(player => player.BirthYear)
                              .ToList();

            foreach (var player in players)
            {
                Console.WriteLine(player);
            }

            Console.ReadLine();
        }

        static void RemoveInFor()
        {
            var list = new List<int> { 0, 1, 2, 3, 4, 5 };
            for(int i=0; i<list.Count; i++)
            {
                var item = list[i];
                if (item <= 3)
                {
                    list.Remove(item);
                    i--;
                }
            }
            Console.WriteLine(list.Count == 2);
        }

        static void RemoveInForBackwards()
        {
            var list = new List<int> { 0, 1, 2, 3, 4, 5 };
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var item = list[i];
                if (item <= 3)
                {
                    list.Remove(item);
                }
            }
            Console.WriteLine(list.Count == 2);
        }

        static void RemoveAllDemo()
        {
            var list = new List<int> { 0, 1, 2, 3, 4, 5 };
            list.RemoveAll(x => x % 2 == 0);

            foreach(var item in list)
            {
                Console.WriteLine(item);
            }
        }

        static void RemoveInForeach()
        {
            var list = new List<int> { 0, 1, 2, 3, 4, 5 };
            foreach(var item in list)
            {
                if (item % 2 == 0)
                {
                    list.Remove(item);
                }
            }
            Console.WriteLine(list.Count);

            List<int>.Enumerator enumerator = list.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    int item = enumerator.Current;
                }
            }
            finally
            {
                enumerator.Dispose();
            }
        }

        static void DeferredExecutionDemo()
        {
            var list = new List<int> { 1, 2, 3 };
            var query = list.Where(c => c >= 2).ToList();
            list.Remove(3);

            //Console.WriteLine(query.Count());
            foreach (var item in query)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }

        static void LinqDemo(string file)
        {
            IEnumerable<ChessPlayer> list = File.ReadAllLines(file)
                                         .Skip(1)
                                         .Select(ChessPlayer.ParseFideCsvLine)
                                         //old-style anonymous method syntax
                                         //.Where(delegate(ChessPlayer player) { return player.BirthYear > 1988; }) 
                                         .Where(player => player.BirthYear > 1988)
                                         .OrderByDescending(player => player.Rating)
                                         .ThenBy(player=>player.Country)
                                         .Take(10)
                                         .ToList();

            //SQL-like syntax:
            //IEnumerable<ChessPlayer> list2 = File.ReadAllLines(file)
            //                             .Skip(1)
            //                             .Select(ChessPlayer.ParseFideCsv);
            //var filtered = from player in list2
            //               where player.BirthYear > 1988
            //               orderby player.Rating descending
            //               select player;

            //problem of mutiple enumeration in case greedy operator was not called at the end of LINQ query
            foreach (var player in list)
            {
                Console.WriteLine(player);
            }

            foreach (var player in list)
            {
                Console.WriteLine(player);
            }

            Console.WriteLine($"The lowest rating in TOP 10: {list.Min(x=>x.Rating)}");
            Console.WriteLine($"The highest rating in TOP 10: {list.Max(x => x.Rating)}");
            Console.WriteLine($"The average rating in TOP 10: {list.Average(x => x.Rating)}");

            Console.WriteLine(list.First());
            Console.WriteLine(list.Last());

            Console.WriteLine(list.First(player=>player.Country == "USA"));
            Console.WriteLine(list.Last(player => player.Country == "USA"));

            ChessPlayer firstFromBra = list.FirstOrDefault(player => player.Country == "BRA");
            if (firstFromBra != null)
            {
                Console.WriteLine(firstFromBra.LastName);
            }

            var lastFromBra = list.LastOrDefault(player => player.Country == "BRA");

            Console.WriteLine(list.Single(player=>player.Country == "FRA"));
            Console.WriteLine(list.SingleOrDefault(player => player.Country == "BRA"));

            Console.WriteLine(list.SingleOrDefault(player => player.Country == "USA"));
        }

        private static void DisplayLargestFilesWithLinq(string pathToDir)
        {
            new DirectoryInfo(pathToDir)
                .GetFiles()
                .OrderByDescending(file => file.Length)
                .Take(5)
                .ForEach(file => Console.WriteLine($"{file.Name} weights {file.Length}"));

            //IEnumerable<FileInfo> orderedFiles = new DirectoryInfo(pathToDir)
            //    .GetFiles()
            //    .OrderBy(file => file.Length)
            //    .Take(5);

            //foreach(var file in orderedFiles)
            //{
            //    Console.WriteLine($"{file.Name} weights {file.Length}");
            //}
        }

        //static long KeySelector(FileInfo file)
        //{
        //    return file.Length;
        //}

        private static void DisplayLargestFilesWithoutLinq(string pathToDir)
        {
            var dirInfo = new DirectoryInfo(pathToDir);
            FileInfo[] files = dirInfo.GetFiles();

            Array.Sort(files, FilesComparison);

            for(int i=0; i<5; i++)
            {
                FileInfo file = files[i];
                Console.WriteLine($"{file.Name} weights {file.Length}");
            }
        }

        static int FilesComparison(FileInfo x, FileInfo y)
        {
            if (x.Length == y.Length) return 0;
            if (x.Length > y.Length) return -1;
            return 1;
        }

        #region SticksGame

        static void HW_SticksGame()
        {
            var game = new SticksGame(10, Player.Human);
            game.MachinePlayed += Game_MachinePlayed;
            game.HumanTurnToMakeMove += Game_HumanTurnToMakeMove;
            game.EndOfGame += Game_EndOfGame;

            game.Start();
        }

        private static void Game_EndOfGame(Player player)
        {
            Console.WriteLine($"Winner:{player}");
        }

        private static void Game_HumanTurnToMakeMove(object sender, int remainingSticks)
        {
            Console.WriteLine($"Remaining sticks:{remainingSticks}");
            Console.WriteLine("Take some sticks");

            bool takenCorrectly = false;
            while (!takenCorrectly)
            {
                if(int.TryParse(Console.ReadLine(), out int takenSticks))
                {
                    var game = (SticksGame)sender;

                    try
                    {
                        game.HumanTakes(takenSticks);
                        takenCorrectly = true;
                    }
                    catch(ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private static void Game_MachinePlayed(int sticksTaken)
        {
            Console.WriteLine($"Machine took:{sticksTaken}");
        }

        #endregion

        private static void EventsDemo()
        {
            Timer timer = new Timer();
            timer.Elapsed += Timer_Elapsed;

            timer.Interval = 5000;
            timer.Start();

            Console.ReadLine();

            Car car = new Car();
            car.TooFastDriving += HandleOnTooFast;
            //car.TooFastDriving += HandleOnTooFast;

            //car.TooFastDriving -= HandleOnTooFast;

            car.Start();

            for (int i = 0; i < 10; i++)
            {
                car.Accelerate();
            }

            Console.ReadLine();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //var timer = (Timer)sender;
            Console.WriteLine("Handling Timer Elapsed Event");
        }

        private static void HandleOnTooFast(object obj, CarArgs speed)
        {            
            Console.WriteLine($"Oh, I got it, calling stop! Current Speed={speed}");
            var car = (Car)obj;
            car.Stop();
        }
    }
}
