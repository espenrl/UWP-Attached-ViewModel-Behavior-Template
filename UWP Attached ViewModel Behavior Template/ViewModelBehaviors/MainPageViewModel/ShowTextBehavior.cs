using System;
using Windows.UI.Popups;
using UWPAttachedViewModelBehaviorTemplate.ViewModels;

namespace UWPAttachedViewModelBehaviorTemplate.ViewModelBehaviors
{
    public class ShowTextBehavior : ViewModelBehavior<MainPageViewModel>
    {
        protected override void OnStart()
        {
            AddDisposable(ViewModel.ShowTextCommand.Subscribe(_ => ShowMessageDialog()));
        }

        private async void ShowMessageDialog()
        {
            var dialog = new MessageDialog(ViewModel.Text.Value);
            await dialog.ShowAsync();
        }
    }
}