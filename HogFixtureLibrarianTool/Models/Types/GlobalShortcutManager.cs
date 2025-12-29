namespace HogFixtureLibrarianTool.Models.Types;

public class GlobalShortcutManager
{
    private readonly MainWindowViewModel _mainWindowViewModel;

    private readonly IGlobalHook _shortcutHook;

    public GlobalShortcutManager()
    {
        _mainWindowViewModel = new MainWindowViewModel();
        _shortcutHook = new TaskPoolGlobalHook();

        _shortcutHook.KeyPressed += _shortcutHook_KeyPressed;
    }

    public GlobalShortcutManager(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _shortcutHook = new TaskPoolGlobalHook();

        _shortcutHook.KeyPressed += _shortcutHook_KeyPressed;
    }

    public async Task StartGlobalShortcutHookAsync()
    {
        await _shortcutHook.RunAsync();
    }

    public void StopGlobalShortcutHook()
    {
        _shortcutHook.Stop();
        _shortcutHook.Dispose();
    }

    private void _shortcutHook_KeyPressed(object? sender, KeyboardHookEventArgs e)
    {
        switch (e.Data.KeyCode)
        {
            case KeyCode.VcF1:
                Dispatcher.UIThread.Post(() => _mainWindowViewModel.GoToDmxValueConverter.Execute());

                break;
            case KeyCode.VcF2:
                Dispatcher.UIThread.Post(() => _mainWindowViewModel.GoToGuidGenerator.Execute());

                break;
            case KeyCode.VcF3:
                Dispatcher.UIThread.Post(() => _mainWindowViewModel.GoToStringMapEditor.Execute());

                break;
            default:
                if (e.RawEvent.Mask.HasAlt())
                    if (e.Data.KeyCode == KeyCode.VcP)
                        Dispatcher.UIThread.Post(() => _mainWindowViewModel.OpenPreferences.Execute());

                break;
        }
    }
}