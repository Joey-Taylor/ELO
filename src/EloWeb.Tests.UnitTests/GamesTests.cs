using System.Linq;
using EloWeb.Models;
using EloWeb.Services;
using NUnit.Framework;

namespace EloWeb.Tests.UnitTests
{
    class GamesTests : ServiceTestsBase
    {
        private Games _games;
        private Players _players;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            _players = new Players(_db);
            _games = new Games(_db);
        }

        [Test]
        public void CanPersistAGame()
        {
            var andy = _players.PlayerByName("Andy");
            var matt = _players.PlayerByName("Matt");
            var newGame = _games.Add(new Game(andy, matt));
            Assert.NotNull(newGame.ID);
        }

        [Test]
        public void CanRetrieveNMostRecentGames()
        {
            var games = _games.MostRecent(4);
            Assert.That(games.Count(), Is.EqualTo(4));
        }
    }
}
