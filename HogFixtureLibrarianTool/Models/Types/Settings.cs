namespace HogFixtureLibrarianTool.Models.Types;

[Serializable]
public record Settings(bool UseHogV3Xml) : IApplicationData, INoNamespaceData
{
    public Settings() : this(false)
    {
    }

    [XmlElement(DataType = "boolean")] public bool UseHogV3Xml { get; set; } = UseHogV3Xml;
}