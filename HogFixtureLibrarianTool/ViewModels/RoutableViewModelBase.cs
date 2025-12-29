namespace HogFixtureLibrarianTool.ViewModels;

public class RoutableViewModelBase : ReactiveObject, IRoutableViewModel, IValidatableViewModel, IActivatableViewModel
{
    public ViewModelActivator Activator { get; } = new();

    public IScreen HostScreen { get; protected set; }

    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    public IValidationContext ValidationContext { get; } = new ValidationContext();
}