namespace HogFixtureLibrarianTool.Models.Types;

[Serializable]
public record Qualifiers(string Match, LinkedList<Qualifier> QualifierList) : INoNamespaceData, IHogData
{
    public Qualifiers() : this(string.Empty, new LinkedList<Qualifier>())
    {
    }

    [XmlAttribute(AttributeName = "match")]
    public string Match { get; set; } = Match;

    [XmlArray(ElementName = "Qualifiers")] public LinkedList<Qualifier> QualifierList { get; set; } = QualifierList;
}