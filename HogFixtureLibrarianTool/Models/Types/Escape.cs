using System.Runtime.CompilerServices;

namespace HogFixtureLibrarianTool.Models.Types;

[Serializable]
public record Escape(
    string FunctionName,
    string FeatureName,
    string Start,
    Function? Function = null,
    Feature? Feature = null) : INoNamespaceData, IHogData
{
    public Escape() : this(string.Empty, string.Empty, string.Empty, null, null)
    {
    }

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
    public string FunctionName { get; set; } = FunctionName;

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
    public string FeatureName { get; set; } = FeatureName;

    [XmlAttribute(AttributeName = "start")]
    public string Start { get; set; } = Start;

    public bool ShouldSerializeFunction()
    {
        return Function is not null;
    }

    public bool ShouldSerializeFeature()
    {
        return Feature is not null;
    }
}