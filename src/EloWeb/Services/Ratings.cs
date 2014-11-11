using System;
using EloWeb.Models;

namespace EloWeb.Services
{
    public class Ratings
    {        
        private PoolLadderContext db;
        

        public Ratings(PoolLadderContext poolLadderContext)
        {
            db = poolLadderContext;
        }

        public void AddRating(Rating rating)
        {            
            db.Ratings.Add(rating);
            db.SaveChanges();
        }

        public void UpdatePlayerRatings(Game game)
        {
            var winner = game.Winner;
            var loser = game.Loser;

            int pointsExchanged = EloCalc.PointsExchanged(winner.CurrentRating.Value, loser.CurrentRating.Value);
            winner.Ratings.Add(
                new Rating
                {
                    PlayerId = winner.ID, 
                    Value = winner.CurrentRating.Value + pointsExchanged, 
                    TimeFrom = game.Date, 
                    GameId = game.ID
                });
            loser.Ratings.Add(
                new Rating
                {
                    PlayerId = loser.ID, 
                    Value = loser.CurrentRating.Value - pointsExchanged, 
                    TimeFrom = game.Date, 
                    GameId = game.ID
                });

            db.SaveChanges();
        }

        public Rating AddInitialRating(Player player)
        {
            var rating = new Rating
            {
                PlayerId = player.ID,
                Value = Rating.InitialRating,
                TimeFrom = player.CreatedTime
            };
            db.Ratings.Add(rating);
            db.SaveChanges();
            return rating;
        }
    }
}
