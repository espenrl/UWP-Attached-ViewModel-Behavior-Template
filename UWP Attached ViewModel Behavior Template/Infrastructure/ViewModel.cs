using System;
using System.Reactive.Disposables;
using JetBrains.Annotations;
using Prism.Windows.Mvvm;

namespace UWPAttachedViewModelBehaviorTemplate
{
    /// <summary>
    /// Extends Prism ViewModelBase with behaviors support, DI behaviors resolution and disposable objects support.
    /// </summary>
    /// <typeparam name="TViewModel">Viewmodel</typeparam>
    /// <seealso cref="ViewModelBase" />
    /// <seealso cref="IViewModel" />
    public abstract class ViewModel<TViewModel> : ViewModelBase, IViewModel
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
            AddDisposable(BehaviorsController);
        }

        /// <summary>
        /// Adds disposable object that will be disposed when disposing the viewmodel.
        /// </summary>
        /// <param name="disposable">The disposable object.</param>
        public void AddDisposable(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        /// <summary>
        /// Releases all disposable objects registered with AddDisposable method.
        /// </summary>
        public virtual void Dispose()
        {
            _disposables.Dispose();
        }
    }
}