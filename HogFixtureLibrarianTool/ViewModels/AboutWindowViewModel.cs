using Avalonia.Platform;

namespace HogFixtureLibrarianTool.ViewModels;

public class AboutWindowViewModel : ValidatableViewModelBase
{
    public AboutWindowViewModel()
    {
        using var resources = AssetLoader.Open(new Uri(@"avares://HogFixtureLibrarianTool/Assets/Authors.txt"));
        using var reader = new StreamReader(resources);
        Authors = reader.ReadToEnd();
    }

    public string SoftwareName { get; } = Application.Current is null || Application.Current.Name is null
        ? "Unknown Application"
        : Application.Current.Name;

    public string SoftwareVersion { get; } = Assembly.GetExecutingAssembly().GetName().Version is null
        ? "Unknown Version"
        : Assembly.GetExecutingAssembly().GetName().Version!.ToString();

    public string Authors { get; }
}