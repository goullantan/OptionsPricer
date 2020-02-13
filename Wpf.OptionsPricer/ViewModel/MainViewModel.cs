using CommonServiceLocator;
using log4net;
using log4net.Config;
using System;
using System.Reflection;
using WpfHelper;

namespace Wpf.OptionsPricer.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : HamburgerMenuItemViewModelBase
    {
        private static readonly ILog Logger = LogManager.GetLogger(nameof(MainViewModel));

        public string About { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            XmlConfigurator.Configure();
            Title = "Option Pricer";
            AppVersion = $"Version: {Assembly.GetExecutingAssembly().GetName().Version.ToString(3)}";
            Author = "Antoine Goullant";
            About = $"{AppVersion} - Author: {Author}";
            Logger.Info("***** Starting OptionsPricer App *****");
            CreateMenuItems();
        }

        private void CreateMenuItems()
        {
            try
            {
                AddHamburgerMenuIconItem("Pricer", "Option Pricing", MahApps.Metro.IconPacks.PackIconMaterialKind.CurrencyEur,
                    ServiceLocator.Current.GetInstance<OptionPricingViewModel>());
                AddHamburgerMenuIconOptionItem("Information", "Practice Guide", MahApps.Metro.IconPacks.PackIconMaterialKind.Information,
                    ServiceLocator.Current.GetInstance<InfoViewModel>());
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}