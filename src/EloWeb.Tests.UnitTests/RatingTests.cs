using System;
using System.Linq;
using EloWeb.Models;
using NUnit.Framework;

namespace EloWeb.Tests.UnitTests
{
    public class RatingTests
    {
        [Test]
        public void OrderingWorksAsExpected()
        {
            var r1000 = new Rating {Value = 1000};
            var r1001 = new Rating { Value = 1001 };
            var r1002 = new Rating { Value = 1002 };

            var inputArray = new[] {r1001, r1000, r1002};
            var orderedArray = inputArray.OrderBy(r => r).ToArray();

            Assert.AreEqual(new[] {r1000, r1001, r1002}, orderedArray);
        }

        [Test]
        public void EqualityIgnoresTime()
        {
            var r1000_1 = new Rating { Value = 1000, TimeFrom = DateTime.UtcNow};
            var r1000_2 = new Rating { Value = 1000, TimeFrom = DateTime.UtcNow.AddHours(1)};
            var r1002 = new Rating { Value = 1002 };

            Assert.AreEqual(r1000_1, r1000_2);
            Assert.AreNotEqual(r1000_1, r1002);
        }

        [Test]
        public void OperatorsIgnoreTime()
        {
            var r1000_1 = new Rating { Value = 1000, TimeFrom = DateTime.UtcNow };
            var r1000_2 = new Rating { Value = 1000, TimeFrom = DateTime.UtcNow.AddHours(1) };
            var r1002 = new Rating { Value = 1002 };

            Assert.Greater(r1002, r1000_1);
            Assert.GreaterOrEqual(r1000_1, r1000_2);
            Assert.Less(r1000_1, r1002);
            Assert.LessOrEqual(r1000_2, r1000_1);
            Assert.IsTrue(r1000_1 == r1000_2);
            Assert.IsTrue(r1000_1 != r1002);
            Assert.IsFalse(r1000_1 == r1002);
            Assert.IsFalse(r1000_1 != r1000_2);
        }
    }
}
