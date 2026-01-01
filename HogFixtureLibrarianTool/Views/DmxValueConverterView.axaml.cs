namespace HogFixtureLibrarianTool.Views;

public partial class DmxValueConverterView : ReactiveUserControl<DmxValueConverterViewModel>
{
    public DmxValueConverterView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            ViewModel!.Starting8BitDmxValueState.Subscribe(Update8BitStartingValueTextBox)
                .DisposeWith(disposables);
            ViewModel!.Ending8BitDmxValueState.Subscribe(Update8BitEndingValueTextBox)
                .DisposeWith(disposables);
            ViewModel!.Function8BitValueState.Subscribe(Update8BitFunctionComboBox)
                .DisposeWith(disposables);
            ViewModel!.Feature8BitValueState.Subscribe(Update8BitFeatureComboBox)
                .DisposeWith(disposables);
            ViewModel!.Starting16BitDmxValueState.Subscribe(Update16BitStartingValueTextBox)
                .DisposeWith(disposables);
            ViewModel!.Ending16BitDmxValueState.Subscribe(Update16BitEndingValueTextBox)
                .DisposeWith(disposables);
            ViewModel!.Function16BitValueState.Subscribe(Update16BitFunctionComboBox)
                .DisposeWith(disposables);
            ViewModel!.Feature16BitValueState.Subscribe(Update16BitFeatureComboBox)
                .DisposeWith(disposables);
        });
    }

    private void Update8BitStartingValueTextBox(IValidationState isValidEightBitDmxValue)
    {
        DmxStartInput.Classes.Clear();

        DmxStartInputToolTip.Text = isValidEightBitDmxValue.Text.ToSingleLine();

        DmxStartInput.Classes.Set(isValidEightBitDmxValue.IsValid ? "Valid" : "Error", true);
    }

    private void Update16BitStartingValueTextBox(IValidationState isValidSixteenBitDmxValue)
    {
        SixteenBitDmxStartInput.Classes.Clear();

        SixteenBitDmxStartInputToolTip.Text = isValidSixteenBitDmxValue.Text.ToSingleLine();

        SixteenBitDmxStartInput.Classes.Set(isValidSixteenBitDmxValue.IsValid ? "Valid" : "Error", true);
    }

    private void Update8BitEndingValueTextBox(IValidationState isValidEightBitDmxValue)
    {
        DmxEndInput.Classes.Clear();

        DmxEndInputToolTip.Text = isValidEightBitDmxValue.Text.ToSingleLine();

        DmxEndInput.Classes.Set(isValidEightBitDmxValue.IsValid ? "Valid" : "Error", true);
    }

    private void Update16BitEndingValueTextBox(IValidationState isValidSixteenBitDmxValue)
    {
        SixteenBitDmxEndInput.Classes.Clear();

        SixteenBitDmxEndInputToolTip.Text = isValidSixteenBitDmxValue.Text.ToSingleLine();

        SixteenBitDmxEndInput.Classes.Set(isValidSixteenBitDmxValue.IsValid ? "Valid" : "Error", true);
    }

    private void Update8BitFunctionComboBox(IValidationState isValidFunction)
    {
        FunctionInput.Classes.Clear();

        FunctionInputToolTip.Text = isValidFunction.Text.ToSingleLine();

        FunctionInput.Classes.Set(isValidFunction.IsValid ? "Valid" : "Warning", true);
    }

    private void Update16BitFunctionComboBox(IValidationState isValidFunction)
    {
        SixteenBitFunctionInput.Classes.Clear();

        SixteenBitFunctionInputToolTip.Text = isValidFunction.Text.ToSingleLine();

        SixteenBitFunctionInput.Classes.Set(isValidFunction.IsValid ? "Valid" : "Warning", true);
    }

    private void Update8BitFeatureComboBox(IValidationState isValidFeature)
    {
        FeatureInput.Classes.Clear();

        FeatureInputToolTip.Text = isValidFeature.Text.ToSingleLine();

        FeatureInput.Classes.Set(isValidFeature.IsValid ? "Valid" : "Warning", true);
    }

    private void Update16BitFeatureComboBox(IValidationState isValidFeature)
    {
        SixteenBitFeatureInput.Classes.Clear();

        SixteenBitFeatureInputToolTip.Text = isValidFeature.Text.ToSingleLine();
        
        SixteenBitFeatureInput.Classes.Set(isValidFeature.IsValid ? "Valid" : "Warning", true);
    }
}