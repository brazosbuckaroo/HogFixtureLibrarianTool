namespace HogFixtureLibrarianTool.Models.Types;

[Serializable]
public record FunctionPreview(Definition Definition, List<Entry> Entries) : IHogData, INoNamespaceData
{
    public FunctionPreview() : this(new Definition(), new List<Entry>())
    {
    }

    public Definition Definition { get; set; } = Definition;

    [XmlElement("Entry")] public List<Entry> Entries { get; set; } = Entries;
}