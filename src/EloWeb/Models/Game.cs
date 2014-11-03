using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EloWeb.Models
{
    public class Game
    {
        private const string BEAT = "beat";
        private const string AT = "at";


        public long GameId { get; set; }
        public virtual Player Winner { get; set; }
        public long ID { get; set; }
        public long WinnerId { get; set; }           
        public int WinnerRating { get; set; }
        public long LoserId { get; set; }        
        public int LoserRating { get; set; }
		/// <summary>
        /// The time at which this game happened
        /// Note: This is in UTC
        /// </summary>
        public DateTime Date { get; set; }


        /// <summary>
        /// Default constructor which sets the Time as DateTime.UtcNow
        /// </summary>
        public Game()

        [ForeignKey("WinnerId")] 
        public virtual Player Winner { get; set; }
        [ForeignKey("LoserId")]
        public virtual Player Loser { get; set; }        

        public Game() { }

        public Game(Player winner, Player loser)
        {
            this.WinnerId = winner.ID;
            this.WinnerRating = winner.Rating;
            this.LoserId = loser.ID;
            this.LoserRating = loser.Rating;
            this.Date = DateTime.Now;   
        }
        
        public override string ToString()
        {
            Date = DateTime.UtcNow;
        }
            return String.Format("{0} {1} {2}", WinnerId, "beat" ,LoserId);
        }       

        public static Game Deserialize(string game)
        {
            var splitOn = new[] { BEAT, AT };
            var splitString = game.Split(splitOn, StringSplitOptions.None);
            return new Game { Winner = splitString[0].Trim(), Loser = splitString[1].Trim(), Time = DateTime.Parse(splitString[2].Trim()) };
        }

        public string Serialize()
        {
            return String.Format("{0} {1} {2} {3} {4:O}", Winner, BEAT ,Loser, AT, Time);
        }

        protected bool Equals(Game other)
        {
            return string.Equals(Winner, other.Winner) && string.Equals(Loser, other.Loser) && Time.Equals(other.Time);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Game) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Winner != null ? Winner.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Loser != null ? Loser.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Time.GetHashCode();
                return hashCode;
            }
        }
    }
}