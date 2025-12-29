namespace HogFixtureLibrarianTool.Models.Types;

public class AppViewLocator : IViewLocator
{
    public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
    {
        return viewModel switch
        {
            DmxValueConverterViewModel context => new DmxValueConverterView { DataContext = context },
            GuidGeneratorViewModel context => new GuidGeneratorView { DataContext = context },
            StringmapEditorViewModel context => new StringmapEditorView { DataContext = context },
            _ => throw new InvalidOperationException()
        };
    }
}