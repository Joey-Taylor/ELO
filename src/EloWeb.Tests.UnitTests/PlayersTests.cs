using System;
using System.Linq;
using EloWeb.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace EloWeb.Tests.UnitTests
{
    class PlayersTests
    {
        private Players players;

        [TestFixtureSetUp]
        public void TestSetup()
        {

            // The order of these matters
            InitialiseTestGames();
            InitialiseTestPlayers();
            players = new Players();
            
        }

        [Test]
        public void CanParsePlayerDescriptionText()
        {   
            var player = Players.PlayerByName("Richard");

            Assert.AreEqual("Richard", player.Name);
        }

        [Test]
        public void CanGetPlayerTotalGamesWon()
        {
            var player = players.PlayerByName("Frank");

            Assert.AreEqual(2, player.GamesWon.Count());
        }

        [Test]
        public void CanGetPlayerTotalGamesLost()
        {
            var player = players.PlayerByName("Frank");

            Assert.AreEqual(1, player.GamesLost.Count());
        }

        [Test]
        public void CanGetWinsByOpponents()
        {
            var player = players.PlayerByName("Frank");

            var winsAgainstPeter = player.WinsByOpponent.First(p => p.Key == "Peter");

            Assert.AreEqual(2, winsAgainstPeter.Count());
        }

        [Test]
        public void CanGetLossesByOpponent()
        {
            var player = players.PlayerByName("Frank");

            var lossesAgainstPeter = player.LossesByOpponent.First(p => p.Key == "Peter");

            Assert.AreEqual(1, lossesAgainstPeter.Count());
        }

        [Test]
        public void CanGetWinLossString()
        {
            var frank = players.PlayerByName("Frank");
            var peter = players.PlayerByName("Peter");
            var bob = players.PlayerByName("Bob");
            var richard = players.PlayerByName("Richard");

            Assert.AreEqual("LWW", frank.RecentForm);
            Assert.AreEqual("WLL", peter.RecentForm);
            Assert.AreEqual("WWWLW", bob.RecentForm);
            Assert.AreEqual("LLLWL", richard.RecentForm);
        }

        [Test]
        public void CanGetWinningStreak()
        {
            var bob = players.PlayerByName("Bob");

            Assert.AreEqual(4, bob.LongestWinningStreak);
            Assert.AreEqual(1, bob.CurrentWinningStreak);
        }

        [Test]
        public void CanGetLosingStreak()
        {
            var richard = players.PlayerByName("Richard");

            Assert.AreEqual(4, richard.LongestLosingStreak);
            Assert.AreEqual(1, richard.CurrentLosingStreak);
        }

        [Test]
        public void CanGetWinRate()
        {
            var richard = players.PlayerByName("Richard");

            Assert.AreEqual(25, richard.WinRate);
        }

        private void InitialiseTestGames()
        {
            Games.Initialise(new String[0]);
            var games = new Game[]
            {
                new Game {Winner = "Peter", Loser = "Frank"},
                new Game {Winner = "Frank", Loser = "Peter"},
                new Game {Winner = "Frank", Loser = "Peter"},
                new Game {Winner = "Bob", Loser = "Richard"},
                new Game {Winner = "Bob", Loser = "Richard"},
                new Game {Winner = "Bob", Loser = "Richard"},
                new Game {Winner = "Richard", Loser = "Bob"},
                new Game {Winner = "Bob", Loser = "Richard"},
                new Game {Winner = "Bob", Loser = "Richard"},
                new Game {Winner = "Bob", Loser = "Richard"},
                new Game {Winner = "Richard", Loser = "Bob"},
                new Game {Winner = "Bob", Loser = "Richard"},
                new Game {Winner = "Richard", Loser = "Bob"},
                new Game {Winner = "Bob", Loser = "Richard"},
                new Game {Winner = "Bob", Loser = "Richard"},
                new Game {Winner = "Bob", Loser = "Richard"},
                new Game {Winner = "Bob", Loser = "Richard"},
                new Game {Winner = "Richard", Loser = "Bob"},
                new Game {Winner = "Bob", Loser = "Richard"}
            };

            foreach (var game in games)
            {
                Games.Add(game);
            }
        }
    }
}
