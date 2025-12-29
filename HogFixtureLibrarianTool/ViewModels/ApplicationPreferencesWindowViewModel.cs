namespace HogFixtureLibrarianTool.ViewModels;

public class ApplicationPreferencesWindowViewModel : ValidatableViewModelBase
{
    private readonly ISettingsReaderWriter _settingsManager;

    private bool _useHogV3;

    public ApplicationPreferencesWindowViewModel()
    {
        _settingsManager = new SettingsManager();
        _useHogV3 = _settingsManager.ApplicationSettings.UseHogV3Xml; // use default settings
        CancelChanges = ReactiveCommand.Create(CancelChangesCommand);
        ApplySettings = ReactiveCommand.Create(ApplySettingsCommand);
        SaveSettings = ReactiveCommand.CreateFromTask(SaveSettingsAsyncCommand);
    }

    public ApplicationPreferencesWindowViewModel(ISettingsReaderWriter settingsManager)
    {
        _settingsManager = settingsManager;
        _useHogV3 = _settingsManager.ApplicationSettings.UseHogV3Xml; // use default settings
        CancelChanges = ReactiveCommand.Create(CancelChangesCommand);
        ApplySettings = ReactiveCommand.Create(ApplySettingsCommand);
        SaveSettings = ReactiveCommand.CreateFromTask(SaveSettingsAsyncCommand);
    }

    public bool UseHogV3
    {
        get => _useHogV3;
        set => this.RaiseAndSetIfChanged(ref _useHogV3, value);
    }

    public ReactiveCommand<Unit, Unit> SaveSettings { get; }

    public ReactiveCommand<Unit, Settings> ApplySettings { get; }

    public ReactiveCommand<Unit, Settings?> CancelChanges { get; }

    private Settings? CancelChangesCommand()
    {
        return null;
    }

    private Settings ApplySettingsCommand()
    {
        var settings = new Settings(UseHogV3);

        return settings;
    }

    private async Task SaveSettingsAsyncCommand()
    {
        await _settingsManager.SaveSettingsAsync(new Settings(UseHogV3));
    }
}