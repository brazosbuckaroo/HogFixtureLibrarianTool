namespace HogFixtureLibrarianTool.Views;

public partial class GuidGeneratorView : ReactiveUserControl<GuidGeneratorViewModel>
{
    public GuidGeneratorView()
    {
        InitializeComponent();

        this.WhenActivated(disposables => { });
    }
}