using System;
using JetBrains.Annotations;

namespace UWPAttachedViewModelBehaviorTemplate
{
    public interface IViewModel : IDisposable
    {
        void AddDisposable([NotNull] IDisposable disposable);
    }
}