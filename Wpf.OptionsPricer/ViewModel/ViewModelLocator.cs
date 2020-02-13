/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Wpf.OptionsPricer"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using BlackScholesPricingModel;
using BlackScholesPricingModel.Contracts;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Statistics;
using Statistics.Contracts;
using System.Collections.Generic;
using Wpf.OptionsPricer.Model;
using Wpf.OptionsPricer.Model.Contracts;

namespace Wpf.OptionsPricer.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public OptionPricingViewModel OptionPricing => ServiceLocator.Current.GetInstance<OptionPricingViewModel>();

        public InfoViewModel Info => ServiceLocator.Current.GetInstance<InfoViewModel>();

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<INormalDistributionDataProvider, NormalDistributionDataProvider>();
            SimpleIoc.Default.Register<IOptionPricingStrategy>(() => new CallPricingStrategy(ServiceLocator.Current.GetInstance<INormalDistributionDataProvider>()), "Call");
            SimpleIoc.Default.Register<IOptionPricingStrategy>(() => new PutPricingStrategy(ServiceLocator.Current.GetInstance<INormalDistributionDataProvider>()), "Put");
            SimpleIoc.Default.Register<IOptionPricingContext>(() => new OptionPricingContext(new List<IOptionPricingStrategy>
            {
                ServiceLocator.Current.GetInstance<IOptionPricingStrategy>("Call"),
                ServiceLocator.Current.GetInstance<IOptionPricingStrategy>("Put")
            }));
            SimpleIoc.Default.Register<IBlackScholesParametersValidator, BlackScholesParametersValidator>();
            SimpleIoc.Default.Register<IBlackScholesFormulaProvider, BlackScholesFormulaProvider>();

            SimpleIoc.Default.Register<IOptionPricingModel, OptionPricingModel>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<OptionPricingViewModel>();
            SimpleIoc.Default.Register<InfoViewModel>();
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}