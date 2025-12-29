namespace HogFixtureLibrarianTool.Models.Types;

[Serializable]
public record Definition(string? Name, FunctionReference FunctionReference) : IHogData, INoNamespaceData
{
    public Definition() : this(string.Empty, new FunctionReference())
    {
    }

    [XmlAttribute("name")] public string? Name { get; set; } = Name;

    [XmlElement("Funcref")] public FunctionReference FunctionReference { get; set; } = FunctionReference;
}