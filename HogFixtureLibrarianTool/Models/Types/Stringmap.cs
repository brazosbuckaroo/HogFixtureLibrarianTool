namespace HogFixtureLibrarianTool.Models.Types;

[Serializable]
public record Stringmap : IHogData, INoNamespaceData
{
    public Stringmap()
    {
        Previews = [];
    }

    public Stringmap(List<FunctionPreview> previews)
    {
        Previews = previews;
    }

    public Stringmap(FunctionPreview preview)
    {
        Previews =
        [
            preview
        ];
    }

    [XmlElement("Funcpreview")] public List<FunctionPreview> Previews { get; }
}