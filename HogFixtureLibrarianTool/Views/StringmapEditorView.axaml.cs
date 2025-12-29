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

        if (rangeDmxStartState.IsValid)
            DmxStartInput.Classes.Set("Valid", true);
        else
            DmxStartInput.Classes.Set("Error", true);
    }

    private void UpdateRangeFunction(IValidationState rangeFuntionState)
    {
        RangeFunctionInput.Classes.Clear();

        RangeFunctionToolTip.Text = rangeFuntionState.Text.ToSingleLine();

        if (rangeFuntionState.IsValid)
            RangeFunctionInput.Classes.Set("Valid", true);
        else
            RangeFunctionInput.Classes.Set("Error", true);
    }

    private void UpdateRangeFeature(IValidationState rangeFeatureState)
    {
        RangeFeatureInput.Classes.Clear();

        RangeFeatureToolTip.Text = rangeFeatureState.Text.ToSingleLine();

        if (rangeFeatureState.IsValid)
            RangeFeatureInput.Classes.Set("Valid", true);
        else
            RangeFeatureInput.Classes.Set("Error", true);
    }

    private void UpdateRangeStart(IValidationState rangeStartState)
    {
        StartInput.Classes.Clear();

        StartToolTip.Text = rangeStartState.Text.ToSingleLine();

        if (rangeStartState.IsValid)
            StartInput.Classes.Set("Valid", true);
        else
            StartInput.Classes.Set("Error", true);
    }
}