namespace HogFixtureLibrarianTool.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();

        Title = GlobalValues.Name;

        this.WhenActivated(disposables =>
        {
            ViewModel!.DmxValueConverterViewModel.AddingMultipleRangesInteraction
                .RegisterHandler(DoShowAddMultipleRanges)
                .DisposeWith(disposables);
            ViewModel!.GuidGeneratorViewModel.AddingMultipleFixtureModesInteraction
                .RegisterHandler(DoShowAddMultupleFixtureModes)
                .DisposeWith(disposables);
            ViewModel!.StringmapEditorViewModel.AddMultipleRangesInteraction
                .RegisterHandler(DoShowAddMultipleRanges)
                .DisposeWith(disposables);
            ViewModel!.ApplicationPreferencesInteraction
                .RegisterHandler(DoShowPreferences)
                .DisposeWith(disposables);
            ViewModel!.AboutInteraction
                .RegisterHandler(DoShowAbout)
                .DisposeWith(disposables);
        });
    }

    private async Task DoShowAddMultipleRanges(
        IInteractionContext<AddMultipleRangesWindowViewModel, HogRange[]?> context)
    {
        var dialog = new AddMultipleRangesWindow();
        dialog.DataContext = context.Input;

        context.SetOutput(await dialog.ShowDialog<HogRange[]?>(this));
    }

    private async Task DoShowAddMultupleFixtureModes(
        IInteractionContext<AddMultipleFixtureModesWindowViewModel, FixtureMode[]?> context)
    {
        var dialog = new AddMultipleFixtureModesWindow();
        dialog.DataContext = context.Input;

        context.SetOutput(await dialog.ShowDialog<FixtureMode[]?>(this));
    }

    private async Task DoShowPreferences(IInteractionContext<ApplicationPreferencesWindowViewModel, Settings?> context)
    {
        var newDialog = new ApplicationPreferencesWindow();
        newDialog.DataContext = context.Input;

        if (!OwnedWindows.ToList().Exists(ownedDialog => ownedDialog.GetType() == newDialog.GetType()))
            context.SetOutput(await newDialog.ShowDialog<Settings?>(this));
        else
            context.SetOutput(null);
    }

    private async Task DoShowAbout(IInteractionContext<AboutWindowViewModel, Unit> context)
    {
        var newDialog = new AboutWindow();
        newDialog.DataContext = context.Input;

        context.SetOutput(await newDialog.ShowDialog<Unit>(this));
    }
}