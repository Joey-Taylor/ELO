using System.Linq;
using EloWeb.Models;
using EloWeb.Services;
using NUnit.Framework;

namespace EloWeb.Tests.UnitTests
{
    class PlayersTests : ServiceTestsBase
    {
        private Players _players;

        [TestFixtureSetUp]        
        public void TestSetup()
        {
            _players = new Players(_db);
        }

        [Test]
        public void CanPersistAPlayer()
        {
            var frank = new Player("Frank");
            _players.Add(frank);
            Assert.IsNotNull(frank.ID);
        }

        [Test]
        public void CanGetAPlayerByName()
        {            
            var steve = _players.PlayerByName("Steve");
            Assert.IsNotNull(steve.ID);
        }

        [Test]
        public void CanGetPlayerTotalGamesWon()
        {
            var andy = _players.PlayerByName("Andy");
            Assert.That(andy.Wins.Count, Is.EqualTo(3));
        }

        [Test]
        public void CanCalculateWinRate()
        {
            var andy = _players.PlayerByName("Andy");
            Assert.That(andy.WinRate, Is.EqualTo(60));
        }

        [Test]
        public void CanGetPlayerTotalGamesLost()
        {
            var andy = _players.PlayerByName("Andy");
            Assert.That(andy.Losses.Count, Is.EqualTo(2));
        }

        [Test]
        public void CanGetWinsByOpponents()
        {
            var andy = _players.PlayerByName("Andy");
            Assert.That(andy.WinsByOpponent.Count(), Is.EqualTo(2));
        }

        [Test]
        public void CanGetLossesByOpponent()
        {
            var andy = _players.PlayerByName("Andy");
            Assert.That(andy.LossesByOpponent.Count(), Is.EqualTo(2));
        }

        [Test]
        public void CanGetWinningStreak()
        {
            var andy = _players.PlayerByName("Andy");
            Assert.That(andy.CurrentWinningStreak, Is.EqualTo(2));
        }

        [Test]
        public void CanGetLosingStreak()
        {
            var ian = _players.PlayerByName("Ian");
            Assert.That(ian.CurrentLosingStreak, Is.EqualTo(2));
        }
    }
}
