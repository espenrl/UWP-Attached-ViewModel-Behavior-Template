using Windows.UI.Xaml.Controls;
using UWPAttachedViewModelBehaviorTemplate.ViewModels;

namespace UWPAttachedViewModelBehaviorTemplate.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public MainPageViewModel ViewModel => DataContext as MainPageViewModel;
    }
}