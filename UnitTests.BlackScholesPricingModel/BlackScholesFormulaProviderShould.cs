using BlackScholesPricingModel;
using BlackScholesPricingModel.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using Statistics.Contracts;

namespace UnitTests.BlackScholesPricingModel
{
    [TestClass]
    public class BlackScholesFormulaProviderShould
    {
        private readonly IOptionPricingContext _optionPricingContext;
        private readonly IBlackScholesParametersValidator _blackScholesParametersValidator;
        private readonly IOptionPricingStrategy _optionPricingStrategy;
        private readonly INormalDistributionDataProvider _normalDistributionDataProvider;
        private readonly IBlackScholesFormulaProvider _blackScholesFormulaProvider;

        public BlackScholesFormulaProviderShould()
        {
            _normalDistributionDataProvider = Substitute.For<INormalDistributionDataProvider>();
            _optionPricingStrategy = Substitute.For<IOptionPricingStrategy>();
            _optionPricingContext = Substitute.For<IOptionPricingContext>();
            _blackScholesParametersValidator = Substitute.For<IBlackScholesParametersValidator>();
            _blackScholesFormulaProvider = new BlackScholesFormulaProvider(_optionPricingContext, _blackScholesParametersValidator);
        }

        [TestMethod]
        public void ReturnZero()
        {
            var message = _blackScholesParametersValidator.CheckParameters(Arg.Any<OptionModel>()).Returns("Error").ToString();
            var price = _blackScholesFormulaProvider.PriceOption(new OptionModel(), out message);
            NUnit.Framework.Assert.That(price, Is.EqualTo(0));
        }

        [TestMethod]
        public void ReturnOptionPrice()
        {
            _optionPricingContext.Resolve(Arg.Any<string>()).ReturnsForAnyArgs(_optionPricingStrategy);  
            _optionPricingStrategy.ComputeOptionPrice(Arg.Any<OptionModel>()).Returns(10);
            var price = _blackScholesFormulaProvider.PriceOption(new OptionModel(), out var message);
            NUnit.Framework.Assert.That(price, Is.EqualTo(10));
        }
    }
}
