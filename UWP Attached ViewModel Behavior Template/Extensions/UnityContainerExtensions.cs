using System;
using Microsoft.Practices.Unity;

namespace UWPAttachedViewModelBehaviorTemplate
{
    public static class UnityContainerExtensions
    {
        public static void RegisterViewModel<T>(this IUnityContainer container)
        {
            // view model
            container.RegisterType<T>(nameof(T));

            // controller factory
            container.RegisterType<Func<T, ViewModelBehaviorsController<T>>>(
                new HierarchicalLifetimeManager(),
                new InjectionFactory(c =>
                    new Func<T, ViewModelBehaviorsController<T>>(
                        viewModel =>
                            c.Resolve<ViewModelBehaviorsController<T>>(new DependencyOverride<T>(viewModel)))));
        }

        public static void RegisterViewModelBehavior<TV, TB>(this IUnityContainer container) where TB: IViewModelBehavior<TV>
        {
            container.RegisterType<IViewModelBehavior<TV>, TB>(nameof(TB));
        }
    }
}