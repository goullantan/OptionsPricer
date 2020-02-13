using BlackScholesPricingModel;
using BlackScholesPricingModel.Contracts;
using Wpf.OptionsPricer.Model.Contracts;

namespace Wpf.OptionsPricer.Model
{
    public class OptionPricingModel: IOptionPricingModel
    {
        private string _inputParametersCheckResult;
        private readonly IBlackScholesFormulaProvider _blackScholesFormulaProvider;

        public OptionPricingModel(IBlackScholesFormulaProvider blackScholesFormulaProvider)
        {
            _blackScholesFormulaProvider = blackScholesFormulaProvider;
            _inputParametersCheckResult = string.Empty;
        }

        /// <summary>
        /// Get Option Price
        /// </summary>
        /// <param name="optionName"></param>
        /// <param name="stockPrice"></param>
        /// <param name="strikePrice"></param>
        /// <param name="timeToMaturity"></param>
        /// <param name="standardDeviation"></param>
        /// <param name="riskFreeInterestRate"></param>
        /// <returns></returns>
        public double GetOptionPrice(string optionName, double stockPrice, double strikePrice, 
            double timeToMaturity, double standardDeviation, double riskFreeInterestRate)
        {
            var optionModel = BuildOptionModelFromInputParameters(optionName, stockPrice, strikePrice, timeToMaturity, standardDeviation, riskFreeInterestRate);
            var price = _blackScholesFormulaProvider.PriceOption(optionModel, out _inputParametersCheckResult);           
            return System.Math.Round(price, 4);//Round option price
        }

        /// <summary>
        /// Get Input Parameters Check Result
        /// </summary>
        /// <returns></returns>
        public string GetInputParametersCheckResult()
        {
            return _inputParametersCheckResult; 
        }

        private OptionModel BuildOptionModelFromInputParameters(string optionName, double stockPrice, double strikePrice,
            double timeToMaturity, double standardDeviation, double riskFreeInterestRate)
        {
            return new OptionModel
            {
                Name = optionName,
                StockPrice = stockPrice,
                StrikePrice = strikePrice,
                TimeToMaturity = timeToMaturity,
                StandardDeviation = standardDeviation,
                RiskFreeInterestRate = riskFreeInterestRate
            };
        }
    }
}
