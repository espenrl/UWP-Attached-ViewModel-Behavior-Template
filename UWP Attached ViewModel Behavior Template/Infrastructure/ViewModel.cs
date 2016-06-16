using System;
using System.Reactive.Disposables;
using JetBrains.Annotations;
using Prism.Windows.Mvvm;

namespace UWPAttachedViewModelBehaviorTemplate
{
    /// <summary>
    /// Extends Prism ViewModelBase with behaviors support, DI resolution of behaviors and registered object dispose.
    /// </summary>
    /// <typeparam name="TViewModel">Viewmodel</typeparam>
    /// <seealso cref="ViewModelBase" />
    /// <seealso cref="IDisposableList" />
    public abstract class ViewModel<TViewModel> : ViewModelBase, IDisposableList
        where TViewModel : ViewModel<TViewModel> // TViewModel should be the derived viewmodel class - needed for DI resolution of behaviors controller
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        protected readonly ViewModelBehaviorsController<TViewModel> BehaviorsController;

        protected ViewModel([NotNull] Func<TViewModel, ViewModelBehaviorsController<TViewModel>> behaviorsControllerFactory)
        {
            if (behaviorsControllerFactory == null) throw new ArgumentNullException(nameof(behaviorsControllerFactory));

            // instantiate behaviors controller
            var viewModel = (TViewModel) this;
            BehaviorsController = behaviorsControllerFactory(viewModel);
            RegisterDisposable(BehaviorsController);
        }

        /// <summary>
        /// Register a disposable object. The registered object will be disposed when disposing the viewmodel.
        /// </summary>
        /// <param name="disposable">The disposable object.</param>
        public void RegisterDisposable(IDisposable disposable)
        {
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