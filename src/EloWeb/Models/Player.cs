using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Ninject.Infrastructure.Language;

namespace EloWeb.Models
{
    public class Player
    {
        private readonly LinkedList<Rating> _ratings = new LinkedList<Rating>();
        public const int InitialRating = 1000;

        public long ID { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Game> Wins { get; set; }
        public virtual ICollection<Game> Losses { get; set; } 

        public Player() { }
        public Player(string name)
        {
            this.Name = name;
            this.Rating = InitialRating;
            this.IsActive = true;
            this.AddRating(InitialRating, DateTime.UtcNow);
            return player;
        }

        public void GivePoints(int pointsExchanged, Player player)
        {
            Rating -= pointsExchanged;
            player.Rating += pointsExchanged;
        }
        public string Name { get; set; }

        public Rating Rating
        {
       
            get { return _ratings.First(); }
            get
            {
                if (Wins != null && Wins.Any())
                {
                    return Wins.Max(g => g.WinnerRating);
                }
                return 0;
            }
        }

        public Rating MaxRating

        public int MinRating
        {
            get { return _ratings.Max(); }
            get
            {
                if (Losses != null && Losses.Any())
                {
                    return Losses.Min(g => g.LoserRating);
                }
                return InitialRating;
            }
        }
        public Rating MinRating
        {
            get { return _ratings.Min(); }
        }

        public string RecentForm
        {
            // TODO
            get { return Wins.Union(Losses).OrderByDescending(g => g.Date).Take(5).ToList().ToString(); }
        }
        public int LongestWinningStreak
        {
            // TODO
            get { return 5; }
        }
        public int CurrentWinningStreak
        {
            // TODO
            get { return 6; }
        }
        public int LongestLosingStreak
        {
            // TODO
            get { return 7; }
        }
        public int CurrentLosingStreak
        {
            // TODO
            get { return 4; }
        }

        public IEnumerable<IGrouping<String, Game>> WinsByOpponent
        {
            get { return Wins.GroupBy(game => game.Loser.Name); }            
        }

        public IEnumerable<IGrouping<String, Game>> LossesByOpponent
        {
            get { return Losses.GroupBy(game => game.Winner.Name); }
        }
        public IEnumerable<Game> GamesWon
        {
            // TODO GamesRepository.WinsByPlayer(Name); }
            get { return new List<Game>().ToEnumerable(); }
        }
        public  IEnumerable<Game> GamesLost
        {
            // TODO return GamesRepository.LossesByPlayer(Name);           
            get { return new List<Game>().ToEnumerable(); }
        }
        public int GamesPlayed
        {
            get { return Games.ByPlayer(this).Count(); }
        }

        private IEnumerable<Result> Results
        {
            // TODO return Games.ByPlayer(this).Select(g => g.Winner == Name ? Result.Win : Result.Loss);       
            get { return new List<Result>().ToEnumerable(); }     
        } 

        public int WinRate
        {
            get { return 50; }
            // TODO
//            var total = Wins.Count + Losses.Count;
//            if (total == 0) return 0;
//            return (int) Math.Round((decimal) Wins.Count/total*100);
            
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