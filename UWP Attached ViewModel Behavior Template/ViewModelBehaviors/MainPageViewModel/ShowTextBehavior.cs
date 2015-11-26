using System;
using Windows.UI.Popups;
using UWP_Attached_ViewModel_Behavior_Template.ViewModels;

namespace UWP_Attached_ViewModel_Behavior_Template.ViewModelBehaviors
{
    public class ShowTextBehavior : IMainPageViewModelBehavior
    {
        private MainPageViewModel _vm;

        public void Start(MainPageViewModel viewModel)
        {
            _vm = viewModel;
            viewModel.ShowTextCommand.Subscribe(_ => ShowMessageDialog());
        }

        private async void ShowMessageDialog()
        {
            var dialog = new MessageDialog(_vm.Text.Value);
            await dialog.ShowAsync();
        }
    }
}