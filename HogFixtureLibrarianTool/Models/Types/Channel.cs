namespace HogFixtureLibrarianTool.Models.Types;

[Serializable]
public record Channel(
    int Number,
    List<HogRange> Ranges,
    int Constant = -1,
    bool Unused = false)
    : IHogData, INoNamespaceData
{
    public Channel() : this(default, [])
    {
    }

    [XmlAttribute(AttributeName = "number", DataType = "int")]
    public int Number { get; set; } = Number;

    [XmlAttribute(AttributeName = "constant", DataType = "int")]
    public int Constant { get; set; } = Constant;

    [XmlAttribute(AttributeName = "unused", DataType = "boolean")]
    public bool Unused { get; set; } = Unused;

    [XmlArray] public List<HogRange> Ranges { get; set; } = Ranges;

    public bool ShouldSerializeConstant()
    {
        return Constant >= 0;
    }

    public bool ShouldSerializeUnused()
    {
        return Unused;
    }
}