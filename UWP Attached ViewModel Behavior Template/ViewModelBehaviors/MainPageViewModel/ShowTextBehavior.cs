using System;
using Windows.UI.Popups;
using JetBrains.Annotations;
using UWPAttachedViewModelBehaviorTemplate.ViewModels;

namespace UWPAttachedViewModelBehaviorTemplate.ViewModelBehaviors
{
    [UsedImplicitly]
    public class ShowTextBehavior : ViewModelBehavior<MainPageViewModel>
    {
        protected override void OnStart()
        {
            // subscribe to command - add subscription to dispose list
            RegisterDisposable(ViewModel.ShowTextCommand.Subscribe(_ => ShowMessageDialog()));
        }

        private async void ShowMessageDialog()
        {
            // ViewModel: property available on a behavior
            var dialog = new MessageDialog(ViewModel.Text.Value, "The message is");
            await dialog.ShowAsync();
        }
    }
}