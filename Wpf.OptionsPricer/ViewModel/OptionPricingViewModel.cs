using GalaSoft.MvvmLight.Command;
using log4net;
using System.Text;
using System.Windows.Input;
using Wpf.OptionsPricer.Model.Contracts;

namespace Wpf.OptionsPricer.ViewModel
{
    public class OptionPricingViewModel : ViewModelBase
    {
        private static readonly ILog Logger = LogManager.GetLogger(nameof(OptionPricingViewModel));

        #region Binded Properties
        public enum EnumOptionNames
        {
            Call,
            Put
        }

        public ICommand PriceButton { get; set; }

        private double _optionPrice;

        public double OptionPrice
        {
            get => _optionPrice;
            set => SetProperty(ref _optionPrice, value);
        }

        private string _optionName;

        public string OptionName
        {
            get => _optionName;
            set => SetProperty(ref _optionName, value);
        }

        private string _inputParametersCheckResult;

        public string InputParametersCheckResult
        {
            get => _inputParametersCheckResult;
            set => SetProperty(ref _inputParametersCheckResult, value);
        }

        #region Pricing Parameters
        private double _stockPrice;

        public double StockPrice
        {
            get => _stockPrice;
            set => SetProperty(ref _stockPrice, value);
        }

        private double _strikePrice;

        public double StrikePrice
        {
            get => _strikePrice;
            set => SetProperty(ref _strikePrice, value);
        }

        private double _timeToMaturity;

        public double TimeToMaturity
        {
            get => _timeToMaturity;
            set => SetProperty(ref _timeToMaturity, value);
        }

        private double _standardDeviation;

        public double StandardDeviation
        {
            get => _standardDeviation;
            set => SetProperty(ref _standardDeviation, value);
        }

        private double _riskFreeInterestRate;

        public double RiskFreeInterestRate
        {
            get => _riskFreeInterestRate;
            set => SetProperty(ref _riskFreeInterestRate, value);
        }
        #endregion 
        #endregion

        private readonly IOptionPricingModel _optionPricingModel;

        public OptionPricingViewModel(IOptionPricingModel optionPricingModel)
        {
            _optionPricingModel = optionPricingModel;
            PriceButton = new RelayCommand(OnClickPriceButton);
        }

        public void OnClickPriceButton()
        {
            if (OptionName is null)//no option selected
            {
                InputParametersCheckResult = "Please select an option name.";
                return;
            }
            
            Logger.Info($"{BuildLog()}");
            try
            {
                OptionPrice = _optionPricingModel.GetOptionPrice(OptionName, StockPrice, StrikePrice,
                        TimeToMaturity, StandardDeviation, RiskFreeInterestRate);
                InputParametersCheckResult = _optionPricingModel.GetInputParametersCheckResult();
                Logger.Info($"OptionPrice = {OptionPrice} - InputParametersCheckResult = {InputParametersCheckResult}");
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        private string BuildLog()
        {
            var str = new StringBuilder();
            str.Append("Calling BlackSholes Formula with Parameters: ");
            str.Append($"OptionName = {OptionName}, ");
            str.Append($"StockPrice = {StockPrice}, ");
            str.Append($"StrikePrice = {StrikePrice}, ");
            str.Append($"TimeToMaturity = {TimeToMaturity}, ");
            str.Append($"StandardDeviation = {StandardDeviation}, ");
            str.Append($"RiskFreeInterestRate = {RiskFreeInterestRate}");
            return str.ToString();
        }
    }
}