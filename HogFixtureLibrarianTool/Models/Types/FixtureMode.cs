namespace HogFixtureLibrarianTool.Models.Types;

public record FixtureMode(string Name, Guid Guid)
{
    public FixtureMode() : this(string.Empty, Guid.Empty)
    {
    }

    public string Name { get; } = Name;

    public Guid Guid { get; } = Guid;
}