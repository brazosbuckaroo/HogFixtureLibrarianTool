namespace HogFixtureLibrarianTool.Models.Types;

[Serializable]
public record Feature(
    string Name,
    bool HideName,
    string DefaultUnitVal,
    string Path,
    string? UnitName,
    string? ControlDataQualifier,
    string? Description,
    UnitInfo? Unit) : IHogData, INoNamespaceData
{
    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; } = Name;
    
    [XmlAttribute(AttributeName = "hideName")]
    public bool HideName { get; set; } = HideName;
    
    [XmlAttribute(AttributeName = "defaultUnitVal")]
    public string DefaultUnitVal { get; set; } = DefaultUnitVal;

    [XmlIgnore]
    public UnitInfo? Unit
    {
        get;
        set
        { 
            if (value is not null)
                UnitName = value.Name;     
        }
    } = Unit;
    
    [XmlAttribute(AttributeName = "unit")]
    public string? UnitName { get; set; } = UnitName;
    
    [XmlAttribute(AttributeName = "path")]
    public string Path { get; set; } = Path;
    
    [XmlAttribute(AttributeName = "controlDataQualifier")]
    public string? ControlDataQualifier { get; set; } = ControlDataQualifier;
    
    [XmlAttribute(AttributeName = "description")]
    public string? Description { get; set; } = Description;
}