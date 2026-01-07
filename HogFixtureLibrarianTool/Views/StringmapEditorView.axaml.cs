namespace HogFixtureLibrarianTool.Views;

public partial class StringmapEditorView : ReactiveUserControl<StringmapEditorViewModel>
{
    public StringmapEditorView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            ViewModel!.RangeDmxStartState.Subscribe(UpdateRangeDmxStart)
                .DisposeWith(disposables);
            ViewModel!.RangeFunctionState.Subscribe(UpdateRangeFunction)
                .DisposeWith(disposables);
            ViewModel!.RangeFeatureState.Subscribe(UpdateRangeFeature)
                .DisposeWith(disposables);
            ViewModel!.RangeStartState.Subscribe(UpdateRangeStart)
                .DisposeWith(disposables);
        });
    }

    private void UpdateRangeDmxStart(IValidationState rangeDmxStartState)
    {
        DmxStartInput.Classes.Clear();

        DmxStartToolTip.Text = rangeDmxStartState.Text.ToSingleLine();

        DmxStartInput.Classes.Set(rangeDmxStartState.IsValid ? "Valid" : "Error", true);
    }

    private void UpdateRangeFunction(IValidationState rangeFunctionState)
    {
        RangeFunctionInput.Classes.Clear();

        RangeFunctionToolTip.Text = rangeFunctionState.Text.ToSingleLine();

        RangeFunctionInput.Classes.Set(rangeFunctionState.IsValid ? "Valid" : "Error", true);
    }

    private void UpdateRangeFeature(IValidationState rangeFeatureState)
    {
        RangeFeatureInput.Classes.Clear();

        RangeFeatureToolTip.Text = rangeFeatureState.Text.ToSingleLine();

        RangeFeatureInput.Classes.Set(rangeFeatureState.IsValid ? "Valid" : "Error", true);
    }

    private void UpdateRangeStart(IValidationState rangeStartState)
    {
        StartInput.Classes.Clear();

        StartToolTip.Text = rangeStartState.Text.ToSingleLine();

        StartInput.Classes.Set(rangeStartState.IsValid ? "Valid" : "Error", true);
    }
}