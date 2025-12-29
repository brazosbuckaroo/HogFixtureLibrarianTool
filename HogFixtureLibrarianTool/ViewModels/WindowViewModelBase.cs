namespace HogFixtureLibrarianTool.ViewModels;

public class WindowViewModelBase : ReactiveObject, IScreen, IActivatableViewModel
{
    public ViewModelActivator Activator { get; } = new();

    public RoutingState Router { get; } = new();
}