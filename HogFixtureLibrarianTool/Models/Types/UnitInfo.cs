namespace HogFixtureLibrarianTool.Models.Types;

[JsonSerializable(typeof(UnitInfo))]
public record UnitInfo(int Id, string Name, int? MinVal, int? MaxVal) : IHogData, INoNamespaceData
{
    [JsonInclude] public int Id { get; set; } = Id;

    [JsonInclude] public string Name { get; set; } = Name;

    [JsonInclude] public int? MinVal { get; set; } = MinVal;

    [JsonInclude] public int? MaxVal { get; set; } = MaxVal;
}