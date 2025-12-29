namespace HogFixtureLibrarianTool.Models.Interfaces;

public interface ISettingsReaderWriter
{
    Settings ApplicationSettings { get; }

    Task SaveSettingsAsync(Settings newSettings);

    Task ReadSettingsAsync();
}