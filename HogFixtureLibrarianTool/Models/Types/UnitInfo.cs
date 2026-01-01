namespace HogFixtureLibrarianTool.Models.Types;

public record UnitInfo(int Id, string Name, int? MinVal, int? MaxVal) : IHogData, INoNamespaceData
{
    [XmlAttribute(AttributeName = "id")] public int Id { get; set; } = Id;

    [XmlAttribute(AttributeName = "name")] public string Name { get; set; } = Name;

    [XmlAttribute(AttributeName = "min")] public int? MinVal { get; set; } = MinVal;

    [XmlAttribute(AttributeName = "max")] public int? MaxVal { get; set; } = MaxVal;
}