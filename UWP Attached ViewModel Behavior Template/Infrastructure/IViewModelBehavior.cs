using System;
using JetBrains.Annotations;

namespace UWPAttachedViewModelBehaviorTemplate
{
    public interface IViewModelBehavior<in T> : IDisposable
    {
        void Start([NotNull] T viewModel);
    }
}