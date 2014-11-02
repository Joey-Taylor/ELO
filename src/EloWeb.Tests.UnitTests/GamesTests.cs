using System;
using EloWeb.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace EloWeb.Tests.UnitTests
{
    public class GamesTests
    {        
        [Test]
        public void CanPersistAndRetrieveAGame()
        {

        }

        [Test]
        public void CanAddAGameToTheList()
        {
            var game = new Game { Winner = new Player("A"), Loser = new Player("B") };            
            Assert.AreEqual("A beat B", game.ToString());
        }

        [Test]
        public void CanAddAGameToTheList()
        {
            var gameReposity = new GamesRepository(new PoolLadderContext());
            var playerA = new Player("A");
            var playerB = new Player("B");
            var game = new Game {Winner = playerA, Loser = playerB};
            gameReposity.Add(game);            

            var expected = new List<Game>
            {
                game
            };

            Assert.AreEqual(expected, GamesRepository.All);
        }

        [Test]
        public void CanRetrieveNMostRecentGames()
        {
            GamesRepository.Initialise(new List<String> { "A beat B", "A beat C", "B beat C" });

            Games.Initialise(new String[0]);
            Games.Add(game1);
            Games.Add(game2);
            Games.Add(game3);

            var expected = new List<Game>
            {
                game2,
                game3
            };

            Assert.AreEqual(expected, GamesRepository.MostRecent(2, GamesRepository.GamesSortOrder.MostRecentLast));
        }

        [Test]
        public void CanRetrieveGamesPlayedByAParticularPlayer()
        {
            GamesRepository.Initialise(new List<String> { "A beat B", "A beat C", "B beat C" });
            var player = new Player {Name = "B"};

            Games.Initialise(new String[0]);
            Games.Add(game1);
            Games.Add(game2);
            Games.Add(game3);

            var expected = new List<Game>
            {
                game2,
                game3
            };

            Assert.AreEqual(expected, GamesRepository.ByPlayer(player));
        }
    }
}
