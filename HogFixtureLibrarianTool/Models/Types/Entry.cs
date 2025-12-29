namespace HogFixtureLibrarianTool.Models.Types;

[Serializable]
public record Entry(string? Value, string? Name, Range Range) : IHogData, INoNamespaceData
{
    public Entry() : this(string.Empty, string.Empty, new Range())
    {
    }

    [XmlAttribute("value")] public string? Value { get; set; } = Value;

    [XmlAttribute("name")] public string? Name { get; set; } = Name;

    [XmlIgnore] public Range Range { get; } = Range;
}