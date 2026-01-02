namespace HogFixtureLibrarianTool.Models.Types;

public class SettingsManager : ISettingsReaderWriter
{
    private const string SettingsFileName = "ApplicationSettings.cfg";

    private static readonly string ConfigDirectory = Path.Combine(AppContext.BaseDirectory, "Config");

    private static readonly string ConfigFilePath = Path.Combine(ConfigDirectory, SettingsFileName);

    private readonly IXmlReaderWriter _xmlManager;

    public SettingsManager()
    {
        if (!Directory.Exists(ConfigDirectory)) Directory.CreateDirectory(ConfigDirectory);

        _xmlManager = new XmlManager();
        ApplicationSettings = new Settings();

        _ = SaveSettingsAsync(ApplicationSettings);
    }

    public SettingsManager(IXmlReaderWriter xmlManager)
    {
        if (!Directory.Exists(ConfigDirectory)) Directory.CreateDirectory(ConfigDirectory);

        _xmlManager = xmlManager;
        ApplicationSettings = new Settings();

        _ = SaveSettingsAsync(ApplicationSettings);
    }

    public Settings ApplicationSettings { get; private set; }

    public async Task SaveSettingsAsync(Settings newSettings)
    {
        // in the event the directory does not get created 
        // ensure it gets created
        if (!Directory.Exists(ConfigDirectory)) Directory.CreateDirectory(ConfigDirectory);

        if (!File.Exists(ConfigFilePath)) await _xmlManager.WriteXmlToFileAsync(ConfigFilePath, ApplicationSettings);

        if (newSettings != ApplicationSettings)
        {
            // perhaps this could be considered 
            // a side effect
            ApplicationSettings = newSettings;

            await _xmlManager.WriteXmlToFileAsync(ConfigFilePath, ApplicationSettings);
        }
    }

    public async Task ReadSettingsAsync()
    {
        if (!Directory.Exists(ConfigDirectory)) Directory.CreateDirectory(ConfigDirectory);

        // after ensuring the creation of the directory
        // just write the current settings to the .cfg file
        if (!File.Exists(ConfigFilePath))
        {
            await _xmlManager.WriteXmlToFileAsync(ConfigFilePath,
                ApplicationSettings);
        }
        else
        {
            var savedSettings = await _xmlManager.ReadXmlFileAsync<Settings?>(ConfigFilePath);
            ApplicationSettings = savedSettings ?? ApplicationSettings;
        }
    }
}