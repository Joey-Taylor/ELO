using System.Collections.Generic;
using System.Linq;

namespace EloWeb.Models
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

        public IEnumerable<Player> All()
        {
            return _db.Players;
        }

        public IEnumerable<Player> Active()
        {
            return _db.Players.Where(p => p.IsActive == true).ToList();
        }

        public IEnumerable<string> Names()
        {
            return _db.Players.Select(p => p.Name).ToList();
        }

        public Player PlayerByName(string name)
        {
            return _db.Players.Single(p => p.Name == name);
        }
    }
}