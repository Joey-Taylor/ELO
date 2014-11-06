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

        public void Add(Player player)
        {
            _db.Players.Add(player);
            _db.SaveChanges();
        }

        public Player Get(long id)
        {
            return _db.Players.Find(id);
        }

        public IEnumerable<Player> All()
        {
            return _db.Players.ToList();
        }

        public List<Player> Active()
        {
            return _db.Players.Where(p => p.IsActive).ToList();
        }

        public IEnumerable<string> Names()
        {
            return _db.Players.Select(p => p.Name).ToList();
        }

        public Player PlayerByName(string name)
        {
            return _db.Players
                .Include("Wins") 
                .Include("Wins.Loser")
                .Include("Losses")
                .Include("Losses.Winner")
                .Single(p => p.Name == name);
        }        
    }
}