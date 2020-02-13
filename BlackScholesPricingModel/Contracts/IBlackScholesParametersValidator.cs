namespace BlackScholesPricingModel.Contracts
{
    public interface IBlackScholesParametersValidator
    {
        /// <summary>
        /// Check Parameters
        /// </summary>
        /// <param name="optionModel"></param>
        /// <returns></returns>
        string CheckParameters(OptionModel optionModel);
    }
}
