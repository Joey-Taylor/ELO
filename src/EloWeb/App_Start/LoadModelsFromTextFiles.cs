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
        private static readonly Games Games = new Games(db);
        private static readonly Ratings Ratings = new Ratings(db);
        private static readonly Players Players = new Players(db);

        private const string Beat = "beat";
        private const string At = "<at>";
        private const string CreatedAt = "<Created At>";

        public static void Load(string path)
        {
            if (Directory.Exists(path) && !db.Database.Exists())
            {
                try
                {
                    SavePlayers(File.ReadLines(path + "Players.txt"));
                    SaveGames(File.ReadLines(path + "Games.txt"));
                }
                catch (FileNotFoundException e) { }
            }            
        }

        private static void SavePlayers(IEnumerable<string> playerStrings)
        {
            var dbNames = db.Players.Select(p => p.Name).ToList();
            var addedNames = new HashSet<string>();
            foreach (var playerString in playerStrings)
            {
                if (!addedNames.Contains(playerString) && !dbNames.Contains(playerString))
                {
                    var player = DeserializePlayer(playerString);
                    db.Players.Add(player);
                    db.SaveChanges();
                    Ratings.AddRating(
                        new Rating
                        {
                            PlayerId = player.ID,
                            TimeFrom = player.CreatedTime,
                            Value = Rating.InitialRating
                        }
                    );
                    addedNames.Add(playerString);
                }
            }
            db.SaveChanges();
        }

        public static Player DeserializePlayer(string playerString)
        {
            var splitOn = new[] { CreatedAt };
            var splitString = playerString.Split(splitOn, StringSplitOptions.None);
            var name = splitString[0].Trim();
            var createdTime = DateTime.Parse(splitString[1].Trim());

            return new Player
            {
                Name = name,
                CreatedTime = createdTime,
                IsActive = true                
            };
        }

        private static void SaveGames(IEnumerable<string> gameStrings)
        {                       
            foreach (var gameString in gameStrings)
            {
                var game = Games.Add(DeserializeGame(gameString));                
                Ratings.UpdatePlayerRatings(game);
            }
        }

        private static Game DeserializeGame(string game)
        {
            var splitOn = new[] { Beat, At };
            var splitString = game.Split(splitOn, StringSplitOptions.None);
            var winner = Players.PlayerByName(splitString[0].Trim());
            var loser = Players.PlayerByName(splitString[1].Trim());          
            
            return new Game { Winner = winner, Loser = loser, Date = DateTime.Parse(splitString[2].Trim()) };
        }
    }
}