using System;
using System.Linq;
using EloWeb.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace EloWeb.Tests.UnitTests
{
    public class GamesTests
    {
        [Test]
        public void CanParseTheTextDescriptionOfAGame()
        {
            Games.Initialise(new List<String>{"A beat B"});

            var expected = new Game {Winner = "A", Loser = "B"};
            var actual = Games.MostRecent(1, Games.GamesSortOrder.MostRecentFirst).First();

            Assert.That(expected.Equals(actual));
        }

        [Test]
        public void CanGenerateATextDescriptionOfAGame()
        {
            var game = new Game { Winner = "A", Loser = "B" };            
            Assert.AreEqual("A beat B", game.ToString());
        }


        [Test]
        public void CanStoreAListOfGames()
        {
            Games.Initialise(new List<String> { "A beat B", "A beat C", "B beat C" } );

            var expected = new List<Game>
            {
                new Game {Winner = "A", Loser = "B"},
                new Game {Winner = "A", Loser = "C"},
                new Game {Winner = "B", Loser = "C"},
            };

            Assert.AreEqual(expected, Games.All);
        }

        [Test]
        public void CanReinitialiseTheListOfGames()
        {
            Games.Initialise(new List<String> { "D beat B", "B beat C", "C beat D" });

            var before = new List<Game>
            {
                new Game {Winner = "D", Loser = "B"},
                new Game {Winner = "B", Loser = "C"},
                new Game {Winner = "C", Loser = "D"},
            };
            Assert.AreEqual(before, Games.All);


            Games.Initialise(new List<String> { "A beat B", "A beat C", "B beat C" });

            var after = new List<Game>
            {
                new Game {Winner = "A", Loser = "B"},
                new Game {Winner = "A", Loser = "C"},
                new Game {Winner = "B", Loser = "C"},
            };

            Assert.AreEqual(after, Games.All);
        }


        [Test]
        public void CanAddAGameToTheList()
        {
            Games.Initialise(new List<String>());

            Games.Add(new Game { Winner = "A", Loser = "B"});
            Games.Add(new Game { Winner = "A", Loser = "C" });
            Games.Add(new Game { Winner = "B", Loser = "C" });

            var expected = new List<Game>
            {
                new Game {Winner = "A", Loser = "B"},
                new Game {Winner = "A", Loser = "C"},
                new Game {Winner = "B", Loser = "C"},
            };

            Assert.AreEqual(expected, Games.All);
        }



        [Test]
        public void CanRetrieveNMostRecentGames()
        {
            Games.Initialise(new List<String> { "A beat B", "A beat C", "B beat C" });

            var expected = new List<Game>
            {
                new Game {Winner = "A", Loser = "C"},
                new Game {Winner = "B", Loser = "C"},
            };

            Assert.AreEqual(expected, Games.MostRecent(2, Games.GamesSortOrder.MostRecentLast));
        }

        [Test]
        public void CanRetrieveGamesPlayedByAParticularPlayer()
        {
            Games.Initialise(new List<String> { "A beat B", "A beat C", "B beat C" });
            var player = new Player {Name = "B"};

            var expected = new List<Game>
            {
                new Game {Winner = "A", Loser = "B"},
                new Game {Winner = "B", Loser = "C"},
            };

            Assert.AreEqual(expected, Games.ByPlayer(player));
        }



    }
}
