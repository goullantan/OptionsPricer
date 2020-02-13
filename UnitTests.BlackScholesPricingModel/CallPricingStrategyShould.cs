using BlackScholesPricingModel;
using BlackScholesPricingModel.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using Statistics.Contracts;

namespace UnitTests.BlackScholesPricingModel
{
    [TestClass]
    public class CallPricingStrategyShould
    {
        private readonly IOptionPricingStrategy _callPricingStrategy;
        private readonly INormalDistributionDataProvider _normalDistributionDataProvider;

        public CallPricingStrategyShould()
        {
            _normalDistributionDataProvider = Substitute.For<INormalDistributionDataProvider>();
            _callPricingStrategy = new CallPricingStrategy(_normalDistributionDataProvider);
        }
          
        [TestMethod]
        public void ComputeAnOptionPriceGreaterThanOrEqualToZero()
        {
            var optionModel = new OptionModel
            {
                StockPrice = 50,
                StrikePrice = 55,
                TimeToMaturity = 1,
                StandardDeviation = 0.2,
                RiskFreeInterestRate = 0.09,
                D1Parameter = 0.5,
                D2Parameter = 0.2
            };
            var price = _callPricingStrategy.ComputeOptionPrice(optionModel);
            NUnit.Framework.Assert.That(price, Is.GreaterThanOrEqualTo(0));
        }
    }
}
