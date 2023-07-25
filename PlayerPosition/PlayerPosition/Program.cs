using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerPositionLib;

namespace PlayerPosition
{
    public class Program
    {
        private static Random rng = new Random();

        static async Task Main(string[] args)
        {
            await Simulation();
        }
        static async Task Simulation()
        {
            LocationTracker field = new(500, 500);
            int playerNumber = 0;
            while(playerNumber < 1 || playerNumber > 24)
            {
                Console.Clear();
                Console.WriteLine("Podaj liczbę zawodników (1-24)");
                playerNumber = int.Parse(Console.ReadLine());
            }
            for(int i = 0; i < playerNumber; i++)
            {
                field.AddPlayer(250, 250);
            }
            //Console.Clear();
            Console.WriteLine("Simulating");

            var tasks = new List<Task>();
            List<Player> playerList = field.PlayerList.Values.ToList();

            //<*>
            int numberOfSomePlayers = playerNumber / 2 == 0 ? 1 : playerNumber / 2;
            var fewPlayers = playerList;

            for(int i = 0; i < 100+(6*playerNumber); i++)
            {
                fewPlayers = playerList.OrderBy(a => rng.Next()).Take(numberOfSomePlayers).ToList();
                foreach(var x in fewPlayers)
                {
                    tasks.Add(Task.Run(() => x.Move()));
                }
            }
            //<*>

            //////////////////////////////////////////////// ????
            //for(int i = 0; i < 3; i++)
            //{
            //    foreach(var x in playerList)
            //    {
            //        tasks.Add(Task.Run(() => x.ChangeLocation(3,4)));
            //    }
            //}
            ////////////////////////////////////////////////

            await Task.WhenAll(tasks);
            Console.WriteLine("Positions and distance after the end of simulation");
            foreach(var x in field.PlayerList)
            {
                if(x.Key == field.GetIdOfLongestDistanceCoveredPlayer())
                    Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(x.Value + " " + field.GetPlayerDistanceCoveredById(x.Key));
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}
