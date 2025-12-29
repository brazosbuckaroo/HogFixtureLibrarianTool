namespace HogFixtureLibrarianTool.Models.Types;

[Serializable]
public record Range(
    string? DmxStart,
    string? Function,
    string? Feature,
    string? Start,
    string? DmxEnd = null,
    string? End = null,
    string? DmxMidOne = null,
    string? DmxMidTwo = null)
    : IHogData, INoNamespaceData
{
    public Range() : this(null, null, null, string.Empty)
    {
    }

    public Range(int? dmxStart,
        string? function,
        string? feature,
        int? dmxEnd = null,
        string? start = null,
        string? end = null,
        int? dmxMidOne = null,
        int? dmxMidTwo = null) : this(dmxStart.ToString(), function, feature, start,
        dmxEnd == null ? null : dmxEnd.ToString(), end, dmxMidOne == null ? null : dmxMidOne.ToString(),
        dmxMidTwo == null ? null : dmxMidTwo.ToString())
    {
    }

    public Range(int? dmxStart,
        string? function,
        string? feature,
        int? dmxEnd = null,
        int? start = null,
        int? end = null,
        int? dmxMidOne = null,
        int? dmxMidTwo = null) : this(dmxStart.ToString(), function, feature, start == null ? null : start.ToString(),
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

    [XmlAttribute("function")] public string? Function { get; set; } = Function;

    [XmlAttribute("feature")] public string? Feature { get; set; } = Feature;

    [XmlAttribute(AttributeName = "start")]
    public string? Start { get; set; } = Start;

    [XmlAttribute(AttributeName = "end")] public string? End { get; set; } = End;
}