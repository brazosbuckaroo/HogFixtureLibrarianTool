namespace HogFixtureLibrarianTool;

public static class GlobalValues
{
    public static string Name { get; } = Assembly.GetExecutingAssembly()
        .GetCustomAttributes(typeof(AssemblyProductAttribute), false)
        .OfType<AssemblyProductAttribute>().First().Product;
}