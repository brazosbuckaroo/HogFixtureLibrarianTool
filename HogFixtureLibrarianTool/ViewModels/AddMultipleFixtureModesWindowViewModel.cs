namespace HogFixtureLibrarianTool.ViewModels;

public class AddMultipleFixtureModesWindowViewModel : ValidatableViewModelBase
{
    private string _numberOfFixtureModes;

    public AddMultipleFixtureModesWindowViewModel()
    {
        NumberOfFixtureModes = string.Empty;
        _numberOfFixtureModes = string.Empty;
        NumberOfFixtureModesState = this.WhenValueChanged(thisViewModel => thisViewModel.NumberOfFixtureModes)
            .Select(IsValidNumber);
        CancelMultipleAdds = ReactiveCommand.Create(CancelMultipleAddsCommand);
        GenerateFixtureModes = ReactiveCommand.CreateFromTask(GenerateMultipleFixtureModesAsyncCommand,
            IsValidInputValues());

        this.WhenActivated(disposables => { NumberOfFixtureModesState.Subscribe().DisposeWith(disposables); });
    }

    public string NumberOfFixtureModes
    {
        get => _numberOfFixtureModes;
        set => this.RaiseAndSetIfChanged(ref _numberOfFixtureModes, value);
    }

    public IObservable<IValidationState> NumberOfFixtureModesState { get; }

    public ReactiveCommand<Unit, FixtureMode[]?> GenerateFixtureModes { get; }

    public ReactiveCommand<Unit, FixtureMode[]?> CancelMultipleAdds { get; }

    private IValidationState IsValidNumber(string? input)
    {
        if (!int.TryParse(input, out _)) return new ValidationState(false, "Must be a valid number.");

        return new ValidationState(true, "Valid");
    }

    private FixtureMode[]? CancelMultipleAddsCommand()
    {
        return null;
    }

    private async Task<FixtureMode[]?> GenerateMultipleFixtureModesAsyncCommand()
    {
        if (!int.TryParse(NumberOfFixtureModes, out var numberOfModes))
            throw new InvalidOperationException("Number of fixture modes must be a number");

        var modes = new FixtureMode[numberOfModes];

        await Task.Run(() =>
        {
            for (var modeNumber = 0; modeNumber < numberOfModes; modeNumber++)
            {
                var label = $"Mode {modeNumber}";
                var guid = Guid.NewGuid();
                var mode = new FixtureMode(label, guid);

                modes[modeNumber] = mode;
            }
        });

        return modes;
    }

    private IObservable<bool> IsValidInputValues()
    {
        return this.WhenAnyValue<AddMultipleFixtureModesWindowViewModel, bool, string>(
            thisViewModel => thisViewModel.NumberOfFixtureModes,
            numberOfFixtureModes => IsValidNumber(numberOfFixtureModes).IsValid);
    }
}