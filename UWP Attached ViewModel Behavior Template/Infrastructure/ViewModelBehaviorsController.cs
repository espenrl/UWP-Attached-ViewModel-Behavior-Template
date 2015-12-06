using System;
using System.Reactive.Disposables;
using JetBrains.Annotations;

namespace UWPAttachedViewModelBehaviorTemplate
{
    public class ViewModelBehaviorsController<T> : IDisposable
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        private readonly T _viewModel;
        private readonly IViewModelBehavior<T>[] _behaviors;

        public ViewModelBehaviorsController([NotNull] T viewModel, [NotNull] IViewModelBehavior<T>[] behaviors)
        {
            _viewModel = viewModel;
            _behaviors = behaviors;
        }

        public void Start()
        {
            foreach (var behavior in _behaviors)
            {
                _disposables.Add(behavior);
                behavior.Start(_viewModel);
            }
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}