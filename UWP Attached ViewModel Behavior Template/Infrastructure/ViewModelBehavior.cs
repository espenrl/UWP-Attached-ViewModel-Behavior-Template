using System;
using System.Reactive.Disposables;
using JetBrains.Annotations;

namespace UWPAttachedViewModelBehaviorTemplate
{
    public abstract class ViewModelBehavior<T> : IViewModelBehavior<T>
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        protected T ViewModel;

        public void Start([NotNull] T viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
            ViewModel = viewModel;
            OnStart();
        }

        protected virtual void OnStart()
        {
        }

        protected void AddDisposable([NotNull] IDisposable disposable)
        {
            if (disposable == null) throw new ArgumentNullException(nameof(disposable));
            _disposables.Add(disposable);
        }

        public virtual void Dispose()
        {
            _disposables.Dispose();
        }
    }
}