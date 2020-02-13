using Accord.Statistics.Distributions.Univariate;
using Statistics.Contracts;
using System;

namespace Statistics
{
    public class NormalDistributionDataProvider : INormalDistributionDataProvider
    {
        private readonly NormalDistribution _normalDistribution;

        public NormalDistributionDataProvider()
        {
            _normalDistribution = new NormalDistribution();
        }

        /// <summary>
        /// Gets the cumulative distribution function (cdf) for this distribution evaluated at point x.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double GetCumulativeNormalDistributionAtPoint(double x)
        {
            if (x == 0) throw new ArgumentException("Input data must be >= 0");
            return _normalDistribution.DistributionFunction(x);
        }
    }
}
