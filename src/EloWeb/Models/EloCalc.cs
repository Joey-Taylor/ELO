using System;

namespace EloWeb.Models
{
    public class EloCalc
    {
        public const int KFactor = 50;
        private const int Volatility = 400;

        public static int PointsExchanged(int winnerRating, int loserRating)
        {
            var expected = ExpectedOutcome(winnerRating, loserRating);            
            var exchanged = (Int32)Math.Round((KFactor * (1 - expected)), MidpointRounding.AwayFromZero);
            return exchanged;
        }

        public static double ExpectedOutcome(int player, int opponent)
        {
            var difference = (double)opponent - player;
            return 1 / (1 + (Math.Pow(10, (difference / Volatility))));
        }
    }
}