namespace HogFixtureLibrarianTool.ViewModels;

public class AddMultipleRangesWindowViewModel : ValidatableViewModelBase
{
    private const int _16BitOffset = 65535;

    private const int _8BitOffset = 256;

    private readonly IDmxValidator _dmxValidator;

    private readonly bool _is16Bit;

    private readonly IDbManager _sqliteManager;

    private string _dmxOffset;

    private string _dmxStartValue;

    private ObservableCollection<string> _features;

    private ObservableCollection<string> _functions;

    private bool _includeEndValue;

    private string _numberOfRanges;

    private string _selectedFeature;

    private string _selectedFunction;

    public AddMultipleRangesWindowViewModel()
    {
        _dmxValidator = new HogDmxValidator();
        _sqliteManager = new SqlLiteManager();
        NumberOfRanges = string.Empty;
        _numberOfRanges = string.Empty;
        DmxStartValue = string.Empty;
        _dmxStartValue = string.Empty;
        DmxOffset = string.Empty;
        _dmxOffset = string.Empty;
        SelectedFunction = string.Empty;
        _selectedFunction = string.Empty;
        SelectedFeature = string.Empty;
        _selectedFeature = string.Empty;
        IncludeEndValue = true;
        _includeEndValue = true;
        _is16Bit = false;
        Functions = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FUNCTIONS").Result);
        _functions = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FUNCTIONS").Result);
        Features = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FEATURES").Result);
        _features = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FEATURES").Result);
        NumberOfRangesState = this.WhenAnyValue(thisViewModel => thisViewModel.NumberOfRanges,
                thisViewModel => thisViewModel.DmxOffset)
            .Select(properties => IsValidNumberOfRanges(properties.Item1, properties.Item2));
        DmxStartValueState = this.WhenAnyValue(thisViewModel => thisViewModel.DmxStartValue,
                thisViewModel => thisViewModel.NumberOfRanges,
                thisViewModel => thisViewModel.DmxOffset)
            .Select(properties => IsValidDmxStartValue(properties.Item1, properties.Item2,
                properties.Item3));
        DmxOffsetState = this.WhenAnyValue(thisViewModel => thisViewModel.DmxOffset)
            .Select(IsValidNumber);
        FunctionInputState = this.WhenValueChanged(thisViewModel => thisViewModel.SelectedFunction)
            .Select(input => _dmxValidator.IsValidFunction(input).Result);
        FeatureInputState = this.WhenValueChanged(thisViewModel => thisViewModel.SelectedFeature)
            .Select(input => _dmxValidator.IsValidFeature(input).Result);
        CancelMultipleAdds = ReactiveCommand.Create(CancelMultipleAddsCommand);
        GenerateMultipleAdds = ReactiveCommand.CreateFromTask<CancellationToken, HogRange[]?>(
            GenerateRangesCommand,
            HasValidInputValues());

        this.WhenActivated(disposables =>
        {
            NumberOfRangesState.Subscribe().DisposeWith(disposables);
            DmxStartValueState.Subscribe().DisposeWith(disposables);
            DmxOffsetState.Subscribe().DisposeWith(disposables);
            FunctionInputState.Subscribe().DisposeWith(disposables);
            FeatureInputState.Subscribe().DisposeWith(disposables);
        });
    }

    public AddMultipleRangesWindowViewModel(IDmxValidator dmxValidator, IDbManager dbManager, bool includeEndValue,
        bool is16Bit = false)
    {
        _dmxValidator = dmxValidator;
        _sqliteManager = dbManager;
        NumberOfRanges = string.Empty;
        _numberOfRanges = string.Empty;
        DmxStartValue = string.Empty;
        _dmxStartValue = string.Empty;
        DmxOffset = string.Empty;
        _dmxOffset = string.Empty;
        SelectedFunction = string.Empty;
        _selectedFunction = string.Empty;
        SelectedFeature = string.Empty;
        _selectedFeature = string.Empty;
        IncludeEndValue = includeEndValue;
        _includeEndValue = includeEndValue;
        _is16Bit = is16Bit;
        Functions = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FUNCTIONS").Result);
        _functions = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FUNCTIONS").Result);
        Features = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FEATURES").Result);
        _features = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FEATURES").Result);
        NumberOfRangesState = this.WhenAnyValue(thisViewModel => thisViewModel.NumberOfRanges,
                thisViewModel => thisViewModel.DmxOffset)
            .Select(properties => IsValidNumberOfRanges(properties.Item1, properties.Item2));
        DmxStartValueState = this.WhenAnyValue(thisViewModel => thisViewModel.DmxStartValue,
                thisViewModel => thisViewModel.NumberOfRanges,
                thisViewModel => thisViewModel.DmxOffset)
            .Select(properties => IsValidDmxStartValue(properties.Item1, properties.Item2,
                properties.Item3));
        DmxOffsetState = this.WhenAnyValue(thisViewModel => thisViewModel.DmxOffset)
            .Select(IsValidNumber);
        FunctionInputState = this.WhenValueChanged(thisViewModel => thisViewModel.SelectedFunction)
            .Select(input => _dmxValidator.IsValidFunction(input).Result);
        FeatureInputState = this.WhenValueChanged(thisViewModel => thisViewModel.SelectedFeature)
            .Select(input => _dmxValidator.IsValidFeature(input).Result);
        CancelMultipleAdds = ReactiveCommand.Create(CancelMultipleAddsCommand);
        GenerateMultipleAdds = ReactiveCommand.CreateFromTask<CancellationToken, HogRange[]?>(
            GenerateRangesCommand,
            HasValidInputValues());

        this.WhenActivated(disposables =>
        {
            NumberOfRangesState.Subscribe().DisposeWith(disposables);
            DmxStartValueState.Subscribe().DisposeWith(disposables);
            DmxOffsetState.Subscribe().DisposeWith(disposables);
            FunctionInputState.Subscribe().DisposeWith(disposables);
            FeatureInputState.Subscribe().DisposeWith(disposables);
        });
    }

