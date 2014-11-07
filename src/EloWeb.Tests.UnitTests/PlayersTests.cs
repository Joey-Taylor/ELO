using System.Linq;
using EloWeb.Models;
using EloWeb.Services;
using Ninject;
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
        public void CanGetPlayerTotalGamesWon() { }

        [Test]
        public void NewPlayersHaveAnInitialRating()
        {
            var frank = new Player("Frank");
            _players.Add(frank);
            Assert.That(frank.CurrentRating, Is.EqualTo(Rating.InitialRating));
        }

        [Test]
        public void CanGetAPlayerByName()
        {
            var richard = new Player("Richard");
            _db.Players.Add(richard);
            _db.SaveChanges();

            var player = _db.Players.Any();

            Assert.True(player);
        }

        [Test]
        public void PlayerNamesMustBeUnique()
        {

        }

        [Test]
        public void CanGetOnlyActivePlayers()
        {

        }


    }
}
