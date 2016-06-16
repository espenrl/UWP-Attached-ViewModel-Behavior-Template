using System;
using System.Reactive.Disposables;
using JetBrains.Annotations;

namespace UWPAttachedViewModelBehaviorTemplate
{
    public abstract class ViewModelBehavior<T> : IViewModelBehavior<T>
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        protected T ViewModel;

        /// <summary>
        /// Start point of a behavior for determistic start.
        /// </summary>
        /// <param name="viewModel">The viewmodel.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <seealso cref="ViewModelBehaviorsController{T}" />
        public void Start([NotNull] T viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            ViewModel = viewModel;
            OnStart();
        }

        /// <summary>
        /// Start point of a behavior for determistic start.
        /// </summary>
        /// <seealso cref="ViewModelBehaviorsController{T}" />
        protected virtual void OnStart() {}

        /// <summary>
        /// Register a disposable object. The registered object will be disposed when disposing the viewmodel behavior.
        /// </summary>
        /// <param name="disposable">The disposable object.</param>
        public void RegisterDisposable([NotNull] IDisposable disposable)
        {
            if (disposable == null) throw new ArgumentNullException(nameof(disposable));

            _disposables.Add(disposable);
        }

        /// <summary>
        /// Releases all disposable objects registered with RegisterDisposable method.
        /// </summary>
        public virtual void Dispose()
        {
            _disposables.Dispose();
        }
    }
}