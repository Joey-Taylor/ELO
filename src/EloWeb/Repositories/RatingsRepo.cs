﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EloWeb.Models;

namespace EloWeb.Repositories
{
    public class RatingsRepo
    {
        private static string _path;

        private static Dictionary<String, Player> _players;
        private const string PlayersFile = "Players.txt";

        private static List<Game> _games;
        private const string GamesFile = "Games.txt";
                    
        public static void Load(string path)
        {
            _path = path;

            if (!Directory.Exists(path))
                CreateNewDataFiles();

            _games = LoadGames(); 
            _players = LoadPlayers();
            RefreshRatings();       
        }

        private static void CreateNewDataFiles()
        {
            Directory.CreateDirectory(_path);
        }

        public static Dictionary<String, Player> LoadPlayers()
        {
            try
            {
                using (var file = new StreamReader(_path + PlayersFile))
                {
                    return LoadPlayers(file);
                }
            }
            catch (FileNotFoundException)
            {
                return new Dictionary<string, Player>();                
            }
        }

        public static Dictionary<string, Player> LoadPlayers(StreamReader file)
        {
            var players = new Dictionary<String, Player>();
            string line;

            while ((line = file.ReadLine()) != null)
                players.Add(line, Player.CreateInitial(line));

            return players;
        }

        public static void AddPlayer(string name)
        {
            _players.Add(name, Player.CreateInitial(name));
            WritePlayerToFile(name);
        }

        private static void WritePlayerToFile(string name)
        {
            File.AppendAllText(_path + PlayersFile, name + "\n");
        }

        public static Dictionary<String, Player> Players()
        {
            return _players;
        }

        public static Player PlayerByName(string name)
        {
            return _players[name];
        }

        public static IEnumerable<String> PlayerNames()
        {
            return _players.Values.Select(p => p.Name);
        }



        public static List<Game> LoadGames()
        {
            try
            {
                using (var file = new StreamReader(_path + GamesFile))
                {
                    return LoadGames(file);
                }
            }
            catch (FileNotFoundException)
            {
                return new List<Game>();
            }
        }

        public static List<Game> LoadGames(StreamReader file)
        {
            var games = new List<Game>();
            string line;

            while ((line = file.ReadLine()) != null)
                games.Add(Game.Create(line));

            return games;
        }

        public static void AddGame(Game game)
        {
            _games.Add(game);
            WriteGameToFile(game);
            RateGame(game);
        }

        private static void WriteGameToFile(Game game)
        {
            File.AppendAllText(_path + GamesFile, game + "\n");
        }

        public static void RefreshRatings()
        {
            foreach (var game in _games)
                RateGame(game);
        }

        private static void RateGame(Game game)
        {
            var winner = _players[game.Winner];
            var loser = _players[game.Loser];

            var exchanged = EloCalc.PointsExchanged(winner.Rating, loser.Rating);
            winner.Rating += exchanged;
            loser.Rating -= exchanged;            
        }

        public static List<Game> Games()
        {
            return _games;
        }
    }
}