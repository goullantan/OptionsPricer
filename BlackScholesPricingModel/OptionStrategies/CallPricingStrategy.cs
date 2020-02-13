using BlackScholesPricingModel.Contracts;
using Statistics.Contracts;
using System;

namespace BlackScholesPricingModel
{
    public class CallPricingStrategy : IOptionPricingStrategy
    {
        private readonly INormalDistributionDataProvider _normalDistributionDataProvider;

        public string Name { get; }

        public CallPricingStrategy(INormalDistributionDataProvider normalDistributionDataProvider)
        {
            _normalDistributionDataProvider = normalDistributionDataProvider;
            Name = "Call";
        }

        public double ComputeOptionPrice(OptionModel option)
        {
            var d1NormalDistribution = _normalDistributionDataProvider.GetCumulativeNormalDistributionAtPoint(option.D1Parameter);
            var d2NormalDistribution = _normalDistributionDataProvider.GetCumulativeNormalDistributionAtPoint(option.D2Parameter);

            var callPrice = option.StockPrice * d1NormalDistribution - option.StrikePrice * Math.Exp(-option.RiskFreeInterestRate * option.TimeToMaturity) * d2NormalDistribution;

            return callPrice;
        }
    }
}
