using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EloWeb.Models;
using EloWeb.Services;
using Ninject.Infrastructure.Language;

namespace EloWeb
{
    public class LoadModelsFromTextFiles
    {
        private static readonly PoolLadderContext db = new PoolLadderContext();

        private const string BEAT = "beat";
        private const string AT = "at";

        public static void Load(string path)
        {
            if (Directory.Exists(path))
            {
                SavePlayers(File.ReadLines(path + "Players.txt"));
                SaveGames(File.ReadLines(path + "Games.txt"));
            }            
        }

        private static void SavePlayers(IEnumerable<string> names)
        {
            var dbNames = db.Players.Select(p => p.Name).ToList();
            var addedNames = new HashSet<string>();
            foreach (var name in names)
            {
                if (!addedNames.Contains(name) && !dbNames.Contains(name))
                {
                    var player = new Player(name);
                    db.Players.Add(player);
                    addedNames.Add(name);
                }
            }
            db.SaveChanges();
        }

        private static void SaveGames(IEnumerable<string> gameStrings)
        {
            var players = new Players(db);
            var games = new Games(db);
            var ratings = new Ratings(db);

            foreach (var gameString in gameStrings)
            {
                var game = games.Add(Deserialize(gameString, players));
                ratings.UpdateRatings(game.Winner, game.Loser);
            }
        }

        private static Game Deserialize(string game, Players players)
        {
            var splitOn = new[] { BEAT, AT };
            var splitString = game.Split(splitOn, StringSplitOptions.None);
            var winner = players.PlayerByName(splitString[0].Trim());
            var loser = players.PlayerByName(splitString[1].Trim());          
            
            return new Game { Winner = winner, Loser = loser, Date = DateTime.Parse(splitString[2].Trim()) };
        }
    }
}