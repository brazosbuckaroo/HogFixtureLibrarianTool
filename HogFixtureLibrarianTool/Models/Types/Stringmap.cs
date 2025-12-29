namespace HogFixtureLibrarianTool.Models.Types;

[Serializable]
public record Stringmap : IHogData, INoNamespaceData
{
    public Stringmap()
    {
        Previews = new List<FunctionPreview>();
    }

    public Stringmap(List<FunctionPreview> previews)
    {
        Previews = previews;
    }

    public Stringmap(FunctionPreview preview)
    {
        Previews = new List<FunctionPreview>();

        Previews.Add(preview);
    }

    [XmlElement("Funcpreview")] public List<FunctionPreview> Previews { get; }
}