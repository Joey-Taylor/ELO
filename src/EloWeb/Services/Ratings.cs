using System;
using EloWeb.Models;

namespace EloWeb.Services
{
    public class Ratings
    {
        public const int InitialRating = 1000;
        private PoolLadderContext db;
        public Ratings(PoolLadderContext poolLadderContext)
        {
            db = poolLadderContext;
        }

        public void AddRating(Player player, int ratingPoints, DateTime appliesFrom)
        {
            var rating = new Rating {PlayerId = player.ID, Value = ratingPoints, TimeFrom = appliesFrom};
            db.Ratings.Add(rating);
            db.SaveChanges();
        }

        public void GivePoints(int pointsExchanged, Player winner, Player loser)
        {
            AddRating(winner, winner.CurrentRating.Value + pointsExchanged, DateTime.Now);
            AddRating(winner, winner.CurrentRating.Value - pointsExchanged, DateTime.Now);
       }

        public void UpdateRatings(Player winner, Player loser)
        {
            int pointsExchanged = EloCalc.PointsExchanged(winner.CurrentRating.Value, loser.CurrentRating.Value);
            GivePoints(pointsExchanged, winner, loser);
            db.SaveChanges();
        }
    }
}
