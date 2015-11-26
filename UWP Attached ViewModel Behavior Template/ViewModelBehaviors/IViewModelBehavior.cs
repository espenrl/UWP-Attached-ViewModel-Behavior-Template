namespace UWP_Attached_ViewModel_Behavior_Template.ViewModelBehaviors
{
    public interface IViewModelBehavior<T>
    {
        void Start(T viewModel);
    }
}