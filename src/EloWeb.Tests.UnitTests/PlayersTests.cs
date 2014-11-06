using System.Linq;
using EloWeb.Models;
using Ninject;
using NUnit.Framework;

namespace EloWeb.Tests.UnitTests
{
    class PlayersTests : ServiceTestsBase
    {
        private Players players;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            _players = new Players(new PoolLadderContext());

            // The order of these matters
            InitialiseTestGames();
            InitialiseTestPlayers();
            players = new Players();
            
        [SetUp]
        public void TestSetup() {                    
            players = Kernel.Get<Players>();
        }

        [Test]
        public void CanParsePlayerDescriptionText()
        {   
            var player = Players.PlayerByName("Richard");

            Assert.AreEqual("Richard", player.Name);
        }

        [Test]
        public void CanGetPlayerTotalGamesWon()
        public void CanGetAPlayerByName()
        {
            
        }

        [Test]
        public void PlayerNamesMustBeUnique()
        {

        }

        [Test]
        public void CanGetOnlyActivePlayers()
        {

        }

        [Test]
        public void RatingChangesArePersisted()
        {
            
        }
    }
}
