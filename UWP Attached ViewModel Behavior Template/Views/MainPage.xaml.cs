using Windows.UI.Xaml.Controls;
using UWP_Attached_ViewModel_Behavior_Template.ViewModels;

namespace UWP_Attached_ViewModel_Behavior_Template.Views
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