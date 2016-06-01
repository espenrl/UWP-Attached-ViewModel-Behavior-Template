using System;
using JetBrains.Annotations;
using Microsoft.Practices.Unity;

namespace UWPAttachedViewModelBehaviorTemplate
{
    public static class UnityContainerExtensions
    {
        /// <summary>
        /// Registers the viewmodel with the container along with a corresponding behaviors controller factory.
        /// See constructor of ViewModel{T} for instantiation of behaviors controller.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container">The container.</param>
        /// <seealso cref="ViewModel{T}" />
        public static void RegisterViewModelAndBehaviorsControllerFactory<T>([NotNull] this IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

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

        /// <summary>
        /// Registers the viewmodel behavior with the container.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the viewmodel.</typeparam>
        /// <typeparam name="TBehavior">The type of the behavior.</typeparam>
        /// <param name="container">The container.</param>
        /// <seealso cref="ViewModel{T}" />
        /// <seealso cref="ViewModelBehavior{T}" />
        public static void RegisterViewModelBehavior<TViewModel, TBehavior>([NotNull] this IUnityContainer container)
            where TBehavior: IViewModelBehavior<TViewModel>
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            container.RegisterType<IViewModelBehavior<TViewModel>, TBehavior>(typeof(TBehavior).Name);
        }
    }
}