namespace BlackScholesPricingModel.Contracts
{
    public interface IOptionPricingStrategy
    {
        string Name { get; }

        /// <summary>
        /// Compute Option Price
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        double ComputeOptionPrice(OptionModel option);
    }
}
