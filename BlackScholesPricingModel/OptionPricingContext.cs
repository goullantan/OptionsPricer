using BlackScholesPricingModel.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackScholesPricingModel
{
    public class OptionPricingContext : IOptionPricingContext
    {
        private readonly IEnumerable<IOptionPricingStrategy> _optionPricingStrategies;

        public OptionPricingContext(IEnumerable<IOptionPricingStrategy> optionPricingStrategies)
        {
            _optionPricingStrategies = optionPricingStrategies;
        }

        /// <summary>
        /// Resolve
        /// </summary>
        /// <param name="optionName"></param>
        /// <returns></returns>
        public IOptionPricingStrategy Resolve(string optionName)
        {
            var optionPricingStrategy = _optionPricingStrategies.FirstOrDefault(opt => opt.Name == optionName);
            if (optionPricingStrategy == null) throw new ArgumentException($"Option name not found : {optionName}");
            return optionPricingStrategy;
        }
    }
}
