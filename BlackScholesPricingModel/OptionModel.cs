namespace BlackScholesPricingModel
{
    public class OptionModel
    {
        public string Name { get; set; }
        public double D1Parameter { get; set; }
        public double D2Parameter { get; set; }
        public double StockPrice { get; set; }
        public double StrikePrice { get; set; }
        public double TimeToMaturity { get; set; }
        public double StandardDeviation { get; set; }
        public double RiskFreeInterestRate { get; set; }
    }
}
