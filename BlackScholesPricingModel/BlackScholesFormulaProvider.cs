using BlackScholesPricingModel.Contracts;
using log4net;
using System;

namespace BlackScholesPricingModel
{
    public class BlackScholesFormulaProvider : IBlackScholesFormulaProvider
    {
        private static readonly ILog Logger = LogManager.GetLogger(nameof(BlackScholesFormulaProvider));

        private readonly IOptionPricingContext _optionPricingContext;
        private readonly IBlackScholesParametersValidator _blackScholesParametersValidator;

        public BlackScholesFormulaProvider(IOptionPricingContext optionPricingContext, IBlackScholesParametersValidator blackScholesParametersValidator)
        {
            _optionPricingContext = optionPricingContext;
            _blackScholesParametersValidator = blackScholesParametersValidator;
        }
        
        /// <summary>
        /// Price option
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public double PriceOption(OptionModel option, out string inputParametersCheckResult)
        {
            try
            {
                inputParametersCheckResult = _blackScholesParametersValidator.CheckParameters(option);
                if (inputParametersCheckResult.Contains("Error")) return 0;

                var optionPricingStrategy = _optionPricingContext.Resolve(option.Name);
                Logger.Info($"Resolving option pricing strategy : {optionPricingStrategy.Name}");
                option.D1Parameter = ComputeD1Parameter(option);
                Logger.Info($"Parameter d1 : {option.D1Parameter}");
                option.D2Parameter = ComputeD2Parameter(option);
                Logger.Info($"Parameter d2 : {option.D2Parameter}");
                return optionPricingStrategy.ComputeOptionPrice(option);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        private double ComputeD1Parameter(OptionModel option)
        {
            var d1Numerator = Math.Log(option.StockPrice / option.StrikePrice) 
                + (option.RiskFreeInterestRate + Math.Pow(option.StandardDeviation, 2) / 2) * option.TimeToMaturity;
            var d1Denominator = option.StandardDeviation * Math.Sqrt(option.TimeToMaturity);

            return d1Numerator / d1Denominator;
        }

        private double ComputeD2Parameter(OptionModel option)
        {
            return option.D1Parameter - option.StandardDeviation * Math.Sqrt(option.TimeToMaturity);
        }
    }
}
