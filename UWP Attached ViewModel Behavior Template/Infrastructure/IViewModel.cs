using System;

namespace UWPAttachedViewModelBehaviorTemplate
{
    public interface IViewModel : IDisposable
    {
        void AddDisposable(IDisposable disposable);
    }
}