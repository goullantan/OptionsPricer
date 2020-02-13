using BlackScholesPricingModel;
using BlackScholesPricingModel.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.BlackScholesPricingModel
{
    [TestClass]
    public class BlackScholesParametersValidatorShould
    {
        private readonly IBlackScholesParametersValidator _blackScholesParametersValidator;

        public BlackScholesParametersValidatorShould()
        {
            _blackScholesParametersValidator = new BlackScholesParametersValidator();
        }

        [TestMethod]
        public void ReturnStockPriceErrorMessage()
        {
            var optionModel = new OptionModel
            {
                StockPrice = -1//bad
            };
            var message = _blackScholesParametersValidator.CheckParameters(optionModel);
            Assert.IsTrue(message.Equals("Error : StockPrice must be > 0"));
        }

        [TestMethod]
        public void ReturnStrikePriceErrorMessage()
        {
            var optionModel = new OptionModel
            {
                StockPrice = 10,//good
                StrikePrice = -1//bad
            };
            var message = _blackScholesParametersValidator.CheckParameters(optionModel);
            Assert.IsTrue(message.Equals("Error : StrikePrice must be > 0"));
        }

        [TestMethod]
        public void ReturnTimeToMaturityErrorMessage()
        {
            var optionModel = new OptionModel
            {
                StockPrice = 10,//good
                StrikePrice = 10,//good
                TimeToMaturity = -1//bad
            };
            var message = _blackScholesParametersValidator.CheckParameters(optionModel);
            Assert.IsTrue(message.Equals("Error : TimeToMaturity must be > 0"));
        }

        [TestMethod]
        public void ReturnStandardDeviationErrorMessage()
        {
            var optionModel = new OptionModel
            {
                StockPrice = 10,//good
                StrikePrice = 10,//good
                TimeToMaturity = 10,//good
                StandardDeviation = -1//bad
            };
            var message = _blackScholesParametersValidator.CheckParameters(optionModel);
            Assert.IsTrue(message.Equals("Error : StandardDeviation must be > 0"));
        }

        [TestMethod]
        public void ReturnValidationMessage()
        {
            var optionModel = new OptionModel
            {
                StockPrice = 10,//good
                StrikePrice = 10,//good
                TimeToMaturity = 10,//good
                StandardDeviation = 10//good
            };
            var message = _blackScholesParametersValidator.CheckParameters(optionModel);
            Assert.IsTrue(message.Equals("All parameters correctly filled"));        
        }
    }
}
