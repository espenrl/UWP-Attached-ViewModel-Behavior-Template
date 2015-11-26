using System.Linq;
using System.Reactive.Linq;
using Prism.Windows.Mvvm;
using Reactive.Bindings;
using UWP_Attached_ViewModel_Behavior_Template.ViewModelBehaviors;

namespace UWP_Attached_ViewModel_Behavior_Template.ViewModels
{
    public interface IMainPageViewModelBehavior : IViewModelBehavior<MainPageViewModel> { }

    public class MainPageViewModel : ViewModelBase
    {
        private readonly IMainPageViewModelBehavior[] _behaviors;

        public MainPageViewModel(IMainPageViewModelBehavior[] behaviors)
        {
            ShowTextCommand = Text.Select(str => !string.IsNullOrWhiteSpace(str)).ToReactiveCommand();

            // initialize behaviors
            _behaviors = behaviors;
            _behaviors.ForEach(b => b.Start(this));
        }

        public ReactiveProperty<string> Text { get; } = new ReactiveProperty<string>();

        public ReactiveCommand ShowTextCommand { get; }
    }
}