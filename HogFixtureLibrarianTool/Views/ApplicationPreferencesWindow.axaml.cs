namespace HogFixtureLibrarianTool.Views;

public partial class ApplicationPreferencesWindow : ReactiveWindow<ApplicationPreferencesWindowViewModel>
{
    public ApplicationPreferencesWindow()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            ViewModel!.ApplySettings.Subscribe(Close)
                .DisposeWith(disposables);
            ViewModel!.CancelChanges.Subscribe(Close)
                .DisposeWith(disposables);
        });
    }
}