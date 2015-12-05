using System;
using System.Reactive.Disposables;
using Prism.Windows.Mvvm;

namespace UWPAttachedViewModelBehaviorTemplate
{
    public abstract class ViewModel<T> : ViewModelBase, IDisposable where T:ViewModel<T>
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        protected readonly ViewModelBehaviorsController<T> BehaviorsController;

        protected ViewModel(Func<T, ViewModelBehaviorsController<T>> controllerFactory)
        {
            var viewModel = (T) this;
            BehaviorsController = controllerFactory(viewModel);
            AddDisposable(BehaviorsController);
        }

        public void AddDisposable(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public virtual void Dispose()
        {
            _disposables.Dispose();
        }
    }
}