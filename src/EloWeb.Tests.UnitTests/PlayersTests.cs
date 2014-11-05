using System;
using System.Linq;
using EloWeb.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace EloWeb.Tests.UnitTests
{
    class PlayersTests
    {
        private Players _players;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            _players = new Players(new PoolLadderContext());

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
            var player = _players.PlayerByName("Frank");

            Assert.AreEqual(2, player.Wins.Count());
        }

        [Test]
        public void CanGetPlayerTotalGamesLost()
        {
            var player = _players.PlayerByName("Frank");

            Assert.AreEqual(1, player.Losses.Count());
        }

        [Test]
        public void CanGetWinsByOpponents()
        {
            var player = _players.PlayerByName("Frank");

            var winsAgainstPeter = player.WinsByOpponent.First(p => p.Key == "Peter");

            Assert.AreEqual(2, winsAgainstPeter.Count());
        }

        [Test]
        public void CanGetLossesByOpponent()
        {
            var player = _players.PlayerByName("Frank");

            var lossesAgainstPeter = player.LossesByOpponent.First(p => p.Key == "Peter");

            Assert.AreEqual(1, lossesAgainstPeter.Count());
        }

//        [Test]
//        public void CanGetWinLossString()
//        {
//            var frank = _players.PlayerByName("Frank");
//            var peter = _players.PlayerByName("Peter");
//            var bob = _players.PlayerByName("Bob");
//            var richard = _players.PlayerByName("Richard");
//
//            Assert.AreEqual("LWW", frank.RecentGames);
//            Assert.AreEqual("WLL", peter.RecentGames);
//            Assert.AreEqual("WWWLW", bob.RecentGames);
//            Assert.AreEqual("LLLWL", richard.RecentGames);
//        }

        [Test]
        public void CanGetWinningStreak()
        {
            var bob = _players.PlayerByName("Bob");

            Assert.AreEqual(4, bob.LongestWinningStreak());
            Assert.AreEqual(1, bob.CurrentWinningStreak());
        }

//        [Test]
//        public void CanGetLosingStreak()
//        {
//            var richard = _players.PlayerByName("Richard");
//
//            Assert.AreEqual(4, richard.LongestLosingStreak);
//            Assert.AreEqual(1, richard.CurrentLosingStreak);
//        }

//        [Test]
//        public void CanGetWinRate()
//        {
//            var richard = _players.PlayerByName("Richard");
//
//            Assert.AreEqual(25, richard.WinRate);
//        }

        private void InitialiseTestGames()
        {
//            Games.Add(new List<String>
//            {
//                "Peter beat Frank", 
//                "Frank beat Peter", 
//                "Frank beat Peter",
//                "Bob beat Richard",
//                "Bob beat Richard",
//                "Bob beat Richard",
//                "Richard beat Bob",
//                "Bob beat Richard",
//                "Bob beat Richard",
//                "Bob beat Richard",
//                "Richard beat Bob",
//                "Bob beat Richard",
//                "Richard beat Bob",
//                "Bob beat Richard",
//                "Bob beat Richard",
//                "Bob beat Richard",
//                "Bob beat Richard",
//                "Richard beat Bob",
//                "Bob beat Richard"
//            });
        }
    }
}
