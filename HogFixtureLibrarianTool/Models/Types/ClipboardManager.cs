using Avalonia.Input.Platform;

namespace HogFixtureLibrarianTool.Models.Types;

public class ClipboardManager : IClipboardService
{
    private readonly Application? _application = Application.Current;

    public async Task SetClipboardTextAsync(string? textToCopy)
    {
        if (_application == null ||
            _application.ApplicationLifetime == null)
            throw new ApplicationException("Application is null . . . abort!!!!!!!");

        if (_application.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
            desktop.MainWindow is not
            {
            } provider ||
            provider.Clipboard is null)
            throw new NullReferenceException("Missing the clipboard reference");

        await provider.Clipboard.SetTextAsync(textToCopy);
    }

    public async Task GetClipboardTextAsync()
    {
        if (_application == null ||
            _application.ApplicationLifetime == null)
            throw new ApplicationException("Application is null . . . abort!!!!!!!");

        if (_application.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
            desktop.MainWindow is not
            {
            } provider ||
            provider.Clipboard is null)
            throw new NullReferenceException("Missing the clipboard reference");

        await provider.Clipboard.TryGetTextAsync();
    }
}