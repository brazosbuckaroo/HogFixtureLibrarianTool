namespace HogFixtureLibrarianTool.ViewModels;

public class MainWindowViewModel : WindowViewModelBase
{
    private readonly ISettingsReaderWriter _settingsManager;

    public MainWindowViewModel()
    {
        _settingsManager = new SettingsManager();
        ApplicationPreferencesInteraction = new Interaction<ApplicationPreferencesWindowViewModel, Settings?>();
        AboutInteraction = new Interaction<AboutWindowViewModel, Unit>();
        DmxValueConverterViewModel = new DmxValueConverterViewModel(this);
        GuidGeneratorViewModel = new GuidGeneratorViewModel(this);
        StringmapEditorViewModel = new StringmapEditorViewModel(this);
        GoToDmxValueConverter = ReactiveCommand.CreateFromTask(GoToDmxValueConverterAsyncCommand);
        GoToGuidGenerator = ReactiveCommand.CreateFromTask(GoToGuidGeneratorAsyncCommand);
        GoToStringMapEditor = ReactiveCommand.CreateFromTask(GoToStringMapEditorAsyncCommand);
        OpenPreferences = ReactiveCommand.CreateFromTask(OpenPreferencesDialogAsyncCommand);
        OpenAbout = ReactiveCommand.CreateFromTask(OpenAboutDialogAsyncCommand);

        Router.Navigate.Execute(DmxValueConverterViewModel);
    }

    public MainWindowViewModel(IDmxValidator dmxValidator,
        IClipboardService clipboardManager,
        IDbManager sqliteManager,
        IXmlReaderWriter xmlManager,
        ISettingsReaderWriter settingManager,
        IConvertor conversionManager)
    {
        _settingsManager = settingManager;
        ApplicationPreferencesInteraction = new Interaction<ApplicationPreferencesWindowViewModel, Settings?>();
        AboutInteraction = new Interaction<AboutWindowViewModel, Unit>();
        DmxValueConverterViewModel = new DmxValueConverterViewModel(this, dmxValidator, sqliteManager);
        GuidGeneratorViewModel = new GuidGeneratorViewModel(this, clipboardManager);
        StringmapEditorViewModel = new StringmapEditorViewModel(this, sqliteManager,
            dmxValidator, xmlManager,
            clipboardManager, settingManager,
            conversionManager);
        GoToDmxValueConverter = ReactiveCommand.CreateFromTask(GoToDmxValueConverterAsyncCommand);
        GoToGuidGenerator = ReactiveCommand.CreateFromTask(GoToGuidGeneratorAsyncCommand);
        GoToStringMapEditor = ReactiveCommand.CreateFromTask(GoToStringMapEditorAsyncCommand);
        OpenPreferences = ReactiveCommand.CreateFromTask(OpenPreferencesDialogAsyncCommand);
        OpenAbout = ReactiveCommand.CreateFromTask(OpenAboutDialogAsyncCommand);

        Router.Navigate.Execute(DmxValueConverterViewModel);
    }

    public DmxValueConverterViewModel DmxValueConverterViewModel { get; }

    public GuidGeneratorViewModel GuidGeneratorViewModel { get; }

    public StringmapEditorViewModel StringmapEditorViewModel { get; }

    public Interaction<ApplicationPreferencesWindowViewModel, Settings?> ApplicationPreferencesInteraction { get; }

    public Interaction<AboutWindowViewModel, Unit> AboutInteraction { get; }

    public ReactiveCommand<Unit, Unit> GoToDmxValueConverter { get; }

    public ReactiveCommand<Unit, Unit> GoToGuidGenerator { get; }

    public ReactiveCommand<Unit, Unit> GoToStringMapEditor { get; }

    public ReactiveCommand<Unit, Unit> OpenPreferences { get; }

    public ReactiveCommand<Unit, Unit> OpenAbout { get; }

    private async Task GoToDmxValueConverterAsyncCommand()
    {
        await Router.Navigate.Execute(DmxValueConverterViewModel);
    }

    private async Task GoToGuidGeneratorAsyncCommand()
    {
        await Router.Navigate.Execute(GuidGeneratorViewModel);
    }

    private async Task GoToStringMapEditorAsyncCommand()
    {
        await Router.Navigate.Execute(StringmapEditorViewModel);
    }

    private async Task OpenPreferencesDialogAsyncCommand()
    {
        var dialog = new ApplicationPreferencesWindowViewModel(_settingsManager);
        var newSettings = await ApplicationPreferencesInteraction.Handle(dialog);

        if (newSettings != null) await _settingsManager.SaveSettingsAsync(newSettings);
    }

    private async Task OpenAboutDialogAsyncCommand()
    {
        var dialog = new AboutWindowViewModel();

        await AboutInteraction.Handle(dialog);
    }
}