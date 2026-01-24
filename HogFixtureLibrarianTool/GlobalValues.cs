namespace HogFixtureLibrarianTool;

public static class GlobalValues
{
    public static string Name { get; } = Assembly.GetExecutingAssembly()
        .GetCustomAttributes(typeof(AssemblyProductAttribute), false)
        .OfType<AssemblyProductAttribute>().First().Product;

    public static string Version { get; } = Assembly.GetExecutingAssembly().GetName().Version is null
        ? "Unknown Version"
        : Assembly.GetExecutingAssembly().GetName().Version!.ToString();
}