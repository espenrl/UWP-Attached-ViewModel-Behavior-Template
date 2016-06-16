using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Unity.Windows;

namespace UWPAttachedViewModelBehaviorTemplate
{
    /// <summary>
    /// Adds support for viewmodel containers (each viewmodel get it's own child container) and viewmodel behaviors.
    /// </summary>
    /// <seealso cref="PrismUnityApplication" />
    public abstract class PrismUnityApplicationEx : PrismUnityApplication
    {
        private readonly Dictionary<Type, Func<IUnityContainer>> _createViewModelContainerFuncMap = new Dictionary<Type, Func<IUnityContainer>>();

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            // override Prism viewmodel resolution
            ViewModelLocationProvider.SetDefaultViewModelFactory(ViewModelFactory);
        }

        /// <summary>
        /// Registers the viewmodel container configurator. Use for registering behaviors with container as well as custom registrations.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the viewmodel.</typeparam>
        /// <param name="viewModelContainerConfiguratorCallback">The viewmodel container configurator callback.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected void RegisterViewModelContainerConfigurator<TViewModel>([NotNull] Action<ViewModelContainerReference<TViewModel>> viewModelContainerConfiguratorCallback)
            where TViewModel : ViewModel<TViewModel>
        {
            if (viewModelContainerConfiguratorCallback == null)
            {
                throw new ArgumentNullException(nameof(viewModelContainerConfiguratorCallback));
            }

            // function for creating a viewmodel container - this line removes dependency on generic type
            Func<IUnityContainer> createViewModelContainerFunc = () => CreateViewModelContainer<TViewModel>(Container, viewModelContainerConfiguratorCallback);

            // add to lookup - map on typeof(TViewModel)
            _createViewModelContainerFuncMap.Add(typeof(TViewModel), createViewModelContainerFunc);
        }

        private object ViewModelFactory(Type viewModelType)
        {
            // try get viewmodel container func (for creation of container on request)
            Func<IUnityContainer> createViewModelContainerFunc;
            var viewModelTypeHasRegistration = _createViewModelContainerFuncMap.TryGetValue(viewModelType, out createViewModelContainerFunc);

            if (viewModelTypeHasRegistration)
            {
                // instantiate viewmodel container
                var container = createViewModelContainerFunc();

                // get viewmodel from container
                var viewModel = container.Resolve(viewModelType) as IDisposableList;
                viewModel?.RegisterDisposable(container);

                return viewModel;
            }

            // no registration for viewModelType found -> fallback to default resolution from main container
            return Container.Resolve(viewModelType);
        }

        private static IUnityContainer CreateViewModelContainer<TViewModel>(IUnityContainer parentContainer, Action<ViewModelContainerReference<TViewModel>> viewModelContainerConfiguratorCallback)
        {
            // create child container for viewmodel
            var childContainer = parentContainer.CreateChildContainer();

            // register viewmodel and controller (viewmodel behaviors controller)
            childContainer.RegisterViewModelAndBehaviorsControllerFactory<TViewModel>();

            // run callback which should register behaviors and custom registrations with viewmodel container / child container
            viewModelContainerConfiguratorCallback(new ViewModelContainerReference<TViewModel>(childContainer));

            return childContainer;
        }
    }
}