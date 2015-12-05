using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Unity.Windows;

namespace UWPAttachedViewModelBehaviorTemplate
{
    public abstract class PrismUnityApplicationEx : PrismUnityApplication
    {
        private readonly Dictionary<Type, Func<IUnityContainer>> _viewModels = new Dictionary<Type, Func<IUnityContainer>>();

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewModelFactory(ViewModelFactory);
        }

        protected void RegisterViewModelConfigurator<T>(Action<IUnityContainer> configuratorCallback)
        {
            _viewModels.Add(typeof(T), () =>
            {
                var childContainer = Container.CreateChildContainer();
                childContainer.RegisterViewModel<T>();
                configuratorCallback(childContainer);

                return childContainer;
            });
        }

        private object ViewModelFactory(Type viewModelType)
        {
            Func<IUnityContainer> configuratorCallback;
            if (!_viewModels.TryGetValue(viewModelType, out configuratorCallback))
            {
                // TODO: error message
                throw new Exception();
            }

            var container = configuratorCallback();

            return container.Resolve(viewModelType);
        }
    }
}