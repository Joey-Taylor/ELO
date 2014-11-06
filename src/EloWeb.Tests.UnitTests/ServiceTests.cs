using System.Data.Entity;
using EloWeb.Models;
using Ninject;
using NUnit.Framework;

namespace EloWeb.Tests.UnitTests
{
    [TestFixture]
    class ServiceTestsBase
    {
        protected IKernel Kernel;
        private PoolLadderContext db;
        private DbContextTransaction transaction;

        [TestFixtureSetUp]
        public void Setup()
        {
            Kernel = new StandardKernel();
            NinjectWebCommon.RegisterServices(Kernel);
            // TODO in memory database?
            db = Kernel.Get<PoolLadderContext>();
        }

        [SetUp]
        public void Init()
        {
            transaction = db.Database.BeginTransaction();
        }

        [TearDown]
        public void Cleanup()
        {
            transaction.Rollback();
        }
    }
}
