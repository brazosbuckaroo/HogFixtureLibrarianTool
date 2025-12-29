namespace HogFixtureLibrarianTool.Views;

public partial class AddMultipleFixtureModesWindow : ReactiveWindow<AddMultipleFixtureModesWindowViewModel>
{
    public AddMultipleFixtureModesWindow()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            ViewModel!.NumberOfFixtureModesState.Subscribe(UpdateNumberOfFixtureModesTextBox)
                .DisposeWith(disposables);
            ViewModel!.CancelMultipleAdds.Subscribe(Close)
                .DisposeWith(disposables);
            ViewModel!.GenerateFixtureModes.Subscribe(Close)
                .DisposeWith(disposables);
        });
    }

    private void UpdateNumberOfFixtureModesTextBox(IValidationState inputState)
    {
        NumberOfFixtureModesInput.Classes.Clear();

        NumberOfFixtureModesInputToolTip.Text = inputState.Text.ToSingleLine();

        if (inputState.IsValid)
            NumberOfFixtureModesInput.Classes.Set("Valid", true);
        else
            NumberOfFixtureModesInput.Classes.Set("Error", true);
    }
}