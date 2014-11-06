using System.Linq;
using EloWeb.Models;
using EloWeb.Services;
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
             players = Kernel.Get<Players>();

            // TODO
            // The order of these matters
//            InitialiseTestGames();
//            InitialiseTestPlayers();
        }

        [Test]
        public void CanParsePlayerDescriptionText()
        {   
            
        }

        [Test]
        public void CanGetPlayerTotalGamesWon() { }
        
        [Test]
        public void CanGetAPlayerByName()
        {
            var player = players.PlayerByName("Richard");

            Assert.AreEqual("Richard", player.Name);
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
