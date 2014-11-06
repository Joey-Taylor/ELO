using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EloWeb.Utils;

namespace EloWeb.Models
{
    public class Player
    {
        private readonly LinkedList<Rating> _ratings = new LinkedList<Rating>();
        public const int InitialRating = 1000;

        public static Player CreateInitial(string name)
        {
            var player = new Player { Name = name };
            player.AddRating(InitialRating, DateTime.UtcNow);
            return player;
        }

        public string Name { get; set; }

        public Rating Rating
        {
            get { return _ratings.First(); }
        }

        public Rating MaxRating
        {
            get { return _ratings.Max(); }
        }

        public Rating MinRating
        {
            get { return _ratings.Min(); }
        }

        public string RecentForm
        {
            get { return WinsAndLossesString.Last(5); }
        }

        public int LongestWinningStreak
        {
            get { return Results.LengthOfLongestSequence(r => r == Result.Win); }
        }

        public int CurrentWinningStreak
        {
            get { return Results.Reverse().TakeWhile(r => r == Result.Win).Count(); }
        }

        public int LongestLosingStreak
        {
            get { return Results.LengthOfLongestSequence(r => r == Result.Loss); }
        }

        public int CurrentLosingStreak
        {
            get { return Results.Reverse().TakeWhile(r => r == Result.Loss).Count(); }
        }

        public IEnumerable<IGrouping<String, Game>> WinsByOpponent
        {
            get { return GamesWon.GroupBy(game => game.Loser); }
        }

        public IEnumerable<IGrouping<String, Game>> LossesByOpponent
        {
            get { return GamesLost.GroupBy(game => game.Winner); }
        }

        public IEnumerable<Game> GamesWon
        {
            get { return Games.WinsByPlayer(Name); }
        }

        public  IEnumerable<Game> GamesLost
        {
            get { return Games.LossesByPlayer(Name); }
        }

        public int GamesPlayed
        {
            get { return Games.ByPlayer(this).Count(); }
        }

        private IEnumerable<Result> Results
        {
            get { return Games.ByPlayer(this).Select(g => g.Winner == Name ? Result.Win : Result.Loss); }
        } 

        private string WinsAndLossesString
        {
            get { return Results.Select(r => r == Result.Win ? "W" : "L").Join(""); }
        }

        public int WinRate
        {
            get
            {
                var results = Results.ToList();

                var total = results.Count;
                if (total == 0) return 0;

                var wins = results.Count(r => r == Result.Win);
                return (int)Math.Round((decimal)wins/total*100);   
            }
        }

        [DisplayFormat(DataFormatString = "{0:+#;-#;0}")]
        public int LatestRatingChange
        {
            get
            {
                return _ratings.Count < 2 ? 0 : _ratings.First.Value.Value - _ratings.First.Next.Value.Value;
            }
        }

        public void AddRating(int rating, DateTime appliesFrom)
        {
            _ratings.AddFirst(new Rating{Value = rating, TimeFrom = appliesFrom});
        }

        public void IncreaseRating(int points, DateTime when)
        {
            AddRating(Rating.Value + points, when);
        }

        public void DecreaseRating(int points, DateTime when)
        {
            AddRating(Rating.Value - points, when);
        }
    }
}