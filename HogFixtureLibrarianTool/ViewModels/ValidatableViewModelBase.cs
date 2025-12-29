namespace HogFixtureLibrarianTool.ViewModels;

public class ValidatableViewModelBase : ReactiveObject, IValidatableViewModel, IActivatableViewModel
{
    public ViewModelActivator Activator { get; } = new();

    public IValidationContext ValidationContext { get; } = new ValidationContext();
}