namespace Statistics.Contracts
{
    public interface INormalDistributionDataProvider
    {
        /// <summary>
        /// Gets the cumulative distribution function (cdf) for this distribution evaluated at point x.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        double GetCumulativeNormalDistributionAtPoint(double x);
    }
}
