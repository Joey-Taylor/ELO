using NUnit.Framework;

namespace EloWeb.Tests.UnitTests
{
    public class GamesTests
    {        
        [Test]
        public void CanPersistAndRetrieveAGame()
        {

        }

        [Test]
        public void CanRetrieveNMostRecentGames()
        {

        }

        [Test]
        public void CanRetrieveGamesPlayedByAParticularPlayer()
        {

        [Test]
        public void CanRetrieveNMostRecentGames()
        {
            Games.Initialise(new List<String> { "A beat B", "A beat C", "B beat C" });

            Games.Initialise(new String[0]);
            Games.Add(game1);
            Games.Add(game2);
            Games.Add(game3);

            var expected = new List<Game>
            {
                game2,
                game3
            };

            Assert.AreEqual(expected, Games.MostRecent(2, Games.GamesSortOrder.MostRecentLast));
        }

        [Test]
        public void CanRetrieveGamesPlayedByAParticularPlayer()
        {
            Games.Initialise(new List<String> { "A beat B", "A beat C", "B beat C" });
            var player = new Player {Name = "B"};

            Games.Initialise(new String[0]);
            Games.Add(game1);
            Games.Add(game2);
            Games.Add(game3);

            var expected = new List<Game>
            {
                game2,
                game3
            };

            Assert.AreEqual(expected, Games.ByPlayer(player));
        }
    }
}