    public string NumberOfRanges
    {
        get => _numberOfRanges;
        set => this.RaiseAndSetIfChanged(ref _numberOfRanges, value);
    }

    public string DmxStartValue
    {
        get => _dmxStartValue;
        set => this.RaiseAndSetIfChanged(ref _dmxStartValue, value);
    }

    public string DmxOffset
    {
        get => _dmxOffset;
        set => this.RaiseAndSetIfChanged(ref _dmxOffset, value);
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

    public string SelectedFunction
    {
        get => _selectedFunction;
        set => this.RaiseAndSetIfChanged(ref _selectedFunction, value);
    }

    public string SelectedFeature
    {
        get => _selectedFeature;
        set => this.RaiseAndSetIfChanged(ref _selectedFeature, value);
    }

    public bool IncludeEndValue
    {
        get => _includeEndValue;
        set => this.RaiseAndSetIfChanged(ref _includeEndValue, value);
    }

    public IObservable<IValidationState> NumberOfRangesState { get; }

    public IObservable<IValidationState> DmxStartValueState { get; }

    public IObservable<IValidationState> DmxOffsetState { get; }

    public IObservable<IValidationState> FunctionInputState { get; }

    public IObservable<IValidationState> FeatureInputState { get; }

    public ReactiveCommand<Unit, HogRange[]?> CancelMultipleAdds { get; }

    public ReactiveCommand<CancellationToken, HogRange[]?> GenerateMultipleAdds { get; }

    private IValidationState IsValidNumber(string? input)
    {
        if (!int.TryParse(input, out var number)) return new ValidationState(false, "Not a valid number");

        if (number < 0) return new ValidationState(false, "Enter a number greater than equal to 0");

        return new ValidationState(true, "Valid Number");
    }

    private IValidationState IsValidNumberOfRanges(string? input, string? dmxOffsetInput)
    {
        if (!int.TryParse(dmxOffsetInput, out var dmxOffset))
            return new ValidationState(false, "Enter DMX Offset first");

        var validNumberOfRanges = 0;

        if (_is16Bit)
            validNumberOfRanges = _16BitOffset / ((dmxOffset + 1) * _8BitOffset) + 1; // say what?
        else
            validNumberOfRanges = (_8BitOffset / (dmxOffset + 1)) + 1;

        if (!int.TryParse(input, out var number))
            return new ValidationState(false, $"Must enter a number between 0 and {validNumberOfRanges + 1}");

        if (number <= 0) return new ValidationState(false, "Enter a number greater than 0");

        if (number > validNumberOfRanges)
            return new ValidationState(false, $"Enter a number less than {validNumberOfRanges + 1}");

        return new ValidationState(true, "Valid");
    }

    private HogRange[]? CancelMultipleAddsCommand()
    {
        return null;
    }

    private async Task<HogRange[]?> GenerateRangesCommand(CancellationToken cancellation = default)
    {
        if (!int.TryParse(NumberOfRanges, out var numberOfRanges))
            throw new InvalidCastException("Must enter a number for number of ranges");

        if (!int.TryParse(DmxStartValue, out var dmxStart))
            throw new InvalidCastException("Must enter a number for DMX 8-Bit start value");

        if (!int.TryParse(DmxOffset, out var dmxOffset))
            throw new InvalidCastException("Must enter a number for DMX Offset");

        var ranges = new HogRange[numberOfRanges];

        await Task.Run(() =>
        {
            if (_is16Bit)
                for (var rangeNumber = 0; rangeNumber < numberOfRanges; rangeNumber++)
                {
                    HogRange? range = null;
                    // Holy shit this is a doozy! 
                    // I had to do this to prevent the app from calculating 
                    // a bad 16-Bit DMX End Value. . . silently fix the issue :P
                    // -DTL
                    var endingDmxValue = dmxStart + (dmxOffset + 1) * _8BitOffset > HogDmxValidator.Max16BitValue
                        ? HogDmxValidator.Max16BitValue
                        : dmxStart + (dmxOffset + 1) * _8BitOffset;

                    if (IncludeEndValue)
                        range = new HogRange(
                            dmxStart: dmxStart,
                            dmxEnd: endingDmxValue,
                            functionName: SelectedFunction,
                            featureName: SelectedFeature,
                            start: dmxStart,
                            end: endingDmxValue);
                    else
                        range = new HogRange(
                            dmxStart: dmxStart,
                            dmxEnd: null,
                            functionName: SelectedFunction,
                            featureName: SelectedFeature,
                            start: dmxStart,
                            end: null);

                    ranges[rangeNumber] = range;
                    dmxStart = endingDmxValue + 1 > HogDmxValidator.Max16BitValue 
                        ? HogDmxValidator.Max16BitValue 
                        : endingDmxValue + 1;
                }
            else
                for (var rangeNumber = 0; rangeNumber < numberOfRanges; rangeNumber++)
                {
                    HogRange? range = null;
                    // Holy shit this is a doozy! 
                    // I had to do this to prevent the app from calculating 
                    // a bad 8-Bit DMX End Value. . . silently fix the issue :P
                    // -DTL
                    var endingDmxValue =
                        dmxStart + dmxOffset > HogDmxValidator.Max8BitValue
                            ? HogDmxValidator.Max8BitValue
                            : dmxStart + dmxOffset; // "what an idiot" - HG

                    if (IncludeEndValue)
                        range = new HogRange(
                            dmxStart: dmxStart,
                            dmxEnd: endingDmxValue,
                            functionName: SelectedFunction,
                            featureName: SelectedFeature,
                            start: dmxStart,
                            end: endingDmxValue);
                    else
                        range = new HogRange( 
                            dmxStart: dmxStart,
                            dmxEnd: null,
                            functionName: SelectedFunction,
                            featureName: SelectedFeature,
                            start: dmxStart,
                            end: null);

                    ranges[rangeNumber] = range;
                    dmxStart = endingDmxValue + 1;
                }
        }, cancellation);

        return ranges;
    }

    private IObservable<bool> HasValidInputValues()
    {
        return this.WhenAnyValue(thisViewModel => thisViewModel.NumberOfRanges,
            thisViewModel => thisViewModel.DmxStartValue,
            thisViewModel => thisViewModel.DmxOffset,
            thisViewModel => thisViewModel.SelectedFunction,
            thisViewModel => thisViewModel.SelectedFeature,
            (numberOfRanges, dmxStartValue,
                    dmxOffset, selectedFunction, selectedFeature) =>
                IsValidNumberOfRanges(numberOfRanges, dmxOffset).IsValid
                && IsValidDmxStartValue(dmxStartValue, numberOfRanges, dmxOffset).IsValid
                && IsValidNumber(dmxOffset).IsValid
                && _dmxValidator.IsValidFunction(selectedFunction).Result.IsValid
                && _dmxValidator.IsValidFeature(selectedFeature).Result.IsValid);
    }

    private IValidationState IsValidDmxStartValue(string? dmxStartValue, string? numberOfRangesInput,
        string? dmxOffsetInput)
    {
        if (!int.TryParse(dmxStartValue, out var dmxStart))
            return new ValidationState(false, "Enter a number for DMX start");

        if (!int.TryParse(dmxOffsetInput, out var dmxOffset))
            return new ValidationState(false, "Enter a number for DMX offset");


        if (_is16Bit)
        {
            var dmxValueState = _dmxValidator.IsValid16BitDmxValue(dmxStartValue);

            if (!dmxValueState.IsValid) return dmxValueState;
        }
        else
        {
            var dmxValueState = _dmxValidator.IsValid8BitDmxValue(dmxStartValue);

            if (!dmxValueState.IsValid) return dmxValueState;
        }

        if (!int.TryParse(numberOfRangesInput, out var numberOfRanges) || numberOfRanges <= 0)
            return new ValidationState(false, "Enter a valid number of ranges first");

        var validDmxStart = 0;

        if (_is16Bit)
            validDmxStart = _16BitOffset - numberOfRanges * (dmxOffset + 1) + 2;
        else
            validDmxStart = _8BitOffset - numberOfRanges * (dmxOffset + 1) + 2;
        
        
        if (validDmxStart < 0)
            return new ValidationState(false, "Enter a DMX Start greater than or equal to 0");
        if (dmxStart < 0 || dmxStart > validDmxStart)
            return new ValidationState(false, $"Must enter a number between 0 and {validDmxStart}");

        return new ValidationState(true, "Valid");
    }
}