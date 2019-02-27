using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EloWeb.Persist;
using WebGrease.Css.Extensions;

namespace EloWeb.Models
{
    public class RetiredPlayers
    {
        private static List<String> _retiredPlayers = new List<String>();

        public static void Initialise(IEnumerable<string> names)
        {
            _retiredPlayers = names.ToList();
        }

        public static bool IsRetired(string name)
        {
            return _retiredPlayers.Contains(name);
        }

        public static void RetirePlayer(string name)
        {
            _retiredPlayers.Add(name);
            RetiredPlayersData.PersistPlayer(name);
        }

        public static void UnRetirePlayer(string name)
        {
            _retiredPlayers.Remove(name);
            RetiredPlayersData.PersistAll(_retiredPlayers);
        }
    }
}