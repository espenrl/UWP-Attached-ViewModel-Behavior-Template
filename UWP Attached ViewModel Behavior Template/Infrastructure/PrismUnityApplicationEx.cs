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
        /// Registers the viewmodel container configurator. Use for registering behaviors with container as well as other DI objects.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the viewmodel.</typeparam>
        /// <param name="viewModelContainerConfiguratorCallback">The viewmodel container configurator callback.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected void RegisterViewModelContainerConfigurator<TViewModel>([NotNull] Action<IUnityContainer> viewModelContainerConfiguratorCallback)
            where TViewModel : ViewModel<TViewModel>
        {
            if (viewModelContainerConfiguratorCallback == null) throw new ArgumentNullException(nameof(viewModelContainerConfiguratorCallback));

            // function for creating a viewmodel container - this line removes dependency on generic type
            Func<IUnityContainer> createViewModelContainerFunc = () => CreateViewModelContainer<TViewModel>(Container, viewModelContainerConfiguratorCallback);

            // add to dictionary - map on typeof(TViewModel)
            _createViewModelContainerFuncMap.Add(typeof(TViewModel), createViewModelContainerFunc);
        }

        private object ViewModelFactory(Type viewModelType)
        {
            // try get viewmodel container func (for creation of container on request)
            Func<IUnityContainer> createViewModelContainerFunc;
            if (_createViewModelContainerFuncMap.TryGetValue(viewModelType, out createViewModelContainerFunc))
            {
                // instantiate viewmodel container
                var container = createViewModelContainerFunc();

                // get viewmodel from container
                var viewModel = container.Resolve(viewModelType) as IViewModel;
                viewModel?.AddDisposable(container);

                return viewModel;
            }

            // fallback to default resolution
            return Container.Resolve(viewModelType);
        }

        private static IUnityContainer CreateViewModelContainer<T>(IUnityContainer parentContainer, Action<IUnityContainer> viewModelContainerConfiguratorCallback)
        {
            // create child container for viewmodel
            var childContainer = parentContainer.CreateChildContainer();

            // register viewmodel and behaviors controller
            childContainer.RegisterViewModelAndBehaviorsControllerFactory<T>();

            // run callback which should register behaviors and other DI objects with child container (viewmodel container)
            viewModelContainerConfiguratorCallback(childContainer);

            return childContainer;
        }
    }
}