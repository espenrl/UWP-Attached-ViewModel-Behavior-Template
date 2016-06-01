using System;
using JetBrains.Annotations;

namespace UWPAttachedViewModelBehaviorTemplate
{
    /// <summary>
    /// Surface contract for a viewmodel that supports behaviors.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public interface IViewModel : IDisposable
    {
        void AddDisposable([NotNull] IDisposable disposable);
    }
}