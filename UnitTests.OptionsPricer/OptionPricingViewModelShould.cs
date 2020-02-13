using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using Wpf.OptionsPricer.Model.Contracts;
using Wpf.OptionsPricer.ViewModel;

namespace UnitTests.OptionsPricer
{
    [TestClass]
    public class OptionPricingViewModelShould
    {
        private readonly IOptionPricingModel _optionPricingModel;
        private readonly OptionPricingViewModel _optionPricingViewModel;

        public OptionPricingViewModelShould()
        {
            _optionPricingModel = Substitute.For<IOptionPricingModel>();
            _optionPricingViewModel = new OptionPricingViewModel(_optionPricingModel);
        }

        [TestMethod]
        public void ReturnSelectOptionNameMessage()
        {
            //Click PriceButton Action
            _optionPricingViewModel.OnClickPriceButton();
            NUnit.Framework.Assert.That(_optionPricingViewModel.InputParametersCheckResult, Is.EqualTo("Please select an option name."));
        }

        [TestMethod]
        public void SetValueToOptionPriceAndInputParametersCheckResult()
        {
            _optionPricingViewModel.OptionName = "Call";

            //OptionPrice
            _optionPricingModel.GetOptionPrice("",0,0,0,0,0).ReturnsForAnyArgs(10);
            //InputParametersCheckResult
            _optionPricingModel.GetInputParametersCheckResult().Returns("OK");

            //Click PriceButton Action
            _optionPricingViewModel.OnClickPriceButton();

            NUnit.Framework.Assert.That(_optionPricingViewModel.OptionPrice, Is.EqualTo(10));
            NUnit.Framework.Assert.That(_optionPricingViewModel.InputParametersCheckResult, Is.EqualTo("OK"));
        }
    }
}
