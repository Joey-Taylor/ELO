using System;
using System.Collections.Generic;
using System.Linq;
using EloWeb.Models;

namespace EloWeb.Services
{
    public class Players
    {
        private readonly PoolLadderContext _db;

        public Players(PoolLadderContext context)
        {
            _db = context;
        }

        public Player Add(Player player)
        {
            _db.Players.Add(player);
            _db.SaveChanges();
            return player;
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

        public Player PlayerById(long id)
        {
            return _db.Players
                .Include("Wins")
                .Include("Wins.Loser")
                .Include("Losses")
                .Include("Losses.Winner")
                .Include("Ratings") 
                .SingleOrDefault(p => p.ID == id);
        }
    
        public Player PlayerByName(string name)
        {
            return _db.Players
                .Include("Wins")
                .Include("Wins.Loser")
                .Include("Losses")
                .Include("Losses.Winner")
                .Include("Ratings")
                .SingleOrDefault(p => p.Name == name);
               
        }
    }
}