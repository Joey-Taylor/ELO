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
        public IEnumerable<Game> Games { get { return Wins.Union(Losses); } }

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
        public int MaxRating()
        {
       
            get { return _ratings.First(); }
            get
            if (Wins.IsNullOrEmpty())
            {
                return Rating;
            }
            return Wins.Max(g => g.WinnerRating);                
            
        }

        public Rating MaxRating

        public int MinRating
        public int MinRating()
        {
            get { return _ratings.Max(); }
            get
            if (Losses.IsNullOrEmpty())
            {
                return Rating;    
            }
            return Losses.Min(g => g.LoserRating);                
        }        
        public Rating MinRating
        {
            get { return _ratings.Min(); }
        }

        public IEnumerable<Game> RecentGames(int n)
        {
            return Games.OrderByDescending(g => g.Date).Take(n).ToList();
        }
       
        public int LongestWinningStreak()
        {
            // TODO
            return 5;
        }
        public int CurrentWinningStreak()
        {
            if (Losses.IsNullOrEmpty())
            {
                if (Wins.IsNullOrEmpty())
                {
                    return 0;
                }
                return Wins.Count;
            }

            var lastLoss = Losses.OrderByDescending(l => l.Date).First();
            var winCount = Wins.OrderByDescending(w => w.Date).TakeWhile(w => w.Date > lastLoss.Date).Count();
            return winCount;            
        }
        public int LongestLosingStreak()
        {
            // TODO
            return 7;
        }
        public int CurrentLosingStreak()
        {
            if (Wins.IsNullOrEmpty())
            {
                return Losses.Count;
            }

            var lastWin = Wins.OrderByDescending(l => l.Date).First();
            var lossCount = Losses.OrderByDescending(w => w.Date).TakeWhile(w => w.Date > lastWin.Date).Count();
            return lossCount;            
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

        public int WinRate()
        {
            if (Wins == null || Wins.Count == 0)
            {
                return 0;
            }

            var total = Wins.Count + Losses.Count;
            if (total == 0) return 0;
            return (int) Math.Round((decimal) Wins.Count/total*100);
            
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
            get
            {
                if (!Games.Any()) {return 0;}
                var lastGame = Games.OrderByDescending(g => g.Date).First();
                var pointsExchanged = EloCalc.PointsExchanged(lastGame.WinnerRating, lastGame.LoserRating);

                if (lastGame.Winner.ID == ID)
                {
                    return pointsExchanged;
                }
                else
                {
                    return - pointsExchanged;
                }
            }
        }        
    }
}