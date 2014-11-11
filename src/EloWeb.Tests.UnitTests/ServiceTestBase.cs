using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using EloWeb.Models;
using EloWeb.Services;
using NUnit.Framework;

namespace EloWeb.Tests.UnitTests
{
    class ServiceTestsBase
    {
        readonly DbConnection _connection = Effort.DbConnectionFactory.CreateTransient();
        protected PoolLadderContext _db;
        private DbContextTransaction _transaction;

        private Players _players;
        private Games _games;
        private Ratings _ratings;

        [TestFixtureSetUp]
        public void ServiceTestSetup()
        {
            _db = new PoolLadderContext(_connection);
            _players = new Players(_db);
            _games = new Games(_db);
            _ratings = new Ratings(_db);
            var steve = _players.Add(new Player("Steve"));
            var ian = _players.Add(new Player("Ian"));
            var andy = _players.Add(new Player("Andy"));
            var players = new List<Player>
            {
                steve,
                andy,
                ian,
                _players.Add(new Player("newPlayer")),
                _players.Add(new Player("Matt"))
            };

            foreach (var player in players)
            {
                _ratings.AddInitialRating(player);
            }

            var games = new List<Game>
            {
                _games.Add(new Game(andy, steve)),
                _games.Add(new Game(steve, andy)),
                _games.Add(new Game(ian, andy)),
                _games.Add(new Game(steve, ian)),
                _games.Add(new Game(andy, steve)),
                _games.Add(new Game(andy, ian))
            };

            foreach (Game game in games)
            {
                _ratings.UpdatePlayerRatings(game);
            }                        
        }

        [SetUp]
        public void Init()
        {
            _transaction = _db.Database.BeginTransaction();
        }

        [TearDown]
        public void Cleanup()
        {
            _transaction.Rollback();
        }
    }
}
