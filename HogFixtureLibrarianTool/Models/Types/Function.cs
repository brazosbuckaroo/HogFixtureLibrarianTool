namespace HogFixtureLibrarianTool.Models.Types;

public record Function(
    string Name,
    string Kind,
    int Id,
    string Family,
    string SubName,
    string Group,
    string GroupBuddy,
    List<Feature> Features,
    List<Function>? Mutexes) : IHogData, INoNamespaceData
{
    [XmlAttribute(AttributeName = "kind")] public string Kind { get; set; } = Kind;

    [XmlAttribute(AttributeName = "id")] public int Id { get; set; } = Id;

    [XmlAttribute(AttributeName = "family")]
    public string Family { get; set; } = Family;

    [XmlAttribute(AttributeName = "subname")]
    public string SubName { get; set; } = SubName;

    [XmlAttribute(AttributeName = "group")]
    public string Group { get; set; } = Group;

    [XmlAttribute(AttributeName = "groupbuddy")]
    public string GroupBuddy { get; set; } = GroupBuddy;

    [XmlArray] public List<Feature> Features { get; set; } = Features;

    [XmlArray(ElementName = "Mutex")] public List<Function>? Mutexes { get; set; } = Mutexes;
}