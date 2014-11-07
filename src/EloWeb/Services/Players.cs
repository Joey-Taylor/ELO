using System;
using System.Collections.Generic;
using System.Linq;
using EloWeb.Models;

namespace EloWeb.Services
{
    public class Players
    {
        private readonly PoolLadderContext _db;
        private readonly Ratings _ratings;

        public Players(PoolLadderContext context, Ratings ratings)
        {
            _db = context;
            _ratings = ratings;
        }

        public void Add(Player player)
        {
            _db.Players.Add(player);
            _db.SaveChanges();
            _ratings.AddRating(
                new Rating
                {
                    PlayerId = player.ID,
                    TimeFrom = DateTime.Now,
                    Value = Rating.InitialRating
                }
            );
        }

        public Player Get(long id)
        {
            return _db.Players.Find(id);
        }

        public IEnumerable<Player> All()
        {
            return _db.Players.Include("Ratings").ToList();
        }

        public IEnumerable<Player> Active()
        {
            return _db.Players
                .Include("Ratings")
                .Include("Wins")
                .Include("Losses")
                .Where(p => p.IsActive);
        }

        public IEnumerable<string> Names()
        {
            return _db.Players.Select(p => p.Name);
        }

        public Player PlayerByName(string name)
        {
            return _db.Players
                .Include("Wins") 
                .Include("Wins.Loser")
                .Include("Losses")
                .Include("Losses.Winner")
                .Include("Ratings")
                .Single(p => p.Name == name);
        }
    }
}