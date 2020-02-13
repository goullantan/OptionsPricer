using BlackScholesPricingModel;
using BlackScholesPricingModel.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using Wpf.OptionsPricer.Model;

namespace UnitTests.OptionsPricer
{
    [TestClass]
    public class OptionPricingModelShould
    {
        private readonly OptionPricingModel _optionPricingModel;
        private readonly IBlackScholesFormulaProvider _blackScholesFormulaProvider;

        public OptionPricingModelShould()
        {
            _blackScholesFormulaProvider = Substitute.For<IBlackScholesFormulaProvider>();
            _optionPricingModel = new OptionPricingModel(_blackScholesFormulaProvider);
        }

        [TestMethod]
        public void ReturnEmptyMessageWhenThePricerIsNotCalled()
        {
            NUnit.Framework.Assert.That(_optionPricingModel.GetInputParametersCheckResult(), Is.EqualTo(string.Empty));
        }

        [TestMethod]
        public void ReturnOptionPrice()
        {
            _blackScholesFormulaProvider.PriceOption(new OptionModel(), out var message).ReturnsForAnyArgs(10);
            var price = _optionPricingModel.GetOptionPrice("", 0, 0, 0, 0, 0);
            NUnit.Framework.Assert.That(price, Is.EqualTo(10));
        }

        [TestMethod]
        public void ReturnInputParametersCheckResult()
        {
            _blackScholesFormulaProvider.PriceOption(new OptionModel(), out Arg.Any<string>())
                .ReturnsForAnyArgs(x => { x[1] = "OK"; return 10; });
            var price = _optionPricingModel.GetOptionPrice("", 0, 0, 0, 0, 0);
            var message = _optionPricingModel.GetInputParametersCheckResult();
            NUnit.Framework.Assert.That(message, Is.EqualTo("OK"));
        }
    }
}
