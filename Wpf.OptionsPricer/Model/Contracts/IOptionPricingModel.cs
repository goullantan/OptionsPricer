namespace Wpf.OptionsPricer.Model.Contracts
{
    public interface IOptionPricingModel
    {
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
        double GetOptionPrice(string optionName, double stockPrice, double strikePrice, double timeToMaturity, double standardDeviation, double riskFreeInterestRate);

        /// <summary>
        /// Get Input Parameters Check Result
        /// </summary>
        /// <returns></returns>
        string GetInputParametersCheckResult();
    }
}
