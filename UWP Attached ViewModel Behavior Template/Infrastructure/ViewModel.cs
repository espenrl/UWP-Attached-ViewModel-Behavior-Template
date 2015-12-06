using System;
using System.Reactive.Disposables;
using JetBrains.Annotations;
using Prism.Windows.Mvvm;

namespace UWPAttachedViewModelBehaviorTemplate
{
    public abstract class ViewModel<T> : ViewModelBase, IViewModel where T : ViewModel<T>
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        protected readonly ViewModelBehaviorsController<T> BehaviorsController;

        protected ViewModel([NotNull] Func<T, ViewModelBehaviorsController<T>> behaviorsControllerFactory)
        {
            if (behaviorsControllerFactory == null) throw new ArgumentNullException(nameof(behaviorsControllerFactory));
            var vm = (T) this;
            BehaviorsController = behaviorsControllerFactory(vm);
            AddDisposable(BehaviorsController);
        }

        public void AddDisposable([NotNull] IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public virtual void Dispose()
        {
            _disposables.Dispose();
        }
    }
}