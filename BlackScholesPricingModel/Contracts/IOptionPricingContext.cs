namespace BlackScholesPricingModel.Contracts
{
    public interface IOptionPricingContext
    {
        /// <summary>
        /// Resolve
        /// </summary>
        /// <param name="optionName"></param>
        /// <returns></returns>
        IOptionPricingStrategy Resolve(string optionName);
    }
}
