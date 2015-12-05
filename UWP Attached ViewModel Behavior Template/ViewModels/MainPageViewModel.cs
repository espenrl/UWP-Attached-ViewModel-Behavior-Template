using System;
using System.Reactive.Linq;
using Reactive.Bindings;

namespace UWPAttachedViewModelBehaviorTemplate.ViewModels
{
    public class MainPageViewModel : ViewModel<MainPageViewModel>
    {
        public MainPageViewModel(Func<MainPageViewModel, ViewModelBehaviorsController<MainPageViewModel>> controllerFactory) : base(controllerFactory)
        {
            ShowTextCommand = Text.Select(str => !string.IsNullOrWhiteSpace(str)).ToReactiveCommand();

            BehaviorsController.Start();
        }

        public ReactiveProperty<string> Text { get; } = new ReactiveProperty<string>();

        public ReactiveCommand ShowTextCommand { get; }
    }
}