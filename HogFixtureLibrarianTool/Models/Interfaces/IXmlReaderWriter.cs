namespace HogFixtureLibrarianTool.Models.Interfaces;

public interface IXmlReaderWriter
{
    Task<string> SerializeDataAsync(INoNamespaceData data);

    Task WriteXmlToFileAsync(string filePath, object data);

    Task<T?> ReadXmlFileAsync<T>(string filePath);
}