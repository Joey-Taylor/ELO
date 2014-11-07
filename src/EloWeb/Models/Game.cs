using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EloWeb.Models
{
    public class Game        
    {        
        public long ID { get; set; }
        public long WinnerId { get; set; }           
        public long LoserId { get; set; }        

        public virtual Player Winner { get; set; }
        public virtual Player Loser { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }

		/// <summary>
        /// The time at which this game happened
        /// Note: This is in UTC
        /// </summary>
        public DateTime Date { get; set; }

        public Game() { }
        /// <summary>
        /// Default constructor which sets the Time as DateTime.UtcNow
        /// </summary>
        public Game(Player winner, Player loser)
        {
            this.WinnerId = winner.ID;
            this.LoserId = loser.ID;
            this.Date = DateTime.Now;   
        }            

        protected bool Equals(Game other)
        {
            return string.Equals(Winner, other.Winner) && string.Equals(Loser, other.Loser) && Date.Equals(other.Date);
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
                hashCode = (hashCode*397) ^ Date.GetHashCode();
                return hashCode;
            }
        }
    }
}