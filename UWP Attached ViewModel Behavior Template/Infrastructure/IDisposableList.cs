using System;
using JetBrains.Annotations;

namespace UWPAttachedViewModelBehaviorTemplate
{
    /// <summary>
    /// Surface contract for an object that can dispose a list of registered objects (implementing IDisposable).
    /// </summary>
    /// <seealso cref="IDisposable" />
    public interface IDisposableList : IDisposable
    {
        /// <summary>
        /// Register a disposable object. The registered object will be disposed when disposing the object.
        /// </summary>
        /// <param name="disposable">The disposable object.</param>
        void RegisterDisposable([NotNull] IDisposable disposable);
    }
}