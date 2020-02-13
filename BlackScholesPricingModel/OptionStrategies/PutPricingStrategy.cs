using BlackScholesPricingModel.Contracts;
using Statistics.Contracts;
using System;

namespace BlackScholesPricingModel
{
    public class PutPricingStrategy : IOptionPricingStrategy
    {
        private readonly INormalDistributionDataProvider _normalDistributionDataProvider;

        public string Name { get; }

        public PutPricingStrategy(INormalDistributionDataProvider normalDistributionDataProvider)
        {
            _normalDistributionDataProvider = normalDistributionDataProvider;
            Name = "Put";
        }

        public double ComputeOptionPrice(OptionModel option)
        {
            var minusD1NormalDistribution = _normalDistributionDataProvider.GetCumulativeNormalDistributionAtPoint(-option.D1Parameter);
            var minusD2NormalDistribution = _normalDistributionDataProvider.GetCumulativeNormalDistributionAtPoint(-option.D2Parameter);

            var callPrice = option.StrikePrice * Math.Exp(-option.RiskFreeInterestRate * option.TimeToMaturity) * minusD2NormalDistribution - option.StockPrice * minusD1NormalDistribution;

            return callPrice;
        }
    }
}
