namespace HogFixtureLibrarianTool.Models.Types;

[Serializable]
public record FunctionReference(string? Function, string? Feature) : IHogData, INoNamespaceData
{
    public FunctionReference() : this(null, null)
    {
    }

    [XmlAttribute("function")] public string? Function { get; set; } = Function;

    [XmlAttribute("feature")] public string? Feature { get; set; } = Feature;
}