namespace HogFixtureLibrarianTool.Models.Types;

public class SettingsManager : ISettingsReaderWriter
{
    private static readonly string _settingsFileName = "ApplicationSettings.cfg";

    private static readonly string _configDirectory = Path.Combine(AppContext.BaseDirectory, "Config");

    private static readonly string _configFilePath = Path.Combine(_configDirectory, _settingsFileName);

    private readonly IXmlReaderWriter _xmlManager;

    public SettingsManager()
    {
        if (!Directory.Exists(_configDirectory)) Directory.CreateDirectory(_configDirectory);

        _xmlManager = new XmlManager();
        ApplicationSettings = new Settings();

        _ = SaveSettingsAsync(ApplicationSettings);
    }

    public SettingsManager(IXmlReaderWriter xmlManager)
    {
        if (!Directory.Exists(_configDirectory)) Directory.CreateDirectory(_configDirectory);

        _xmlManager = xmlManager;
        ApplicationSettings = new Settings();

        _ = SaveSettingsAsync(ApplicationSettings);
    }

    public Settings ApplicationSettings { get; protected set; }

    public async Task SaveSettingsAsync(Settings newSettings)
    {
        // in the event the directory does not get created 
        // ensure it gets created
        if (!Directory.Exists(_configDirectory)) Directory.CreateDirectory(_configDirectory);

        if (!File.Exists(_configFilePath)) await _xmlManager.WriteXmlToFileAsync(_configFilePath, ApplicationSettings);

        if (newSettings != ApplicationSettings)
        {
            // perhaps this could be considered 
            // a side effect
            ApplicationSettings = newSettings;

            await _xmlManager.WriteXmlToFileAsync(_configFilePath, ApplicationSettings);
        }
    }

    public async Task ReadSettingsAsync()
    {
        if (!Directory.Exists(_configDirectory)) Directory.CreateDirectory(_configDirectory);

        // after ensuring the creation of the directory
        // just write the current settings to the .cfg file
        if (!File.Exists(_configFilePath))
        {
            await _xmlManager.WriteXmlToFileAsync(_configFilePath,
                ApplicationSettings);
        }
        else
        {
            var savedSettings = await _xmlManager.ReadXmlFileAsync<Settings?>(_configFilePath);
            ApplicationSettings = savedSettings ?? ApplicationSettings;
        }
    }
}