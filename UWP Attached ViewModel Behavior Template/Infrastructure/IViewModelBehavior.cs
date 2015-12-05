using System;

namespace UWPAttachedViewModelBehaviorTemplate
{
    public interface IViewModelBehavior<in T> : IDisposable
    {
        void Start(T viewModel);
    }
}