using System;
using System.Linq;
using EloWeb.Models;
using NUnit.Framework;

namespace EloWeb.Tests.UnitTests
{
    class PlayersTests
    {
        [TestFixtureSetUp]
        public void TestSetup()
        {
            // The order of these matters
            InitialiseTestGames();
            InitialiseTestPlayers();
        }

        [Test]
        public void CanPersistAndRetrievePlayer()
        {
            var player = new Player("Test", DateTime.UtcNow);
            var playerString = player.Serialize();
            var deserializedPlayer = Player.Deserialize(playerString);

            Assert.AreEqual(player, deserializedPlayer);
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
            var player = Players.PlayerByName("Frank");

            Assert.AreEqual(2, player.GamesWon.Count());
        }

        [Test]
        public void CanGetPlayerTotalGamesLost()
        {
            var player = Players.PlayerByName("Frank");

            Assert.AreEqual(1, player.GamesLost.Count());
        }

        [Test]
        public void CanGetWinsByOpponents()
        {
            var player = Players.PlayerByName("Frank");

            var winsAgainstPeter = player.WinsByOpponent.First(p => p.Key == "Peter");

            Assert.AreEqual(2, winsAgainstPeter.Count());
        }

        [Test]
        public void CanGetLossesByOpponent()
        {
            var player = Players.PlayerByName("Frank");

            var lossesAgainstPeter = player.LossesByOpponent.First(p => p.Key == "Peter");

            Assert.AreEqual(1, lossesAgainstPeter.Count());
        }

        [Test]
        public void CanGetWinLossString()
        {
            var frank = Players.PlayerByName("Frank");
            var peter = Players.PlayerByName("Peter");
            var bob = Players.PlayerByName("Bob");
            var richard = Players.PlayerByName("Richard");

            Assert.AreEqual("LWW", frank.RecentForm);
            Assert.AreEqual("WLL", peter.RecentForm);
            Assert.AreEqual("WWWLW", bob.RecentForm);
            Assert.AreEqual("LLLWL", richard.RecentForm);
        }

        [Test]
        public void CanGetWinningStreak()
        {
            var bob = Players.PlayerByName("Bob");

            Assert.AreEqual(4, bob.LongestWinningStreak);
            Assert.AreEqual(1, bob.CurrentWinningStreak);
        }

        [Test]
        public void CanGetLosingStreak()
        {
            var richard = Players.PlayerByName("Richard");

            Assert.AreEqual(4, richard.LongestLosingStreak);
            Assert.AreEqual(1, richard.CurrentLosingStreak);
        }

        [Test]
        public void CanGetWinRate()
        {
            var richard = Players.PlayerByName("Richard");

            Assert.AreEqual(25, richard.WinRate);
        }

        private void InitialiseTestPlayers()
        {
            var peter = new Player("Peter", DateTime.MinValue);
            var frank = new Player("Frank", DateTime.MinValue);
            var richard = new Player("Richard", DateTime.MinValue);
            var bob = new Player("Bob", DateTime.MinValue);

            Players.Initialise(new[] {peter, frank, richard, bob});
        }

        private void InitialiseTestGames()
        {
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

            Games.Initialise(games);
        }
    }
}
