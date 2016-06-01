using System;
using JetBrains.Annotations;

namespace UWPAttachedViewModelBehaviorTemplate
{
    /// <summary>
    /// Surface contract for a viewmodel behavior.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="IDisposable" />
    public interface IViewModelBehavior<in T> : IDisposable
    {
        /// <summary>
        /// Start point of a behavior for determistic start.
        /// </summary>
        /// <param name="viewModel">The viewmodel.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <seealso cref="ViewModelBehaviorsController{T}" />
        void Start([NotNull] T viewModel);
    }
}