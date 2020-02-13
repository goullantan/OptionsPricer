namespace BlackScholesPricingModel.Contracts
{
    public interface IBlackScholesFormulaProvider
    {
        /// <summary>
        /// Price option
        /// </summary>
        /// <param name="option"></param>
        /// <param name="inputParametersCheckResult"></param>
        /// <returns></returns>
        double PriceOption(OptionModel option, out string inputParametersCheckResult);
    }
}
