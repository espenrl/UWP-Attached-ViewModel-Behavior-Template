# UWP Attached ViewModel Behavior Template

Based on the ideas of Sacha Barber: [Attached VM Behaviours](http://www.codeproject.com/Articles/885009/Attached-VM-Behaviours#Disposable-ViewModels-/-Child-Container-Lifecycle-Management)

The main idea is getting rid of those big viewmodel code files
- separation of viewmodel properties/commands and the viewmodel logic: logic is separated into viewmodel behaviors
- a viewmodel can (and should) have many viewmodel behaviors
- scales very well with number of properties/commands
- separation of logic into smaller units makes unit testing easier
- smaller units increases code maintainability

This project incorporates VM behaviors with PRISM, DI (Unity) and RX - and with ease of use.

####Technical details
- every viewmodel get its own container (child container of main container)
- viewmodel and behaviors are resolved by DI: use constructor for own DI purposes
- IDisposable is implemented such that dispose of viewmodel disposes the container and behaviors

####Example code viewmodel:
Don't worry about the constructor - it's due to DI and is handled automagically.
```csharp
[UsedImplicitly]
public class MainPageViewModel : ViewModel<MainPageViewModel>
{
	public MainPageViewModel(Func<MainPageViewModel, ViewModelBehaviorsController<MainPageViewModel>> controllerFactory)
	  : base(controllerFactory)
	{
		ShowTextCommand = Text.Select(str => !string.IsNullOrWhiteSpace(str)).ToReactiveCommand();

		// determistic start of behaviors
		BehaviorsController.Start();
	}

	public ReactiveProperty<string> Text { get; } = new ReactiveProperty<string>();

	public ReactiveCommand ShowTextCommand { get; }
}
```

####Example code viewmodel behavior:
Show a message box with text from `Text` propery when ShowTextCommand is invoked.
```csharp
[UsedImplicitly]
public class ShowTextBehavior : ViewModelBehavior<MainPageViewModel>
{
	protected override void OnStart()
	{
		// subscribe to command - add subscription to dispose list
		AddDisposable(ViewModel.ShowTextCommand.Subscribe(_ => ShowMessageDialog()));
	}

	private async void ShowMessageDialog()
	{
		// ViewModel: property available on a behavior
		var dialog = new MessageDialog(ViewModel.Text.Value, "The message is");
		await dialog.ShowAsync();
	}
}
```

####Example code: register viewmodel and behavior with DI container (App.xaml.cs)
```csharp
protected override void ConfigureContainer()
{
	// MainPageViewModel
	RegisterViewModelContainerConfigurator<MainPageViewModel>(c =>
	{
		c.RegisterViewModelBehavior<MainPageViewModel, ShowTextBehavior>();
	});
}
```

####Links
- [PRISM](https://www.nuget.org/packages/Prism.Windows)
- [Unity](https://www.nuget.org/packages/Unity/) (dependency injection)
- [Reactive eXtensions](https://www.nuget.org/packages/Rx-Main/) (RX)
- [ReactiveProperty](https://www.nuget.org/packages/ReactiveProperty/): aids use of RX in MVVM pattern

####Notes
Code is annotated with attributes for ReSharper - [JetBrains.Annotations](https://www.nuget.org/packages/JetBrains.Annotations)
