using System.Linq;
using EloWeb.Models;
using Ninject;
using NUnit.Framework;

namespace EloWeb.Tests.UnitTests
{
    class PlayerTests : ServiceTestsBase
    {
        [Test]
        public void NewPlayersHaveAnInitialRating()
        {
            var frank = new Player("Frank");
            
            Assert.That(frank.CurrentRating, Is.EqualTo(Player.InitialRating));
        }

        [Test]
        public void RatingPointsAreExchanged()
        {

        }

        [Test]
        public void CanGetPlayerTotalGamesWon()
        {

        }

        [Test]
        public void CanGetPlayerTotalGamesLost()
        {

        }

        [Test]
        public void CanGetWinsByOpponents()
        {

        }

        [Test]
        public void CanGetLossesByOpponent()
        {

        }

        [Test]
        public void CanGetWinningStreak()
        {

        }

        [Test]
        public void CanGetLosingStreak()
        {

        }

        [Test]
        public void CanGetWinRate()
        {

        }
    }
}
