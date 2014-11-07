using System.Data.Common;
using System.Data.Entity;
using EloWeb.Models;
using NUnit.Framework;

namespace EloWeb.Tests.UnitTests
{
    class ServiceTestsBase
    {
        readonly DbConnection _connection = Effort.DbConnectionFactory.CreateTransient();
        protected PoolLadderContext _db;
        private DbContextTransaction _transaction;

        [TestFixtureSetUp]
        public void Setup()
        {            
            _db = new PoolLadderContext(_connection);
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
