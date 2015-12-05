using System;
using System.Reactive.Disposables;

namespace UWPAttachedViewModelBehaviorTemplate
{
    public abstract class ViewModelBehavior<T> : IViewModelBehavior<T>
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        protected T ViewModel;

        public void Start(T viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
            ViewModel = viewModel;
            OnStart();
        }

        protected virtual void OnStart()
        {
            
        }

        protected void AddDisposable(IDisposable disposable)
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