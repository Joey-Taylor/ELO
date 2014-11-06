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
            
            Assert.That(frank.Rating, Is.EqualTo(Player.InitialRating));
        }

        [Test]
        public void RatingPointsAreExchanged()
        {
            var frank = new Player("Frank");
            var frankOriginalRating = frank.Rating;
            var dave = new Player("Dave");
            var daveOriginalRating = dave.Rating;
            var pointsExchanged = 10;

            frank.GivePoints(10, dave);

            Assert.That(frank.Rating, Is.EqualTo(frankOriginalRating - pointsExchanged));
            Assert.That(dave.Rating, Is.EqualTo(daveOriginalRating + pointsExchanged));
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
