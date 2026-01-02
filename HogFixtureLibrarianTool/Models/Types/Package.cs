namespace HogFixtureLibrarianTool.Models.Types;

[Serializable]
public record Package(string? Name, List<FunctionPreview> Previews) : IHogData, INoNamespaceData
{
    public Package() : this(string.Empty, [])
    {
    }

    [XmlAttribute(AttributeName = "name")] public string? Name { get; set; } = Name;

    [XmlElement("Funcpreview")] public List<FunctionPreview> Previews { get; set; } = Previews;
}