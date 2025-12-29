namespace HogFixtureLibrarianTool;

public class App : Application
{
    private ClipboardManager? _clipboardManager;

    private ConversionManager? _conversionManager;

    private HogDmxValidator? _dmxValidator;

    private MainWindowViewModel? _mainWindowViewModel;

    private SettingsManager? _settingsManager;

    private GlobalShortcutManager? _shortcutManager;

    private SqlLiteManager? _sqlLiteManager;

    private XmlManager? _xmlManager;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        Name = GlobalValues.Name;
        _dmxValidator = new HogDmxValidator();
        _clipboardManager = new ClipboardManager();
        _sqlLiteManager = new SqlLiteManager();
        _xmlManager = new XmlManager();
        _settingsManager = new SettingsManager(_xmlManager);
        _conversionManager = new ConversionManager();
        _mainWindowViewModel = new MainWindowViewModel(_dmxValidator,
            _clipboardManager,
            _sqlLiteManager,
            _xmlManager,
            _settingsManager,
            _conversionManager);
        _shortcutManager = new GlobalShortcutManager(_mainWindowViewModel);

        await _settingsManager.ReadSettingsAsync();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = _mainWindowViewModel
            };

            desktop.ShutdownRequested += desktop_ShutdownRequested;

            await _shortcutManager.StartGlobalShortcutHookAsync();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void desktop_ShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
    {
        if (!e.Cancel && _shortcutManager != null) _shortcutManager.StopGlobalShortcutHook();
    }
}