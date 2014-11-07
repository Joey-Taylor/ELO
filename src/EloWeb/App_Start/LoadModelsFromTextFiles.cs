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
        private static readonly Players Players = new Players(db, Ratings);

        private const string BEAT = "beat";
        private const string AT = "<at>";

        public static void Load(string path)
        {
            if (Directory.Exists(path))
            {
                try
                {
                    SavePlayers(File.ReadLines(path + "Players.txt"));
                    SaveGames(File.ReadLines(path + "Games.txt"));
                }
                catch (FileNotFoundException e) { }
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
                    db.SaveChanges();
                    Ratings.AddRating(
                        new Rating
                        {
                            PlayerId = player.ID,
                            TimeFrom = DateTime.Parse("2014-11-05T12:22:17.6334974Z"),
                            Value = Rating.InitialRating
                        }
                    );
                    addedNames.Add(name);
                }
            }
            db.SaveChanges();
        }

        private static void SaveGames(IEnumerable<string> gameStrings)
        {                       
            foreach (var gameString in gameStrings)
            {
                var game = Games.Add(Deserialize(gameString));                
                Ratings.UpdatePlayerRatings(game);
            }
        }

        private static Game Deserialize(string game)
        {
            var splitOn = new[] { BEAT, AT };
            var splitString = game.Split(splitOn, StringSplitOptions.None);
            var winner = Players.PlayerByName(splitString[0].Trim());
            var loser = Players.PlayerByName(splitString[1].Trim());          
            
            return new Game { Winner = winner, Loser = loser, Date = DateTime.Parse(splitString[2].Trim()) };
        }
    }
}