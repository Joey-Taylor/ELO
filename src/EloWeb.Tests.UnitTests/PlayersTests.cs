using System;
using System.Linq;
using EloWeb.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace EloWeb.Tests.UnitTests
{
    class PlayersTests
    {
        [TestFixtureSetUp]
        public void TestSetup()
        {
            InitialiseTestPlayers();
            InitialiseTestGames();
        }

        [Test]
        public void CanParsePlayerDescriptionText()
        {   
            var player = Players.PlayerByName("Richard");

            Assert.AreEqual("Richard", player.Name);
            Assert.AreEqual(1000, player.Rating);
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
            Players.Initialise(new List<String> { "Peter", "Frank", "Richard", "Bob" });
        }

        private void InitialiseTestGames()
        {
            Games.Initialise(new List<String>
            {
                "Peter beat Frank", 
                "Frank beat Peter", 
                "Frank beat Peter",
                "Bob beat Richard",
                "Bob beat Richard",
                "Bob beat Richard",
                "Richard beat Bob",
                "Bob beat Richard",
                "Bob beat Richard",
                "Bob beat Richard",
                "Richard beat Bob",
                "Bob beat Richard",
                "Richard beat Bob",
                "Bob beat Richard",
                "Bob beat Richard",
                "Bob beat Richard",
                "Bob beat Richard",
                "Richard beat Bob",
                "Bob beat Richard"
            });
        }
    }
}
