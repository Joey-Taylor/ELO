using System;
using System.Linq;
using EloWeb.Models;
using EloWeb.Services;
using NUnit.Framework;

namespace EloWeb.Tests.UnitTests
{
    class RatingsTests: ServiceTestsBase
    {
        private Players _players;
        private Ratings _ratings;
        private Games _games;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            _players = new Players(_db);
            _ratings = new Ratings(_db);
            _games = new Games(_db);
        }

        [Test]
        public void NewPlayersHaveAnInitialRating()
        {
            var newPlayer = _players.PlayerByName("newPlayer");
            Assert.That(newPlayer.CurrentRating.Value, Is.EqualTo(Rating.InitialRating));
        }

        [Test]
        public void CanManuallyAddARating()
        {            
            _ratings.AddRating(new Rating{PlayerId = 1, TimeFrom = DateTime.Now, Value = 12345});
            var player = _players.PlayerById(1);
            Assert.That(player.CurrentRating.Value, Is.EqualTo(12345));
        }
    }
}
