using System;
using System.Reactive.Disposables;
using JetBrains.Annotations;

namespace UWPAttachedViewModelBehaviorTemplate
{
    [UsedImplicitly]
    public class ViewModelBehaviorsController<T> : IDisposable
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        private readonly T _viewModel;
        private readonly IViewModelBehavior<T>[] _behaviors;

        public ViewModelBehaviorsController([NotNull] T viewModel, [NotNull] IViewModelBehavior<T>[] behaviors)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
            if (behaviors == null) throw new ArgumentNullException(nameof(behaviors));

            _viewModel = viewModel;
            _behaviors = behaviors;

            foreach (var behavior in behaviors)
            {
                _disposables.Add(behavior);
            }
        }

        /// <summary>
        /// Starts all behaviors.
        /// NOTE: Has to be called manually from viewmodel for a determistic start.
        /// </summary>
        public void Start()
        {
            foreach (var behavior in _behaviors)
            {
                behavior.Start(_viewModel);
            }
        }

        /// <summary>
        /// Releases all behaviors.
        /// </summary>
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}