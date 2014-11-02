using System;
using System.Collections.Generic;
using System.Linq;

namespace EloWeb.Models
{
    public class GamesRepository
    {
        private readonly PoolLadderContext _db;
        public GamesRepository(PoolLadderContext dbContext)
        {
            _db = dbContext;
        }

        public void Add(Game game)
        {
            _db.Games.Add(game);
            _db.SaveChanges();
        }

        public IEnumerable<Game> All()
        {
            return _db.Games.ToList();
        }

        public IEnumerable<Game> MostRecent(int howMany)
        {
            return _db.Games.OrderByDescending(g => g.Date).Take(howMany).ToList();
        }

        public IEnumerable<Game> ByPlayer(Player player)
        {
            return _db.Games.Where(g => g.Winner == player || g.Loser == player).ToList();
        }

        public IEnumerable<Game> WinsByPlayer(string name)
        {
            return _db.Games.Where(g => g.Winner.Name == name).ToList();
        }

        public IEnumerable<Game> LossesByPlayer(string name)
        {
            return _db.Games.Where(g => g.Loser.Name == name).ToList();
        }
    }
}