namespace HogFixtureLibrarianTool.Models.Types;

[Serializable]
[XmlRoot(ElementName = "Qualifiers")]
public record Qualifiers(string Match, List<Qualifier> QualifierList) : INoNamespaceData, IHogData
{
    public Qualifiers() : this(string.Empty, new List<Qualifier>())
    {
    }

    [XmlAttribute(AttributeName = "match")]
    public string Match { get; set; } = Match;

    [XmlArray(ElementName = "Qualifiers")] public List<Qualifier> QualifierList { get; set; } = QualifierList;
}