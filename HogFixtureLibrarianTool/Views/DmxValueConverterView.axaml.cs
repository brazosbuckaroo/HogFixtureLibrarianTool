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

        if (isValidEightBitDmxValue.IsValid)
            DmxStartInput.Classes.Set("Valid", true);
        else
            DmxStartInput.Classes.Set("Error", true);
    }

    private void Update16BitStartingValueTextBox(IValidationState isValidSixteenBitDmxValue)
    {
        SixteenBitDmxStartInput.Classes.Clear();

        SixteenBitDmxStartInputToolTip.Text = isValidSixteenBitDmxValue.Text.ToSingleLine();

        if (isValidSixteenBitDmxValue.IsValid)
            SixteenBitDmxStartInput.Classes.Set("Valid", true);
        else
            SixteenBitDmxStartInput.Classes.Set("Error", true);
    }

    private void Update8BitEndingValueTextBox(IValidationState isValidEightBitDmxValue)
    {
        DmxEndInput.Classes.Clear();

        DmxEndInputToolTip.Text = isValidEightBitDmxValue.Text.ToSingleLine();

        if (isValidEightBitDmxValue.IsValid)
            DmxEndInput.Classes.Set("Valid", true);
        else
            DmxEndInput.Classes.Set("Error", true);
    }

    private void Update16BitEndingValueTextBox(IValidationState isValidSixteenBitDmxValue)
    {
        SixteenBitDmxEndInput.Classes.Clear();

        SixteenBitDmxEndInputToolTip.Text = isValidSixteenBitDmxValue.Text.ToSingleLine();

        if (isValidSixteenBitDmxValue.IsValid)
            SixteenBitDmxEndInput.Classes.Set("Valid", true);
        else
            SixteenBitDmxEndInput.Classes.Set("Error", true);
    }

    private void Update8BitFunctionComboBox(IValidationState isValidFunction)
    {
        FunctionInput.Classes.Clear();

        FunctionInputToolTip.Text = isValidFunction.Text.ToSingleLine();

        if (isValidFunction.IsValid)
            FunctionInput.Classes.Set("Valid", true);
        else
            FunctionInput.Classes.Set("Warning", true);
    }

    private void Update16BitFunctionComboBox(IValidationState isValidFunction)
    {
        SixteenBitFunctionInput.Classes.Clear();

        SixteenBitFunctionInputToolTip.Text = isValidFunction.Text.ToSingleLine();

        if (isValidFunction.IsValid)
            SixteenBitFunctionInput.Classes.Set("Valid", true);
        else
            SixteenBitFunctionInput.Classes.Set("Warning", true);
    }

    private void Update8BitFeatureComboBox(IValidationState isValidFeature)
    {
        FeatureInput.Classes.Clear();

        FeatureInputToolTip.Text = isValidFeature.Text.ToSingleLine();

        if (isValidFeature.IsValid)
            FeatureInput.Classes.Set("Valid", true);
        else
            FeatureInput.Classes.Set("Warning", true);
    }

    private void Update16BitFeatureComboBox(IValidationState isValidFeature)
    {
        SixteenBitFeatureInput.Classes.Clear();

        SixteenBitFeatureInputToolTip.Text = isValidFeature.Text.ToSingleLine();

        if (isValidFeature.IsValid)
            SixteenBitFeatureInput.Classes.Set("Valid", true);
        else
            SixteenBitFeatureInput.Classes.Set("Warning", true);
    }
}