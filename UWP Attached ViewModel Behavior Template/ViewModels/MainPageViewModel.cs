using System;
using System.Reactive.Linq;
using JetBrains.Annotations;
using Reactive.Bindings;

namespace UWPAttachedViewModelBehaviorTemplate.ViewModels
{
    [UsedImplicitly]
    public class MainPageViewModel : ViewModel<MainPageViewModel>
    {
        public MainPageViewModel(Func<MainPageViewModel, ViewModelBehaviorsController<MainPageViewModel>> controllerFactory) : base(controllerFactory)
        {
            ShowTextCommand = Text.Select(str => !string.IsNullOrWhiteSpace(str)).ToReactiveCommand();

            // determistic start of behaviors
            BehaviorsController.Start();
        }

        public ReactiveProperty<string> Text { get; } = new ReactiveProperty<string>();

        public ReactiveCommand ShowTextCommand { get; }
    }
}