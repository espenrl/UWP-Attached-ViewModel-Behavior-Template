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
        public App()
        {
            InitializeComponent();
        }

        protected override async Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            // navigate to MainPage (start page)
            NavigationService.Navigate("Main", null);
        }
  
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer(); // must be called to initialize PRISM

            /* ### container configurator:
             * on request of page (PRISM)
             * -> create viewmodel container / child container
             * -> run the container configurator
             * -> resolve viewmodel, viewmodel behaviors and all other registered dependencies
             * -> viewmodel returned to PRISM
             */
            RegisterViewModelContainerConfigurator<MainPageViewModel>(c =>
            {
                c.RegisterViewModelBehavior<ShowTextBehavior>(); // generics guarantee only behaviors for MainPageViewModel can be registered

                // c.ViewModelContainer: reference to the viewmodel container - do custom registrations here
            });
        }
    }
}