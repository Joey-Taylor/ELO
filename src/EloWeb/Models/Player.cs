using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EloWeb.Utils;

namespace EloWeb.Models
{
    public class Player
    {        
        public long ID { get; set; }
        public string Name { get; set; }        
        public bool IsActive { get; set; }

        public virtual ICollection<Game> Wins { get; set; }
        public virtual ICollection<Game> Losses { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public IEnumerable<Game> Games { get { return Wins.Union(Losses).OrderByDescending(g => g.Date); } }

        public Player() { }
        public Player(string name)
        {
            Name = name;            
            IsActive = true;                  
        }

        public Rating CurrentRating
        {
            get { return Ratings.First(); } 
        }

        public Rating MaxRating
        {
            get { return Ratings.Max(); }
            
        }
    
        public Rating MinRating
        {
            get { return Ratings.Min(); }
        }

        public IEnumerable<Game> RecentGames(int n)
        {
            return Games.Take(n);
        }
       
        public int LongestWinningStreak
        {
            get { return Games.LengthOfLongestSequence(g => g.WinnerId == ID); }
        }
        public int CurrentWinningStreak
        {
            get
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
        }
        public int LongestLosingStreak
        {
            get { return Games.LengthOfLongestSequence(g => g.LoserId == ID); }
        }
        public int CurrentLosingStreak
        {
            get
            {
                if (Wins.IsNullOrEmpty())
                {
                    return Losses.Count;
                }

                var lastWin = Wins.OrderByDescending(l => l.Date).First();
                var lossCount = Losses.OrderByDescending(w => w.Date).TakeWhile(w => w.Date > lastWin.Date).Count();
                return lossCount;
            }
        }

        public IEnumerable<IGrouping<String, Game>> WinsByOpponent
        {
            get { return Wins.GroupBy(game => game.Loser.Name); }            
        }

        public IEnumerable<IGrouping<String, Game>> LossesByOpponent
        {
            get { return Losses.GroupBy(game => game.Winner.Name); }
        }

        public int WinRate
        {
            get
            {
                if (Wins == null || Wins.Count == 0)
                {
                    return 0;
                }

                var total = Wins.Count + Losses.Count;
                if (total == 0) return 0;
                return (int) Math.Round((decimal) Wins.Count/total*100);
            }

        }

        [DisplayFormat(DataFormatString = "{0:+#;-#;0}")]
        public int LatestRatingChange
        {
            get
            {
                if (Ratings.Count < 2) 
                    return 0; 
                return Ratings.First().Value - Ratings.Skip(1).Take(1).First().Value;
            }
        }          
    }
}