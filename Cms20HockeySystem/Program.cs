using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Cms20HockeySystem
{
    class Program
    {
        static void Test(List<int> ints)
        {
            ints[0] = 101;
        }

        //int Biggest(int a, int b)
        //{
        //    if (a > b) return a;
        //    return b;
        //}

        static Player FindPlayerWithLowestJersey(List<Player> players)
        {
            Player playerWithLowestJerseySoFar = players[0];
            foreach (var p in players)
                if (p.JerseyNumber < playerWithLowestJerseySoFar.JerseyNumber)
                    playerWithLowestJerseySoFar = p;
            return playerWithLowestJerseySoFar;
        }


        static Player FindPlayerByName(List<Player> players, string name)
        {
            foreach (var p in players)
                if (p.Namn == name)
                    return p;
            return null;
        }


        static List<Player> FindPlayersByNationality(List<Player> players, string country)
        {
            List<Player> ret = new List<Player>();
            foreach (var p in players)
            {
                if(p.Country == country)
                    ret.Add(p);
            }

            return ret;
        }

        static void Main(string[] args)
        {
            List<Player> allPlayers = ReadPlayersFromFile();


            //var playersFromSve = allPlayers.Where(p => p.Country == "SWE");
            var playersFromSve = allPlayers.Where(p => p.Country == "SWE" && p.Team == "Detroit")
                .OrderBy(p => p.JerseyNumber).ThenBy(p => p.Namn);
            //var playersFromSve = FindPlayersByNationality(allPlayers, "SWE");

            var p2 = FindPlayerByName(allPlayers, "Stefan Holmberg");
            if (p2 == null)
            {
                Console.WriteLine("Inte en legendarisk spelare");
            }
            p2 = FindPlayerByName(allPlayers, "Mats Sundin");
            if (p2 == null)
            {
                Console.WriteLine("Inte en legendarisk spelare");
            }
            else
            {
                Console.WriteLine($"Du vet väl att han spåelade i nr {p2.JerseyNumber}");
            }


            //HEAP - dynamisk

            var intList = new List<int>();
            intList.Add(100);
            intList.Add(50);
            intList.Add(2100);
            intList.Add(105);
            
            int smallest = intList[0];
            int largest = intList[0];
            foreach (var i in intList)
            {
                if (i < smallest)
                    smallest = i;
                if (i > largest)
                    largest = i;
            }
            //while (true)
            //{
            //    Console.WriteLine("Mata in tal (0 för att avsluta)");
            //    int t = Convert.ToInt32(Console.ReadLine());
            //    if (t == 0) break;
            //    intList.Add(t);
            //}

            //if (intList.Contains(105))
            //{
            //    Console.WriteLine("Hejsan");
            //}
            //intList.Sort();






            foreach (var p in allPlayers)
            {
                Console.WriteLine($"{p.Namn} {p.JerseyNumber} {p.Country} {p.Team}");
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
                    p.Team = split[2];
                    p.Country = split[3];
                    players.Add(p);
                }

            }

            return players;
        }
    }
}
