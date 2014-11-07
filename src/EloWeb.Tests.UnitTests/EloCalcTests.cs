using EloWeb.Models;
using NUnit.Framework;

namespace EloWeb.Tests.UnitTests
{
    class EloCalcTests
    {
        const int HighRating = 2000;
        const int AverageRating = 1000;
        const int LowRating = 500;      

        [Test]
        public void ExpectedOutcomeIsEqualForEvenlyMatchedPlayers()
        {
            var outcome = EloCalc.ExpectedOutcome(AverageRating, AverageRating);

            Assert.That(outcome, Is.EqualTo(0.5));
        }

        [Test]
        public void PointsExchangedCannotBeGreaterThanTheKFactor()
        {
            var pointsExchanged = EloCalc.PointsExchanged(LowRating, HighRating);

            Assert.That(pointsExchanged, Is.LessThanOrEqualTo(EloCalc.KFactor));
        }

        [Test]
        public void ChanceOfALowRatedPlayerBeatingAHighlyRatedPlayerIsLessThanEven()
        {                  
            var outcome = EloCalc.ExpectedOutcome(LowRating, HighRating);

            Assert.That(outcome, Is.LessThan(0.5));
        }

        [Test]
        public void ChanceOfAHighlyRatedPlayerBeatingALowRatedPlayerIsBetterThanEven()
        {
            var outcome = EloCalc.ExpectedOutcome(HighRating, LowRating);

            Assert.That(outcome, Is.GreaterThan(0.5));
        }
    }
}
