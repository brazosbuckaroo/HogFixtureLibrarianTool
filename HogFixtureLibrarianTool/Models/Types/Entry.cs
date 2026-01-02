namespace HogFixtureLibrarianTool.Models.Types;

[Serializable]
public record Entry(string? Value, string? Name, HogRange Range) : IHogData, INoNamespaceData
{
    public Entry() : this(string.Empty, string.Empty, new HogRange())
    {
    }

    [XmlAttribute("value")] public string? Value { get; set; } = Value;

    [XmlAttribute("name")] public string? Name { get; set; } = Name;

    [XmlIgnore] public HogRange Range { get; } = Range;
}