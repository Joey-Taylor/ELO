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

            Assert.AreEqual(2, player.GamesWon);
        }

        [Test]
        public void CanGetPlayerTotalGamesLost()
        {
            var player = Players.PlayerByName("Frank");

            Assert.AreEqual(1, player.GamesLost);
        }

        private void InitialiseTestPlayers()
        {
            Players.Initialise(new List<String>() { "Peter", "Frank", "Richard" });
        }

        private void InitialiseTestGames()
        {
            Games.Initialise(new List<String>() { "Peter beat Frank", "Frank beat Peter", "Frank beat Peter" });
        }
    }
}
