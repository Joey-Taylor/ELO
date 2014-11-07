using System.Collections.Generic;
using System.Linq;
using EloWeb.Utils;
using WebGrease.Css.Extensions;

namespace EloWeb.Models
{
    public class Players
    {
        private static Dictionary<string, Player> _players = new Dictionary<string, Player>();

        public static void Initialise(IEnumerable<Player> players)
        {
           _players = players.ToDictionary(p => p.Name);
           Games.All.ForEach(UpdateRatings);
        }

        public static void Add(Player player)
        {
            _players.Add(player.Name, player);
        }

        public static void UpdateRatings(Game game)
        {
            var winner = PlayerByName(game.Winner);
            var loser = PlayerByName(game.Loser);

            var pointsExchanged = EloCalc.PointsExchanged(winner.Rating, loser.Rating);

            winner.IncreaseRating(pointsExchanged, game.Time);
            loser.DecreaseRating(pointsExchanged, game.Time);
        }

        public static IEnumerable<Player> All()
        {
            return _players.Values;
        }

        public static IEnumerable<Player> Active()
        {
            return _players.Values.Where(p => !RetiredPlayers.IsRetired(p.Name));
        }

        public static IEnumerable<string> Names()
        {
            return _players.Keys;
        }

        public static Player PlayerByName(string name)
        {
            return _players.GetOrDefault(name);
        }
    }
}