using System;
using JetBrains.Annotations;
using Microsoft.Practices.Unity;

namespace UWPAttachedViewModelBehaviorTemplate
{
    public static class UnityContainerExtensions
    {
        public static void RegisterViewModelWithBehaviors<T>([NotNull] this IUnityContainer container)
        {
            // viewmodel
            container.RegisterType<T>();

            // viewmodel behaviors factory
            container.RegisterType<Func<T, ViewModelBehaviorsController<T>>>(
                new HierarchicalLifetimeManager(),
                new InjectionFactory(c =>
                    new Func<T, ViewModelBehaviorsController<T>>(
                        viewModel =>
                            c.Resolve<ViewModelBehaviorsController<T>>(new DependencyOverride<T>(viewModel)))));
        }

        public static void RegisterViewModelBehavior<TV, TB>([NotNull] this IUnityContainer container) where TB: IViewModelBehavior<TV>
        {
            container.RegisterType<IViewModelBehavior<TV>, TB>(typeof(TB).Name);
        }
    }
}