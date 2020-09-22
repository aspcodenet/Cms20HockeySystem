using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Net.Sockets;

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

            while (true)
            {
                Console.WriteLine("1. Add new");
                Console.WriteLine("2. Edit");
                Console.WriteLine("3. Break");
                string sel = Console.ReadLine();
                if (sel == "1")
                {
                    var newPlayer = CreatePlayer();
                    allPlayers.Add(newPlayer);
                }
                else if (sel == "2")
                {
                    EditPlayer(allPlayers);
                }
                else if (sel == "3")
                {
                    SaveListToFile(allPlayers);
                    break;
                }

            }
            Console.WriteLine("Hello World!");
        }

        private static void SaveListToFile(List<Player> allPlayers)
        {
            using (var sw =
                new StreamWriter(@"..\..\..\HockeyPlayers.txt"))
            {
                foreach (var p in allPlayers)
                {
                    var line = $"{p.Namn},{p.JerseyNumber}";
                    sw.WriteLine(line);
                }
            }


        }

        private static void EditPlayer(List<Player> allPlayers)
        {
            int index = 1;
            foreach (var p in allPlayers)
            {
                Console.WriteLine($"{index}. {p.Namn}");
                index++;
            }

            Console.WriteLine("Enter index to edit");
            int sel = Convert.ToInt32(Console.ReadLine());

            var p2 = allPlayers[sel - 1];
            Console.WriteLine($"Nu editerart vi {p2.Namn}");

            Console.WriteLine($"Skriv in nytt namm eller blank för att låta {p2.Namn} vara klar");
            var namn = Console.ReadLine();
            if (!string.IsNullOrEmpty(namn))
                p2.Namn = namn;

            //var p = GetPlayerFromName(allPlayers, namn);

        }

        private static Player GetPlayerFromName(List<Player> allPlayers, string namn)
        {
            foreach(var p in allPlayers)
                if (p.Namn == namn)
                    return p;
            return null;
        }

        private static Player CreatePlayer()
        {
            Console.WriteLine("Namn");
            var s = Console.ReadLine();
            Console.WriteLine("Jersey");
            int j = Convert.ToInt32(Console.ReadLine());
            var p = new Player();
            p.Namn = s;
            p.JerseyNumber = j;
            return p;
        }

        private static List<Player> ReadPlayersFromFile()
        {
            var players = new List<Player>();

            //using (var sr =
            //    File.OpenText(@".\HockeyPlayers.txt"))
            using (var sr =
                File.OpenText(@"..\..\..\HockeyPlayers.txt"))

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
