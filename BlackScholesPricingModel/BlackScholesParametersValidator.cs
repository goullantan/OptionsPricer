using BlackScholesPricingModel.Contracts;
using System.Collections.Generic;

namespace BlackScholesPricingModel
{
    public class BlackScholesParametersValidator : IBlackScholesParametersValidator
    {
        private readonly List<string> _strictlyPositiveProperties;

        public BlackScholesParametersValidator()
        {
            _strictlyPositiveProperties = new List<string>
            {
                "StockPrice","StrikePrice","TimeToMaturity","StandardDeviation"//InterestRate can be negative
            };
        }

        /// <summary>
        /// Check Parameters
        /// </summary>
        /// <param name="optionModel"></param>
        /// <returns></returns>
        public string CheckParameters(OptionModel optionModel)
        {
            var properties = optionModel.GetType().GetProperties();
            foreach(var property in properties)
            {
                if (_strictlyPositiveProperties.Contains(property.Name))
                {
                    var value = (double)GetPropValue(optionModel, property.Name);
                    if (value <= 0) return $"Error : {property.Name} must be > 0";
                }               
            }

            return "All parameters correctly filled";
        }

        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
