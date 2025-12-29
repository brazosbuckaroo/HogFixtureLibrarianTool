namespace HogFixtureLibrarianTool.Models.Types;

[Serializable]
public record PreviewPackages(List<Package> Packages) : IHogData, INoNamespaceData
{
    public PreviewPackages() : this([])
    {
    }

    [XmlElement] public List<Package> Packages { get; set; } = Packages;
}