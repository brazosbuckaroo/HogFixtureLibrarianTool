namespace HogFixtureLibrarianTool.Models.Types;

public class XmlManager : IXmlReaderWriter
{
    private readonly XmlSerializerNamespaces _emptyNamespace;
    private readonly XmlReaderSettings _settings;

    public XmlManager()
    {
        _settings = new XmlReaderSettings
        {
            Async = true
        };
        _emptyNamespace = new XmlSerializerNamespaces();
        _emptyNamespace.Add(string.Empty, string.Empty);
    }

    public async Task<string> SerializeDataAsync(INoNamespaceData data)
    {
        var xmlSerializer = new XmlSerializer(data.GetType());
        var xmlString = string.Empty;

        await using var stream = new MemoryStream();
        xmlSerializer.Serialize(stream, data, _emptyNamespace);

        stream.Position = 0; // resetting to read XML

        using var reader = XmlReader.Create(stream, _settings);
        
        while (await reader.ReadAsync()) xmlString = await reader.ReadOuterXmlAsync();

        return xmlString;
    }

    public async Task WriteXmlToFileAsync(string filePath, object data)
    {
        var xmlSerializer = new XmlSerializer(data.GetType());

        try
        {
            await using var stream = File.Create(filePath);
            xmlSerializer.Serialize(stream, data);
        }
        catch (InvalidOperationException)
        {
            // TODO: Figure out why I caught this exception and
            //       determine what I want to do with this information.
            //          -DTL
            // Side note:
            //      Document your shit bro!
        }
    }

    public async Task<T?> ReadXmlFileAsync<T>(string filePath)
    {
        var xmlSerializer = new XmlSerializer(typeof(T));
        T? data = default;

        try
        {
            await using var stream = File.Open(filePath, FileMode.Open);
            data = (T?)xmlSerializer.Deserialize(stream);
        }
        catch (XmlException)
        {
            return default;
        }

        return data;
    }
}