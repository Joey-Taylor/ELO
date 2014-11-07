using System.Collections.Generic;
using System.IO;
using System.Linq;
using EloWeb.Models;

namespace EloWeb.Persist
{
    public class PlayersData
    {
        private static string _path;

        public static IEnumerable<Player> Load(string path)
        {
            try
            {
                _path = path;
                return File.ReadLines(_path).Select(Player.Deserialize);
            }
            catch (FileNotFoundException)
            {
                return new Player[0];
            }
        }

        public static void PersistPlayer(Player player)
        {
            File.AppendAllText(_path, player.Serialize() + "\n");
        }
    }
}