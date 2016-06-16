using Microsoft.Practices.Unity;

namespace UWPAttachedViewModelBehaviorTemplate
{
    /// <summary>
    /// Reference to the viewmodel container including helper methods.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    public class ViewModelContainerReference<TViewModel>
    {
        public ViewModelContainerReference(IUnityContainer viewModelContainer)
        {
            ViewModelContainer = viewModelContainer;
        }

        /// <summary>
        /// Gets the viewmodel container / child container. Use for custom container registrations.
        /// </summary>
        /// <value>
        /// The viewmodel container.
        /// </value>
        public IUnityContainer ViewModelContainer { get; }

        /// <summary>
        /// Registers the viewmodel behavior with the container.
        /// </summary>
        /// <typeparam name="TBehavior">The type of the behavior.</typeparam>
        /// <seealso cref="ViewModel{T}" />
        /// <seealso cref="ViewModelBehavior{T}" />
        public void RegisterViewModelBehavior<TBehavior>()
            where TBehavior : IViewModelBehavior<TViewModel>
        {
            ViewModelContainer.RegisterType<IViewModelBehavior<TViewModel>, TBehavior>(typeof(TBehavior).Name);
        }
    }
}