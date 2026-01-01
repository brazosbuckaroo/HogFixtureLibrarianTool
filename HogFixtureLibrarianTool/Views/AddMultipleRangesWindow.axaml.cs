namespace HogFixtureLibrarianTool.Views;

public partial class AddMultipleRangesWindow : ReactiveWindow<AddMultipleRangesWindowViewModel>
{
    public AddMultipleRangesWindow()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            ViewModel!.NumberOfRangesState.Subscribe(UpdateRangesInputTextBox)
                .DisposeWith(disposables);
            ViewModel!.DmxStartValueState.Subscribe(UpdateDmxInputTextBox)
                .DisposeWith(disposables);
            ViewModel!.DmxOffsetState.Subscribe(UpdateDmxOffsetInputTextBox)
                .DisposeWith(disposables);
            ViewModel!.FunctionInputState.Subscribe(UpdateFunctionComboBox)
                .DisposeWith(disposables);
            ViewModel!.FeatureInputState.Subscribe(UpdateFeatureComboBox)
                .DisposeWith(disposables);
            ViewModel!.CancelMultipleAdds.Subscribe(Close)
                .DisposeWith(disposables);
            ViewModel!.GenerateMultipleAdds.Subscribe(Close)
                .DisposeWith(disposables);
        });
    }

    private void UpdateRangesInputTextBox(IValidationState inputState)
    {
        NumberOfRangesInput.Classes.Clear();

        NumberOfRangesToolTip.Text = inputState.Text.ToSingleLine();

        NumberOfRangesInput.Classes.Set(inputState.IsValid ? "Valid" : "Error", true);
    }

    private void UpdateDmxInputTextBox(IValidationState inputState)
    {
        EightBitDmxValueInput.Classes.Clear();

        EightBitDmxValueToolTip.Text = inputState.Text.ToSingleLine();

        EightBitDmxValueInput.Classes.Set(inputState.IsValid ? "Valid" : "Error", true);
    }

    private void UpdateDmxOffsetInputTextBox(IValidationState inputState)
    {
        DmxOffsetInput.Classes.Clear();

        DmxOffsetToolTip.Text = inputState.Text.ToSingleLine();

        DmxOffsetInput.Classes.Set(inputState.IsValid ? "Valid" : "Error", true);
    }

    private void UpdateFunctionComboBox(IValidationState inputState)
    {
        FunctionInput.Classes.Clear();

        FunctionInputToolTip.Text = inputState.Text.ToSingleLine();

        FunctionInput.Classes.Set(inputState.IsValid ? "Valid" : "Error", true);
    }

    private void UpdateFeatureComboBox(IValidationState inputState)
    {
        FeatureInput.Classes.Clear();

        FeatureInputToolTip.Text = inputState.Text.ToSingleLine();

        FeatureInput.Classes.Set(inputState.IsValid ? "Valid" : "Error", true);
    }
}