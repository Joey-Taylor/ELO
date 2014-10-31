using System.Collections.Generic;
using System.IO;
using System.Linq;
using EloWeb.Models;

namespace EloWeb.Persist
{
    public class GamesData
    {
        private static string _path;
        
        public static IEnumerable<string> Load(string path)
        {
            try
            {
                _path = path;
                return File.ReadLines(_path);
            }
            catch (FileNotFoundException)
            {
                return new List<string>().AsEnumerable();
            }
        }

        public static void PersistGame(Game game)
        {
            File.AppendAllText(_path, game.ToString() + "\n");
        }
    }
}