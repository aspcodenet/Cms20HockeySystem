using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace Cms20HockeySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var allPlayers = ReadPlayersFromFile();
            foreach (var p in allPlayers)
            {
                Console.WriteLine($"{p.Namn} {p.JerseyNumber}");
            }
            Console.WriteLine("Hello World!");
        }

        private static List<Player> ReadPlayersFromFile()
        {
            var players = new List<Player>();

            using (var sr =
                File.OpenText(@"C:\Users\stefan\source\repos\Cms20HockeySystem\Cms20HockeySystem\HockeyPlayers.txt"))
            {
                while (true)
                {
                    var line = sr.ReadLine();
                    if (line == null) break;
                    //Mats Sundin,13
                    string[] split = line.Split(',');
                    var p = new Player();
                    p.Namn = split[0];
                    p.JerseyNumber = Convert.ToInt32(split[1]);
                    players.Add(p);
                }

            }

            return players;
        }
    }
}
