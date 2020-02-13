using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Statistics;
using Statistics.Contracts;

namespace UnitTests.Statistics
{
    [TestClass]
    public class NormalDistributionDataProviderShould
    {
        private readonly INormalDistributionDataProvider _normalDistributionDataProvider;

        public NormalDistributionDataProviderShould()
        {
            _normalDistributionDataProvider = new NormalDistributionDataProvider();
        }

        [TestMethod]
        public void ThrowArgumentExceptionWithInputEqualToZero()
        {
            Assert.ThrowsException<ArgumentException>(() => _normalDistributionDataProvider.GetCumulativeNormalDistributionAtPoint(0));
        }

        [TestMethod]
        public void GetResultBetweenZeroAndOneWithInputBetweenZeroAndOne()
        {
            var result = _normalDistributionDataProvider.GetCumulativeNormalDistributionAtPoint(0.7);
            Assert.IsTrue(result >= 0 && result <= 1);
        }

        [TestMethod]
        public void GetResultBetweenZeroAndOneWithInputHigherThanOne()
        {
            var result = _normalDistributionDataProvider.GetCumulativeNormalDistributionAtPoint(3);
            Assert.IsTrue(result >= 0 && result <= 1);
        }

        [TestMethod]
        public void GetResultBetweenZeroAndOneWithInputLowerThanZero()
        {
            var result = _normalDistributionDataProvider.GetCumulativeNormalDistributionAtPoint(-5);
            Assert.IsTrue(result >= 0 && result <= 1);
        }
    }
}
