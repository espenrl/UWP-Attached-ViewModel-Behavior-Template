using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Microsoft.Practices.Unity;
using Prism.Unity.Windows;
using UWP_Attached_ViewModel_Behavior_Template.ViewModelBehaviors;
using UWP_Attached_ViewModel_Behavior_Template.ViewModels;

namespace UWP_Attached_ViewModel_Behavior_Template
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : PrismUnityApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            NavigationService.Navigate("Main", null);
            return Task.FromResult<object>(null);
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            
            // view models
            RegisterTypeIfMissing(typeof(MainPageViewModel), typeof(MainPageViewModel), true);

            // behaviors
            Container.RegisterType<IMainPageViewModelBehavior, ShowTextBehavior>(nameof(ShowTextBehavior));
        }
    }
}