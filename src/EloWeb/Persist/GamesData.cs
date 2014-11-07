using System.Collections.Generic;
using System.IO;
using System.Linq;
using EloWeb.Models;

namespace EloWeb.Persist
{
    public class GamesData
    {
        private static string _path;
        
        public static IEnumerable<Game> Load(string path)
        {
            try
            {
                _path = path;
                return File.ReadLines(_path).Select(Game.Deserialize);
            }
            catch (FileNotFoundException)
            {
                return new Game[0];
            }
        }

        public static void PersistGame(Game game)
        {
            File.AppendAllText(_path, game.Serialize() + "\n");
        }
    }
}