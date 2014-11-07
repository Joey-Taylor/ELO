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
        private const string CreatedAt = "<Created At>";

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
            get
            {
                return Ratings.OrderByDescending(r => r.TimeFrom).First();                                    
            } 
        }

        public IEnumerable<Rating> Ratings
        {
            get { return _ratings; }
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

        public int GamesPlayed
        {
            get { return Wins.Count + Losses.Count; }
        }
        public int WinRate
        {
            get
            {
                if (Wins == null || Wins.Count == 0)
                {
                    return 0;
                }

                if (GamesPlayed == 0) return 0;
                return (int) Math.Round((decimal) Wins.Count/GamesPlayed*100);
            }

        }

        [DisplayFormat(DataFormatString = "{0:+#;-#;0}")]
        public int LatestRatingChange
        {
            get
            {
                if (Ratings.Count < 2) 
                    return 0; 
                var sortedRatings = Ratings.OrderByDescending(r => r.TimeFrom);
                return sortedRatings.First().Value - sortedRatings.Skip(1).Take(1).First().Value;
            }
        }          

        protected bool Equals(Player other)
        {
            return CreatedTime.Equals(other.CreatedTime) && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Player) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (CreatedTime.GetHashCode()*397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        public string Serialize()
        {
            return string.Format("{0} {1} {2:O}", Name, CreatedAt, CreatedTime);
        }

        public static Player Deserialize(string playerString)
        {
            var splitOn = new[] { CreatedAt };
            var splitString = playerString.Split(splitOn, StringSplitOptions.None);
            var name = splitString[0].Trim();
            var createdTime = DateTime.Parse(splitString[1].Trim());

            return new Player(name, createdTime);
        }
    }
}