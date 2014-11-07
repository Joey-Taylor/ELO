using System.Collections.Generic;
using System.Linq;
using EloWeb.Models;

namespace EloWeb.Services
{
    public class Games
    {
        private readonly PoolLadderContext _db;
        public Games(PoolLadderContext dbContext)
        {
            _db = dbContext;
        }

        public Game Add(Game game)
        {
            _db.Games.Add(game);
            _db.SaveChanges();
            return game;
        }

        public IEnumerable<Game> All()
        {
            return _db.Games.ToList();
        }

        public IEnumerable<Game> MostRecent(int howMany)
        {
            return _db.Games.OrderByDescending(g => g.Date).Take(howMany).ToList();
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