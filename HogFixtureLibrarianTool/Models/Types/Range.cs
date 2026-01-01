namespace HogFixtureLibrarianTool.Models.Types;

[Serializable]
public record Range(
    string? DmxStart,
    string? FunctionName,
    string? FeatureName,
    string? Start = null,
    string? DmxEnd = null,
    string? End = null,
    string? DmxMidOne = null,
    string? DmxMidTwo = null,
    Qualifiers? Qualifiers = null,
    LinkedList<Escape>? Escapes = null,
    Function? Function = null,
    Feature? Feature = null)
    : IHogData, INoNamespaceData
{
    public Range() : this(null, null, null, string.Empty)
    {
    }

    public Range(int? dmxStart,
        string? functionName,
        string? featureName,
        int? dmxEnd = null,
        string? start = null,
        string? end = null,
        int? dmxMidOne = null,
        int? dmxMidTwo = null,
        Qualifiers? qualifiers = null,
        LinkedList<Escape>? Escapes = null,
        Function? function = null,
        Feature? feature = null) : this(dmxStart.ToString(), functionName, featureName, start,
        dmxEnd == null ? null : dmxEnd.ToString(), end, dmxMidOne == null ? null : dmxMidOne.ToString(),
        dmxMidTwo == null ? null : dmxMidTwo.ToString())
    {
    }

    public Range(int? dmxStart,
        string? functionName,
        string? featureName,
        int? dmxEnd = null,
        int? start = null,
        int? end = null,
        int? dmxMidOne = null,
        int? dmxMidTwo = null,
        Qualifiers? qualifiers = null,
        LinkedList<Escape>? Escapes = null,
        Function? function = null,
        Feature? feature = null) : this(dmxStart.ToString(), functionName, featureName,
        start == null ? null : start.ToString(),
        dmxEnd == null ? null : dmxEnd.ToString(), end == null ? null : end.ToString(),
        dmxMidOne == null ? null : dmxMidOne.ToString(), dmxMidTwo == null ? null : dmxMidTwo.ToString())
    {
    }

    [XmlAttribute(AttributeName = "dmxstart")]
    public string? DmxStart { get; set; } = DmxStart;

    [XmlIgnore] public string? DmxMidOne { get; set; } = DmxMidOne;

    [XmlIgnore] public string? DmxMidTwo { get; set; } = DmxMidTwo;

    [XmlAttribute(AttributeName = "dmxend")]
    public string? DmxEnd { get; set; } = DmxEnd;

    [XmlIgnore]
    public Function? Function
    {
        get;
        set
        {
            if (value is not null)
                FunctionName = value.Name;
        }
    } = Function;

    [XmlAttribute(AttributeName = "function")]
    public string? FunctionName { get; set; } = FunctionName;

    [XmlIgnore]
    public Feature? Feature
    {
        get;
        set
        {
            if (value is not null)
                FeatureName = value.Name;
        }
    } = Feature;

    [XmlAttribute(AttributeName = "feature")]
    public string? FeatureName { get; set; } = FeatureName;

    [XmlAttribute(AttributeName = "start")]
    public string? Start { get; set; } = Start;

    [XmlAttribute(AttributeName = "end")] public string? End { get; set; } = End;

    [XmlArray] public LinkedList<Escape>? Escapes { get; set; } = Escapes;

    [XmlAnyElement] public Qualifiers? Qualifiers { get; set; } = Qualifiers;
}