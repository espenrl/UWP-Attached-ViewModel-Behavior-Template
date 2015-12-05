using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Unity.Windows;

namespace UWPAttachedViewModelBehaviorTemplate
{
    public abstract class PrismUnityApplicationEx : PrismUnityApplication
    {
        private readonly Dictionary<Type, Func<IUnityContainer>> _viewModelConfigurators = new Dictionary<Type, Func<IUnityContainer>>();

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewModelFactory(ViewModelFactory);
        }

        protected void RegisterViewModelConfigurator<T>(Action<IUnityContainer> configuratorCallback) where T : ViewModel<T>
        {
            _viewModelConfigurators.Add(typeof(T), () => CreateChildContainer<T>(configuratorCallback));
        }

        private IUnityContainer CreateChildContainer<T>(Action<IUnityContainer> containerConfiguratorCallback)
        {
            var childContainer = Container.CreateChildContainer();
            childContainer.RegisterViewModel<T>();
            containerConfiguratorCallback(childContainer);

            return childContainer;
        }

        private object ViewModelFactory(Type viewModelType)
        {
            Func<IUnityContainer> containerConfiguratorCallback;
            if (_viewModelConfigurators.TryGetValue(viewModelType, out containerConfiguratorCallback))
            {
                var container = containerConfiguratorCallback();

                var viewModel = container.Resolve(viewModelType) as IViewModel;
                if (viewModel != null)
                {
                    viewModel.AddDisposable(container);
                }

                return viewModel;
            }

            // fallback to default
            return Container.Resolve(viewModelType);
        }
    }
}