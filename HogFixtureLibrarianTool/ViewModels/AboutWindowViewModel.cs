namespace HogFixtureLibrarianTool.ViewModels;

public class AboutWindowViewModel : ValidatableViewModelBase
{
    public AboutWindowViewModel()
    {
        using var resources = AssetLoader.Open(new Uri(@"avares://HogFixtureLibrarianTool/Assets/Authors.txt"));
        using var reader = new StreamReader(resources);
        Authors = reader.ReadToEnd();
    }

    public string SoftwareName { get; } = GlobalValues.Name;

    public string SoftwareVersion { get; } = GlobalValues.Version;

    public string Authors { get; }
}