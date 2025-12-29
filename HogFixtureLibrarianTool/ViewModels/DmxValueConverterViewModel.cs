namespace HogFixtureLibrarianTool.ViewModels;

public class DmxValueConverterViewModel : RoutableViewModelBase
{
    private readonly IDmxValidator _dmxValidator;

    private readonly IDbManager _sqliteManager;

    private ObservableCollection<string> _features;

    private ObservableCollection<string> _functions;

    private string _range16BitDmxEndInput;

    private string _range16BitDmxStartInput;

    private string _range16BitFeatureInput;

    private string _range16BitFunctionInput;

    private string _range8BitDmxEndInput;

    private string _range8BitDmxStartInput;

    private string _range8BitFeatureInput;

    private string _range8BitFunctionInput;

    private ObservableCollection<HogRange> _ranges16Bit;

    private ObservableCollection<HogRange> _ranges8Bit;

    private HogRange? _selected16BitRange;

    private HogRange? _selected8BitRange;

    public DmxValueConverterViewModel()
    {
        _dmxValidator = new HogDmxValidator();
        _sqliteManager = new SqlLiteManager();
        Range8BitDmxStartInput = string.Empty;
        _range8BitDmxStartInput = string.Empty;
        Range8BitDmxEndInput = string.Empty;
        _range8BitDmxEndInput = string.Empty;
        Range8BitFunctionInput = string.Empty;
        _range8BitFunctionInput = string.Empty;
        Range8BitFeatureInput = string.Empty;
        _range8BitFeatureInput = string.Empty;
        Range16BitDmxStartInput = string.Empty;
        _range16BitDmxStartInput = string.Empty;
        Range16BitDmxEndInput = string.Empty;
        _range16BitDmxEndInput = string.Empty;
        Range16BitFunctionInput = string.Empty;
        _range16BitFunctionInput = string.Empty;
        Range16BitFeatureInput = string.Empty;
        _range16BitFeatureInput = string.Empty;
        Selected8BitRange = null;
        _selected8BitRange = null;
        Selected16BitRange = null;
        _selected16BitRange = null;
        Ranges8Bit = new ObservableCollection<HogRange>();
        _ranges8Bit = new ObservableCollection<HogRange>();
        Ranges16Bit = new ObservableCollection<HogRange>();
        _ranges16Bit = new ObservableCollection<HogRange>();
        Functions = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FUNCTIONS").Result);
        _functions = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FUNCTIONS").Result);
        Features = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FEATURES").Result);
        _features = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FEATURES").Result);
        Create8BitDmxRange = ReactiveCommand.Create(Create8BitDmxRangeCommand, IsValidInputDmxValues());
        Delete8BitDmxRange = ReactiveCommand.Create<HogRange>(Delete8BitDmxRangeCommand);
        DeleteAll8BitRanges = ReactiveCommand.Create(DeleteAll8BitRangesCommand);
        Create16BitDmxRange = ReactiveCommand.Create(Create16BitDmxRangeCommand, IsValidInputDmxValues(true));
        Delete16BitDmxRange = ReactiveCommand.Create<HogRange>(Delete16BitDmxRangeCommand);
        DeleteAll16BitRanges = ReactiveCommand.Create(DeleteAll16BitRangesCommand);
        Starting8BitDmxValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range8BitDmxStartInput)
            .Select(_dmxValidator.IsValid8BitDmxValue);
        Ending8BitDmxValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range8BitDmxEndInput)
            .Select(_dmxValidator.IsValid8BitDmxValue);
        Function8BitValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range8BitFunctionInput)
            .Select(IsValidInputFunction);
        Feature8BitValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range8BitFeatureInput)
            .Select(IsValidInputFeature);
        Starting16BitDmxValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range16BitDmxStartInput)
            .Select(_dmxValidator.IsValid16BitDmxValue);
        Ending16BitDmxValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range16BitDmxEndInput)
            .Select(_dmxValidator.IsValid16BitDmxValue);
        Function16BitValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range16BitFunctionInput)
            .Select(IsValidInputFunction);
        Feature16BitValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range16BitFeatureInput)
            .Select(IsValidInputFeature);
        AddingMultipleRangesInteraction = new Interaction<AddMultipleRangesWindowViewModel, HogRange[]?>();
        OpenAddMultipleDmxRangesDialog =
            ReactiveCommand.CreateFromTask<bool>(OpenAddMultipleDmxRangesDialogAsyncCommand);
        DeleteSelectedOrLast8BitRange = ReactiveCommand.Create(DeleteSelectedOrLastRange8BitCommand);
        DeleteSelectedOrLast16BitRange = ReactiveCommand.Create(DeleteSelectedOrLastRange16BitCommand);

        this.WhenActivated(disposables =>
        {
            Starting8BitDmxValueState.Subscribe().DisposeWith(disposables);
            Ending8BitDmxValueState.Subscribe().DisposeWith(disposables);
            Function8BitValueState.Subscribe().DisposeWith(disposables);
            Feature8BitValueState.Subscribe().DisposeWith(disposables);
            Starting16BitDmxValueState.Subscribe().DisposeWith(disposables);
            Ending16BitDmxValueState.Subscribe().DisposeWith(disposables);
            Function16BitValueState.Subscribe().DisposeWith(disposables);
            Feature16BitValueState.Subscribe().DisposeWith(disposables);
        });
    }

    public DmxValueConverterViewModel(IScreen hostScreen)
    {
        _dmxValidator = new HogDmxValidator();
        _sqliteManager = new SqlLiteManager();
        HostScreen = hostScreen;
        Range8BitDmxStartInput = string.Empty;
        _range8BitDmxStartInput = string.Empty;
        Range8BitDmxEndInput = string.Empty;
        _range8BitDmxEndInput = string.Empty;
        Range8BitFunctionInput = string.Empty;
        _range8BitFunctionInput = string.Empty;
        Range8BitFeatureInput = string.Empty;
        _range8BitFeatureInput = string.Empty;
        Range16BitDmxStartInput = string.Empty;
        _range16BitDmxStartInput = string.Empty;
        Range16BitDmxEndInput = string.Empty;
        _range16BitDmxEndInput = string.Empty;
        Range16BitFunctionInput = string.Empty;
        _range16BitFunctionInput = string.Empty;
        Range16BitFeatureInput = string.Empty;
        _range16BitFeatureInput = string.Empty;
        Selected8BitRange = null;
        _selected8BitRange = null;
        Selected16BitRange = null;
        _selected16BitRange = null;
        Ranges8Bit = new ObservableCollection<HogRange>();
        _ranges8Bit = new ObservableCollection<HogRange>();
        Ranges16Bit = new ObservableCollection<HogRange>();
        _ranges16Bit = new ObservableCollection<HogRange>();
        Functions = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FUNCTIONS").Result);
        _functions = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FUNCTIONS").Result);
        Features = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FEATURES").Result);
        _features = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FEATURES").Result);
        Create8BitDmxRange = ReactiveCommand.Create(Create8BitDmxRangeCommand, IsValidInputDmxValues());
        Delete8BitDmxRange = ReactiveCommand.Create<HogRange>(Delete8BitDmxRangeCommand);
        DeleteAll8BitRanges = ReactiveCommand.Create(DeleteAll8BitRangesCommand);
        Create16BitDmxRange = ReactiveCommand.Create(Create16BitDmxRangeCommand, IsValidInputDmxValues(true));
        Delete16BitDmxRange = ReactiveCommand.Create<HogRange>(Delete16BitDmxRangeCommand);
        DeleteAll16BitRanges = ReactiveCommand.Create(DeleteAll16BitRangesCommand);
        Starting8BitDmxValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range8BitDmxStartInput)
            .Select(_dmxValidator.IsValid8BitDmxValue);
        Ending8BitDmxValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range8BitDmxEndInput)
            .Select(_dmxValidator.IsValid8BitDmxValue);
        Function8BitValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range8BitFunctionInput)
            .Select(IsValidInputFunction);
        Feature8BitValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range8BitFeatureInput)
            .Select(IsValidInputFeature);
        Starting16BitDmxValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range16BitDmxStartInput)
            .Select(_dmxValidator.IsValid16BitDmxValue);
        Ending16BitDmxValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range16BitDmxEndInput)
            .Select(_dmxValidator.IsValid16BitDmxValue);
        Function16BitValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range16BitFunctionInput)
            .Select(IsValidInputFunction);
        Feature16BitValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range16BitFeatureInput)
            .Select(IsValidInputFeature);
        AddingMultipleRangesInteraction = new Interaction<AddMultipleRangesWindowViewModel, HogRange[]?>();
        OpenAddMultipleDmxRangesDialog =
            ReactiveCommand.CreateFromTask<bool>(OpenAddMultipleDmxRangesDialogAsyncCommand);
        DeleteSelectedOrLast8BitRange = ReactiveCommand.Create(DeleteSelectedOrLastRange8BitCommand);
        DeleteSelectedOrLast16BitRange = ReactiveCommand.Create(DeleteSelectedOrLastRange16BitCommand);

        this.WhenActivated(disposables =>
        {
            Starting8BitDmxValueState.Subscribe().DisposeWith(disposables);
            Ending8BitDmxValueState.Subscribe().DisposeWith(disposables);
            Function8BitValueState.Subscribe().DisposeWith(disposables);
            Feature8BitValueState.Subscribe().DisposeWith(disposables);
            Starting16BitDmxValueState.Subscribe().DisposeWith(disposables);
            Ending16BitDmxValueState.Subscribe().DisposeWith(disposables);
            Function16BitValueState.Subscribe().DisposeWith(disposables);
            Feature16BitValueState.Subscribe().DisposeWith(disposables);
        });
    }

    public DmxValueConverterViewModel(IScreen hostScreen,
        IDmxValidator dmxValidator,
        IDbManager sqliteManager)
    {
        _dmxValidator = dmxValidator;
        _sqliteManager = sqliteManager;
        HostScreen = hostScreen;
        Range8BitDmxStartInput = string.Empty;
        _range8BitDmxStartInput = string.Empty;
        Range8BitDmxEndInput = string.Empty;
        _range8BitDmxEndInput = string.Empty;
        Range8BitFunctionInput = string.Empty;
        _range8BitFunctionInput = string.Empty;
        Range8BitFeatureInput = string.Empty;
        _range8BitFeatureInput = string.Empty;
        Range16BitDmxStartInput = string.Empty;
        _range16BitDmxStartInput = string.Empty;
        Range16BitDmxEndInput = string.Empty;
        _range16BitDmxEndInput = string.Empty;
        Range16BitFunctionInput = string.Empty;
        _range16BitFunctionInput = string.Empty;
        Range16BitFeatureInput = string.Empty;
        _range16BitFeatureInput = string.Empty;
        Selected8BitRange = null;
        _selected8BitRange = null;
        Selected16BitRange = null;
        _selected16BitRange = null;
        Ranges8Bit = new ObservableCollection<HogRange>();
        _ranges8Bit = new ObservableCollection<HogRange>();
        Ranges16Bit = new ObservableCollection<HogRange>();
        _ranges16Bit = new ObservableCollection<HogRange>();
        Functions = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FUNCTIONS").Result);
        _functions = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FUNCTIONS").Result);
        Features = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FEATURES").Result);
        _features = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FEATURES").Result);
        Create8BitDmxRange = ReactiveCommand.Create(Create8BitDmxRangeCommand, IsValidInputDmxValues(true));
        Delete8BitDmxRange = ReactiveCommand.Create<HogRange>(Delete8BitDmxRangeCommand);
        DeleteAll8BitRanges = ReactiveCommand.Create(DeleteAll8BitRangesCommand);
        Create16BitDmxRange = ReactiveCommand.Create(Create16BitDmxRangeCommand, IsValidInputDmxValues());
        Delete16BitDmxRange = ReactiveCommand.Create<HogRange>(Delete16BitDmxRangeCommand);
        DeleteAll16BitRanges = ReactiveCommand.Create(DeleteAll16BitRangesCommand);
        Starting8BitDmxValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range8BitDmxStartInput)
            .Select(_dmxValidator.IsValid8BitDmxValue);
        Ending8BitDmxValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range8BitDmxEndInput)
            .Select(_dmxValidator.IsValid8BitDmxValue);
        Function8BitValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range8BitFunctionInput)
            .Select(IsValidInputFunction);
        Feature8BitValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range8BitFeatureInput)
            .Select(IsValidInputFeature);
        Starting16BitDmxValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range16BitDmxStartInput)
            .Select(_dmxValidator.IsValid16BitDmxValue);
        Ending16BitDmxValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range16BitDmxEndInput)
            .Select(_dmxValidator.IsValid16BitDmxValue);
        Function16BitValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range16BitFunctionInput)
            .Select(IsValidInputFunction);
        Feature16BitValueState = this.WhenValueChanged(thisViewModel => thisViewModel.Range16BitFeatureInput)
            .Select(IsValidInputFeature);
        AddingMultipleRangesInteraction = new Interaction<AddMultipleRangesWindowViewModel, HogRange[]?>();
        OpenAddMultipleDmxRangesDialog =
            ReactiveCommand.CreateFromTask<bool>(OpenAddMultipleDmxRangesDialogAsyncCommand);
        DeleteSelectedOrLast8BitRange = ReactiveCommand.Create(DeleteSelectedOrLastRange8BitCommand);
        DeleteSelectedOrLast16BitRange = ReactiveCommand.Create(DeleteSelectedOrLastRange16BitCommand);

        this.WhenActivated(disposables =>
        {
            Starting8BitDmxValueState.Subscribe().DisposeWith(disposables);
            Ending8BitDmxValueState.Subscribe().DisposeWith(disposables);
            Function8BitValueState.Subscribe().DisposeWith(disposables);
            Feature8BitValueState.Subscribe().DisposeWith(disposables);
            Starting16BitDmxValueState.Subscribe().DisposeWith(disposables);
            Ending16BitDmxValueState.Subscribe().DisposeWith(disposables);
            Function16BitValueState.Subscribe().DisposeWith(disposables);
            Feature16BitValueState.Subscribe().DisposeWith(disposables);
        });
    }

    public string Range8BitDmxStartInput
    {
        get => _range8BitDmxStartInput;
        set
        {
            // this is to allow for entering a single value and
            // getting both the 16-bit starting and 16-bit
            // ending value... seems like an odd way but it works???
            // TODO:
            // Think of a better way to do this. :P
            //      -DTL
            if (Range8BitDmxStartInput == Range8BitDmxEndInput) Range8BitDmxEndInput = value;

            this.RaiseAndSetIfChanged(ref _range8BitDmxStartInput, value);
        }
    }

    public string Range16BitDmxStartInput
    {
        get => _range16BitDmxStartInput;
        set
        {
            // this is to allow for entering a single value and
            // getting both the 16-bit starting and 16-bit
            // ending value... seems like an odd way but it works???
            // TODO:
            // Think of a better way to do this. :P
            //      -DTL
            if (Range16BitDmxStartInput == Range16BitDmxEndInput) Range16BitDmxEndInput = value;

            this.RaiseAndSetIfChanged(ref _range16BitDmxStartInput, value);
        }
    }

    public string Range8BitDmxEndInput
    {
        get => _range8BitDmxEndInput;
        set => this.RaiseAndSetIfChanged(ref _range8BitDmxEndInput, value);
    }

    public string Range16BitDmxEndInput
    {
        get => _range16BitDmxEndInput;
        set => this.RaiseAndSetIfChanged(ref _range16BitDmxEndInput, value);
    }

    public string Range8BitFunctionInput
    {
        get => _range8BitFunctionInput;
        set => this.RaiseAndSetIfChanged(ref _range8BitFunctionInput, value);
    }

    public string Range8BitFeatureInput
    {
        get => _range8BitFeatureInput;
        set => this.RaiseAndSetIfChanged(ref _range8BitFeatureInput, value);
    }

    public string Range16BitFunctionInput
    {
        get => _range16BitFunctionInput;
        set => this.RaiseAndSetIfChanged(ref _range16BitFunctionInput, value);
    }

    public string Range16BitFeatureInput
    {
        get => _range16BitFeatureInput;
        set => this.RaiseAndSetIfChanged(ref _range16BitFeatureInput, value);
    }

    public HogRange? Selected8BitRange
    {
        get => _selected8BitRange;
        set => this.RaiseAndSetIfChanged(ref _selected8BitRange, value);
    }

    public HogRange? Selected16BitRange
    {
        get => _selected8BitRange;
        set => this.RaiseAndSetIfChanged(ref _selected16BitRange, value);
    }

    public IObservable<IValidationState> Starting8BitDmxValueState { get; }

    public IObservable<IValidationState> Ending8BitDmxValueState { get; }

    public IObservable<IValidationState> Starting16BitDmxValueState { get; }

    public IObservable<IValidationState> Ending16BitDmxValueState { get; }

    public IObservable<IValidationState> Function8BitValueState { get; }

    public IObservable<IValidationState> Feature8BitValueState { get; }

    public IObservable<IValidationState> Function16BitValueState { get; }

    public IObservable<IValidationState> Feature16BitValueState { get; }

    public Interaction<AddMultipleRangesWindowViewModel, HogRange[]?> AddingMultipleRangesInteraction { get; }

    public ObservableCollection<HogRange> Ranges8Bit
    {
        get => _ranges8Bit;
        set => this.RaiseAndSetIfChanged(ref _ranges8Bit, value);
    }

    public ObservableCollection<HogRange> Ranges16Bit
    {
        get => _ranges16Bit;
        set => this.RaiseAndSetIfChanged(ref _ranges16Bit, value);
    }

    public ObservableCollection<string> Functions
    {
        get => _functions;
        set => this.RaiseAndSetIfChanged(ref _functions, value);
    }

    public ObservableCollection<string> Features
    {
        get => _features;
        set => this.RaiseAndSetIfChanged(ref _features, value);
    }

    public ReactiveCommand<Unit, Unit> Create8BitDmxRange { get; }

    public ReactiveCommand<HogRange, Unit> Delete8BitDmxRange { get; }

    public ReactiveCommand<Unit, Unit> Create16BitDmxRange { get; }

    public ReactiveCommand<HogRange, Unit> Delete16BitDmxRange { get; }

    public ReactiveCommand<Unit, Unit> DeleteAll8BitRanges { get; }

    public ReactiveCommand<Unit, Unit> DeleteAll16BitRanges { get; }

    public ReactiveCommand<bool, Unit> OpenAddMultipleDmxRangesDialog { get; }

    public ReactiveCommand<Unit, Unit> DeleteSelectedOrLast8BitRange { get; }

    public ReactiveCommand<Unit, Unit> DeleteSelectedOrLast16BitRange { get; }

    private async Task OpenAddMultipleDmxRangesDialogAsyncCommand(bool is16Bit = false)
    {
        var dialog = new AddMultipleRangesWindowViewModel(_dmxValidator, _sqliteManager, true, is16Bit);
        var newRanges = await AddingMultipleRangesInteraction.Handle(dialog);

        if (is16Bit)
        {
            if (newRanges != null)
            {
                Ranges8Bit.Clear();

                var sixteenBitRanges = new HogRange[newRanges.Length];

                for (var i = 0; i < newRanges.Length; i++)
                    sixteenBitRanges[i] = Convert16BitValueTo8BitValue(newRanges[i]);

                Ranges8Bit = new ObservableCollection<HogRange>(sixteenBitRanges);
            }
        }
        else
        {
            if (newRanges != null)
            {
                Ranges16Bit.Clear();

                var eightBitRanges = new HogRange[newRanges.Length];

                for (var i = 0; i < newRanges.Length; i++)
                    eightBitRanges[i] = Convert8BitValueTo16BitValue(newRanges[i]);

                Ranges16Bit = new ObservableCollection<HogRange>(eightBitRanges);
            }
        }
    }

    private void Create8BitDmxRangeCommand()
    {
        var newValue = Convert16BitValueTo8BitValue(new HogRange(Range16BitDmxStartInput,
            DmxEnd: Range16BitDmxEndInput,
            Function: Range16BitFunctionInput,
            Feature: Range16BitFeatureInput,
            Start: null,
            End: null));

        Ranges8Bit.Add(newValue);

        Range16BitDmxStartInput = string.Empty;
        Range16BitDmxEndInput = string.Empty;
        Range16BitFunctionInput = string.Empty;
        Range16BitFeatureInput = string.Empty;
    }

    private void Delete8BitDmxRangeCommand(HogRange rangeToDelete)
    {
        Ranges8Bit.Remove(rangeToDelete);
    }

    private void DeleteAll8BitRangesCommand()
    {
        Ranges8Bit.Clear();
    }

    private void DeleteSelectedOrLastRange8BitCommand()
    {
        if (Ranges8Bit.Count == 0) return;

        if (Selected8BitRange != null)
            Ranges8Bit.Remove(Selected8BitRange);
        else
            Ranges8Bit.RemoveAt(Ranges8Bit.Count - 1);
    }

    private void Create16BitDmxRangeCommand()
    {
        var newValue = Convert8BitValueTo16BitValue(new HogRange(Range8BitDmxStartInput,
            DmxEnd: Range8BitDmxEndInput,
            Function: Range8BitFunctionInput,
            Feature: Range8BitFeatureInput,
            Start: null,
            End: null));

        Ranges16Bit.Add(newValue);

        Range8BitDmxStartInput = string.Empty;
        Range8BitDmxEndInput = string.Empty;
        Range8BitFunctionInput = string.Empty;
        Range8BitFeatureInput = string.Empty;
    }

    private void Delete16BitDmxRangeCommand(HogRange rangeToDelete)
    {
        Ranges16Bit.Remove(rangeToDelete);
    }

    private void DeleteAll16BitRangesCommand()
    {
        Ranges16Bit.Clear();
    }

    private void DeleteSelectedOrLastRange16BitCommand()
    {
        if (Ranges16Bit.Count == 0) return;

        if (Selected16BitRange != null)
            Ranges16Bit.Remove(Selected16BitRange);
        else
            Ranges16Bit.RemoveAt(Ranges16Bit.Count - 1);
    }

    private HogRange Convert8BitValueTo16BitValue(HogRange eightBitRange)
    {
        if (!_dmxValidator.TryConvertDmxStringToIntegers(eightBitRange,
                out _,
                out _))
            throw new ArgumentException("Invalid 8-Bit DMX Value.");

        // special HOG case for NOVALUE AND DISALLOWED
        if (_dmxValidator.TryConvertDmxStringToIntegers(eightBitRange,
                out var sixteenBitStartingValue,
                out var sixteenBitEndingValue)
            && sixteenBitStartingValue == null && sixteenBitEndingValue == null)
            return eightBitRange;

        var midOne = 0.0;
        var midTwo = 0.0;

        // a check to see if the dmx values are flipped :P
        if (sixteenBitEndingValue < sixteenBitStartingValue)
        {
            sixteenBitEndingValue = sixteenBitEndingValue * 256;
            sixteenBitStartingValue = (sixteenBitStartingValue + 1) * 256 - 1;
            var difference = (double?)(sixteenBitStartingValue - sixteenBitEndingValue) / 2;
            midTwo = Math.Floor((double)(sixteenBitEndingValue + difference));
            midOne = Math.Ceiling((double)(sixteenBitStartingValue - difference));
        }
        else
        {
            if (sixteenBitStartingValue.HasValue && sixteenBitEndingValue.HasValue)
            {
                sixteenBitStartingValue = sixteenBitStartingValue * 256;
                sixteenBitEndingValue = (sixteenBitEndingValue + 1) * 256 - 1;
                var difference = (double?)(sixteenBitEndingValue - sixteenBitStartingValue) / 2;
                midOne = Math.Floor((double)(sixteenBitStartingValue + difference));
                midTwo = Math.Ceiling((double)(sixteenBitEndingValue - difference));
            }
        }

        var range = new HogRange(dmxStart: sixteenBitStartingValue,
            dmxMidOne: (int)midOne,
            dmxMidTwo: (int)midTwo,
            dmxEnd: string.IsNullOrEmpty(eightBitRange.DmxEnd) ? null : sixteenBitEndingValue,
            function: eightBitRange.Function,
            feature: eightBitRange.Feature,
            start: eightBitRange.Start,
            end: eightBitRange.End);

        return range;
    }

    private HogRange Convert16BitValueTo8BitValue(HogRange sixteenBitRange)
    {
        if (!_dmxValidator.TryConvertDmxStringToIntegers(sixteenBitRange,
                out _,
                out _,
                true))
            throw new ArgumentException("Invalid 16-Bit DMX Value.");

        // special HOG case for NOVALUE AND DISALLOWED
        if (_dmxValidator.TryConvertDmxStringToIntegers(sixteenBitRange,
                out var sixteenBitStartingValue,
                out var sixteenBitEndingValue,
                true)
            && sixteenBitStartingValue == null && sixteenBitEndingValue == null)
            return sixteenBitRange;

        // a check to see if the dmx values are the same :P
        if (sixteenBitEndingValue == sixteenBitStartingValue)
        {
            sixteenBitEndingValue = sixteenBitEndingValue / 256;
            sixteenBitStartingValue = sixteenBitEndingValue;
        }
        // a check to see if the dmx values are flipped :o
        else if (sixteenBitEndingValue < sixteenBitStartingValue)
        {
            sixteenBitEndingValue = sixteenBitEndingValue / 256;
            sixteenBitStartingValue = (sixteenBitStartingValue + 1) / 256 - 1;
        }
        else
        {
            sixteenBitStartingValue = sixteenBitStartingValue / 256;
            sixteenBitEndingValue = (sixteenBitEndingValue + 1) / 256 - 1;
        }

        var range = new HogRange(sixteenBitStartingValue,
            dmxEnd: string.IsNullOrEmpty(sixteenBitRange.DmxEnd) ? null : sixteenBitEndingValue,
            function: sixteenBitRange.Function,
            feature: sixteenBitRange.Feature,
            start: sixteenBitRange.Start,
            end: sixteenBitRange.End);

        return range;
    }

    private IObservable<bool> IsValidInputDmxValues(bool is16Bit = false)
    {
        if (is16Bit)
            return this.WhenAnyValue(thisViewModel => thisViewModel.Range16BitDmxStartInput,
                thisViewModel => thisViewModel.Range16BitDmxEndInput,
                (starting16BitValue, ending16BitValue) =>
                    _dmxValidator.IsValid16BitDmxValue(starting16BitValue).IsValid
                    && _dmxValidator.IsValid16BitDmxValue(ending16BitValue).IsValid);

        return this.WhenAnyValue(thisViewModel => thisViewModel.Range8BitDmxStartInput,
            thisViewModel => thisViewModel.Range8BitDmxEndInput,
            (starting8BitValue, ending8BitValue) =>
                _dmxValidator.IsValid8BitDmxValue(starting8BitValue).IsValid
                && _dmxValidator.IsValid8BitDmxValue(ending8BitValue).IsValid);
    }

    private IValidationState IsValidInputFunction(string? functionInput)
    {
        if (!_dmxValidator.IsValidFunction(functionInput).Result.IsValid)
            return new ValidationState(false, "No function selected.");

        return new ValidationState(true, "Valid");
    }

    private IValidationState IsValidInputFeature(string? featureInput)
    {
        if (!_dmxValidator.IsValidFunction(featureInput).Result.IsValid)
            return new ValidationState(false, "No feature selected.");

        return new ValidationState(true, "Valid");
    }
}