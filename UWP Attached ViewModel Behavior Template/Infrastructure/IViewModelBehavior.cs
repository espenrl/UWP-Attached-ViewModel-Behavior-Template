using System;

namespace UWPAttachedViewModelBehaviorTemplate
{
    public interface IViewModelBehavior<T> : IDisposable
    {
        void Start(T viewModel);
    }
}