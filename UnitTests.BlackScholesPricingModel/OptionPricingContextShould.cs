using System;
using System.Collections.Generic;
using BlackScholesPricingModel;
using BlackScholesPricingModel.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace UnitTests.BlackScholesPricingModel
{
    [TestClass]
    public class OptionPricingContextShould
    {
        private readonly IOptionPricingContext _optionPricingContext;
        private readonly IOptionPricingStrategy _callPricingStrategy;
        private readonly IOptionPricingStrategy _putPricingStrategy;

        public OptionPricingContextShould()
        {
            _callPricingStrategy = Substitute.For<IOptionPricingStrategy>();
            _putPricingStrategy = Substitute.For<IOptionPricingStrategy>();

            _callPricingStrategy.Name.Returns("Call");
            _putPricingStrategy.Name.Returns("Put");

            _optionPricingContext = new OptionPricingContext(new List<IOptionPricingStrategy>
            {
                _callPricingStrategy,
                _putPricingStrategy
            });
        }

        [TestMethod]
        public void ResolveIOptionPricingStrategyWithCallArgument()
        {
            var instance = _optionPricingContext.Resolve("Call");
            Assert.IsInstanceOfType(instance, typeof(IOptionPricingStrategy));
            Assert.IsTrue(instance.Name.Equals("Call"));
        }

        [TestMethod]
        public void ResolveIOptionPricingStrategyWithPutArgument()
        {
            var instance = _optionPricingContext.Resolve("Put");
            Assert.IsInstanceOfType(instance, typeof(IOptionPricingStrategy));
            Assert.IsTrue(instance.Name.Equals("Put"));
        }

        [TestMethod]
        public void ThrowArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => _optionPricingContext.Resolve("BullSpread"));
        }
    }
}
