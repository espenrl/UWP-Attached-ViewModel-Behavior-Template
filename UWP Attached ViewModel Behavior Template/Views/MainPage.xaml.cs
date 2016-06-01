using Windows.UI.Xaml.Controls;
using JetBrains.Annotations;
using UWPAttachedViewModelBehaviorTemplate.ViewModels;

namespace UWPAttachedViewModelBehaviorTemplate.Views
{
    [UsedImplicitly]
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public MainPageViewModel ViewModel => DataContext as MainPageViewModel;
    }
}