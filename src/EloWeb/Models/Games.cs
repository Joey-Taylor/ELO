using System.Collections.Generic;
using System.Linq;

namespace EloWeb.Models
{
    public class Games
    {
        public enum GamesSortOrder
        {
            MostRecentFirst = 1,
            MostRecentLast = 2
        }

        private static List<Game> _games = new List<Game>();

        public static void Initialise(IEnumerable<Game> games)
        {
            _games = games.ToList();
        }

        public static void Add(Game game)
        {
            _games.Add(game);
        }

        public static IEnumerable<Game> All
        {
            get { return _games; }
        }

        public static IEnumerable<Game> MostRecent(int howMany, GamesSortOrder sortOrder)
        {
            var games = All.Reverse().Take(howMany);

            return sortOrder == GamesSortOrder.MostRecentLast ? games.Reverse() : games;
        }

        public static IEnumerable<Game> ByPlayer(Player player)
        {
            return _games.Where(game => game.Winner == player.Name || game.Loser == player.Name);
        }

        public static IEnumerable<Game> WinsByPlayer(string name)
        {
            return _games.Where(game => game.Winner == name);
        }

        public static IEnumerable<Game> LossesByPlayer(string name)
        {
            return _games.Where(game => game.Loser == name);
        }
    }
}