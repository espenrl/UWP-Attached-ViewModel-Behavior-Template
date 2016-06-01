using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using UWPAttachedViewModelBehaviorTemplate.ViewModelBehaviors;
using UWPAttachedViewModelBehaviorTemplate.ViewModels;

namespace UWPAttachedViewModelBehaviorTemplate
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : PrismUnityApplicationEx
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

            // MainPageViewModel
            RegisterViewModelContainerConfigurator<MainPageViewModel>(c =>
            {
                c.RegisterViewModelBehavior<MainPageViewModel, ShowTextBehavior>();
            });
        }
    }
}