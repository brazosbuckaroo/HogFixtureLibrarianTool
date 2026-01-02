namespace HogFixtureLibrarianTool.ViewModels;

public class StringmapEditorViewModel : RoutableViewModelBase
{
    private readonly IClipboardService _clipboardManager;

    private readonly IConvertor _conversionManager;

    private readonly IDmxValidator _dmxValidator;

    private readonly ISettingsReaderWriter _settingsManager;

    private readonly IDbManager _sqliteManager;

    private readonly IXmlReaderWriter _xmlManager;

    private Channel? _channel;

    private string _channelXml;

    private ObservableCollection<Entry> _entries;

    private ObservableCollection<string> _features;

    private ObservableCollection<string> _functions;

    private PreviewPackages? _previewPackages;

    private string _rangeDmxStartInput;

    private string _rangeFeatureInput;

    private string _rangeFunctionInput;

    private ObservableCollection<HogRange> _ranges;

    private string _rangeStartInput;

    private Entry? _selectedEntry;

    private HogRange? _selectedRange;

    private Stringmap? _stringmap;

    private string _stringmapXml;

    public StringmapEditorViewModel()
    {
        _sqliteManager = new SqlLiteManager();
        _dmxValidator = new HogDmxValidator();
        _xmlManager = new XmlManager();
        _clipboardManager = new ClipboardManager();
        _settingsManager = new SettingsManager();
        _conversionManager = new ConversionManager();
        RangeDmxStartInput = string.Empty;
        RangeFunctionInput = string.Empty;
        RangeFeatureInput = string.Empty;
        RangeStartInput = string.Empty;
        _rangeDmxStartInput = string.Empty;
        _rangeStartInput = string.Empty;
        _rangeFunctionInput = string.Empty;
        _rangeFeatureInput = string.Empty;
        SelectedRange = null;
        _selectedRange = null;
        _channel = null;
        ChannelXml = string.Empty;
        _channelXml = string.Empty;
        SelectEntry = null;
        _selectedEntry = null;
        StringmapXml = string.Empty;
        _stringmapXml = string.Empty;
        _stringmap = null;
        _previewPackages = null;
        Functions = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FUNCTIONS").Result);
        _functions = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FUNCTIONS").Result);
        Features = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FEATURES").Result);
        _features = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FEATURES").Result);
        Ranges = [];
        _ranges = [];
        Entries = [];
        _entries = [];
        RangeDmxStartState = this.WhenValueChanged(thisViewModel => thisViewModel.RangeDmxStartInput)
            .Select(_dmxValidator.IsValid8BitDmxValue);
        RangeStartState = this.WhenValueChanged(thisViewModel => thisViewModel.RangeStartInput)
            .Select(_dmxValidator.IsValid8BitDmxValue);
        RangeFunctionState = this.WhenValueChanged(thisViewModel => thisViewModel.RangeFunctionInput)
            .Select(input => _dmxValidator.IsValidFunction(input).Result);
        RangeFeatureState = this.WhenValueChanged(thisViewModel => thisViewModel.RangeFeatureInput)
            .Select(input => _dmxValidator.IsValidFeature(input).Result);
        AddMultipleRangesInteraction = new Interaction<AddMultipleRangesWindowViewModel, HogRange[]?>();
        CreateRangeAndEntry = ReactiveCommand.Create(CreateRangeAndEntryCommand,
            IsValidRangeInput());
        DeleteSelectedOrLastRange = ReactiveCommand.Create(DeleteLastOrSelectedCommand);
        DeleteAllRanges = ReactiveCommand.Create(DeleteAllCommand);
        DeleteRange = ReactiveCommand.Create<HogRange>(DeleteRangeCommand);
        GenerateXml = ReactiveCommand.CreateFromTask(GenerateXmlAsyncCommand);
        DeleteEntry = ReactiveCommand.Create<Entry>(DeleteEntryCommand);
        CopyXmlText = ReactiveCommand.CreateFromTask<bool>(CopyXmlTextAsyncCommand);
        AddMultipleRanges = ReactiveCommand.CreateFromTask(AddMultipleRangesAsyncCommand);

        this.WhenActivated(disposables =>
        {
            RangeDmxStartState.Subscribe().DisposeWith(disposables);
            RangeFeatureState.Subscribe().DisposeWith(disposables);
            RangeFunctionState.Subscribe().DisposeWith(disposables);
        });
    }

    public StringmapEditorViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        _sqliteManager = new SqlLiteManager();
        _dmxValidator = new HogDmxValidator();
        _xmlManager = new XmlManager();
        _clipboardManager = new ClipboardManager();
        _settingsManager = new SettingsManager();
        _conversionManager = new ConversionManager();
        RangeDmxStartInput = string.Empty;
        RangeFunctionInput = string.Empty;
        RangeFeatureInput = string.Empty;
        RangeStartInput = string.Empty;
        _rangeDmxStartInput = string.Empty;
        _rangeStartInput = string.Empty;
        _rangeFunctionInput = string.Empty;
        _rangeFeatureInput = string.Empty;
        SelectedRange = null;
        _selectedRange = null;
        _channel = null;
        ChannelXml = string.Empty;
        _channelXml = string.Empty;
        SelectEntry = null;
        _selectedEntry = null;
        StringmapXml = string.Empty;
        _stringmapXml = string.Empty;
        _stringmap = null;
        _previewPackages = null;
        Functions = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FUNCTIONS").Result);
        _functions = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FUNCTIONS").Result);
        Features = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FEATURES").Result);
        _features = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FEATURES").Result);
        Ranges = [];
        _ranges = [];
        Entries = [];
        _entries = [];
        RangeDmxStartState = this.WhenValueChanged(thisViewModel => thisViewModel.RangeDmxStartInput)
            .Select(_dmxValidator.IsValid8BitDmxValue);
        RangeStartState = this.WhenValueChanged(thisViewModel => thisViewModel.RangeStartInput)
            .Select(_dmxValidator.IsValid8BitDmxValue);
        RangeFunctionState = this.WhenValueChanged(thisViewModel => thisViewModel.RangeFunctionInput)
            .Select(input => _dmxValidator.IsValidFunction(input).Result);
        RangeFeatureState = this.WhenValueChanged(thisViewModel => thisViewModel.RangeFeatureInput)
            .Select(input => _dmxValidator.IsValidFeature(input).Result);
        AddMultipleRangesInteraction = new Interaction<AddMultipleRangesWindowViewModel, HogRange[]?>();
        CreateRangeAndEntry = ReactiveCommand.Create(CreateRangeAndEntryCommand,
            IsValidRangeInput());
        DeleteSelectedOrLastRange = ReactiveCommand.Create(DeleteLastOrSelectedCommand);
        DeleteAllRanges = ReactiveCommand.Create(DeleteAllCommand);
        DeleteRange = ReactiveCommand.Create<HogRange>(DeleteRangeCommand);
        GenerateXml = ReactiveCommand.CreateFromTask(GenerateXmlAsyncCommand);
        DeleteEntry = ReactiveCommand.Create<Entry>(DeleteEntryCommand);
        CopyXmlText = ReactiveCommand.CreateFromTask<bool>(CopyXmlTextAsyncCommand);
        AddMultipleRanges = ReactiveCommand.CreateFromTask(AddMultipleRangesAsyncCommand);

        this.WhenActivated(disposables =>
        {
            RangeDmxStartState.Subscribe().DisposeWith(disposables);
            RangeFeatureState.Subscribe().DisposeWith(disposables);
            RangeFunctionState.Subscribe().DisposeWith(disposables);
        });
    }

    public StringmapEditorViewModel(IScreen hostScreen,
        IDbManager dbManager,
        IDmxValidator dmxValidator,
        IXmlReaderWriter xmlManager,
        IClipboardService clipboardManager,
        ISettingsReaderWriter settingsManager,
        IConvertor conversionManager)
    {
        HostScreen = hostScreen;
        _sqliteManager = dbManager;
        _dmxValidator = dmxValidator;
        _xmlManager = xmlManager;
        _clipboardManager = clipboardManager;
        _settingsManager = settingsManager;
        _conversionManager = conversionManager;
        RangeDmxStartInput = string.Empty;
        RangeFunctionInput = string.Empty;
        RangeFeatureInput = string.Empty;
        RangeStartInput = string.Empty;
        _rangeDmxStartInput = string.Empty;
        _rangeStartInput = string.Empty;
        _rangeFunctionInput = string.Empty;
        _rangeFeatureInput = string.Empty;
        SelectedRange = null;
        _selectedRange = null;
        _channel = null;
        ChannelXml = string.Empty;
        _channelXml = string.Empty;
        SelectEntry = null;
        _selectedEntry = null;
        StringmapXml = string.Empty;
        _stringmapXml = string.Empty;
        _stringmap = null;
        _previewPackages = null;
        Functions = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FUNCTIONS").Result);
        _functions = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FUNCTIONS").Result);
        Features = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FEATURES").Result);
        _features = new ObservableCollection<string>(_sqliteManager.GetTableAsync("FEATURES").Result);
        Ranges = [];
        _ranges = [];
        Entries = [];
        _entries = [];
        RangeDmxStartState = this.WhenValueChanged(thisViewModel => thisViewModel.RangeDmxStartInput)
            .Select(_dmxValidator.IsValid8BitDmxValue);
        RangeStartState = this.WhenValueChanged(thisViewModel => thisViewModel.RangeStartInput)
            .Select(_dmxValidator.IsValid8BitDmxValue);
        RangeFunctionState = this.WhenValueChanged(thisViewModel => thisViewModel.RangeFunctionInput)
            .Select(input => _dmxValidator.IsValidFunction(input).Result);
        RangeFeatureState = this.WhenValueChanged(thisViewModel => thisViewModel.RangeFeatureInput)
            .Select(input => _dmxValidator.IsValidFeature(input).Result);
        AddMultipleRangesInteraction = new Interaction<AddMultipleRangesWindowViewModel, HogRange[]?>();
        CreateRangeAndEntry = ReactiveCommand.Create(CreateRangeAndEntryCommand,
            IsValidRangeInput());
        DeleteSelectedOrLastRange = ReactiveCommand.Create(DeleteLastOrSelectedCommand);
        DeleteAllRanges = ReactiveCommand.Create(DeleteAllCommand);
        DeleteRange = ReactiveCommand.Create<HogRange>(DeleteRangeCommand);
        GenerateXml = ReactiveCommand.CreateFromTask(GenerateXmlAsyncCommand);
        DeleteEntry = ReactiveCommand.Create<Entry>(DeleteEntryCommand);
        CopyXmlText = ReactiveCommand.CreateFromTask<bool>(CopyXmlTextAsyncCommand);
        AddMultipleRanges = ReactiveCommand.CreateFromTask(AddMultipleRangesAsyncCommand);

        this.WhenActivated(disposables =>
        {
            RangeDmxStartState.Subscribe().DisposeWith(disposables);
            RangeFeatureState.Subscribe().DisposeWith(disposables);
            RangeFunctionState.Subscribe().DisposeWith(disposables);
        });
    }

    public string RangeDmxStartInput
    {
        get => _rangeDmxStartInput;
        set
        {
            // making an assumption that the DMX Start
            // and Start value need to be the same
            // hopefully there is a better way
            if (RangeDmxStartInput == RangeStartInput) RangeStartInput = value;

            this.RaiseAndSetIfChanged(ref _rangeDmxStartInput, value);
        }
    }

    public string RangeFunctionInput
    {
        get => _rangeFunctionInput;
        set => this.RaiseAndSetIfChanged(ref _rangeFunctionInput, value);
    }

    public string RangeFeatureInput
    {
        get => _rangeFeatureInput;
        set => this.RaiseAndSetIfChanged(ref _rangeFeatureInput, value);
    }

    public string RangeStartInput
    {
        get => _rangeStartInput;
        set => this.RaiseAndSetIfChanged(ref _rangeStartInput, value);
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

    public ObservableCollection<HogRange> Ranges
    {
        get => _ranges;
        set => this.RaiseAndSetIfChanged(ref _ranges, value);
    }

    public ObservableCollection<Entry> Entries
    {
        get => _entries;
        set => this.RaiseAndSetIfChanged(ref _entries, value);
    }

    public HogRange? SelectedRange
    {
        get => _selectedRange;
        set => this.RaiseAndSetIfChanged(ref _selectedRange, value);
    }

    public string ChannelXml
    {
        get => _channelXml;
        set => this.RaiseAndSetIfChanged(ref _channelXml, value);
    }

    public Entry? SelectEntry
    {
        get => _selectedEntry;
        set => this.RaiseAndSetIfChanged(ref _selectedEntry, value);
    }

    public string StringmapXml
    {
        get => _stringmapXml;
        set => this.RaiseAndSetIfChanged(ref _stringmapXml, value);
    }

    public Interaction<AddMultipleRangesWindowViewModel, HogRange[]?> AddMultipleRangesInteraction { get; }

    public IObservable<IValidationState> RangeDmxStartState { get; }

    public IObservable<IValidationState> RangeStartState { get; }

    public IObservable<IValidationState> RangeFunctionState { get; }

    public IObservable<IValidationState> RangeFeatureState { get; }

    public ReactiveCommand<Unit, Unit> CreateRangeAndEntry { get; }

    public ReactiveCommand<Unit, Unit> DeleteSelectedOrLastRange { get; }

    public ReactiveCommand<Unit, Unit> DeleteAllRanges { get; }

    public ReactiveCommand<HogRange, Unit> DeleteRange { get; }

    public ReactiveCommand<Unit, Unit> GenerateXml { get; }

    public ReactiveCommand<Entry, Unit> DeleteEntry { get; }

    public ReactiveCommand<bool, Unit> CopyXmlText { get; }

    public ReactiveCommand<Unit, Unit> AddMultipleRanges { get; }

    private void CreateRangeAndEntryCommand()
    {
        if (!_dmxValidator.IsValid8BitDmxValue(RangeDmxStartInput).IsValid)
            throw new ArgumentException("Range DMX Start is not a valid DMX value");

        if (!_dmxValidator.IsValid8BitDmxValue(RangeStartInput).IsValid)
            throw new ArgumentException("Range Start is not a valid DMX value");

        var newRange = new HogRange(RangeDmxStartInput,
            RangeFunctionInput,
            RangeFeatureInput,
            RangeStartInput);
        var newEntry = CreateEntry(newRange);

        Ranges.Add(newRange);
        Entries.Add(newEntry);

        RangeDmxStartInput = string.Empty;
        RangeFunctionInput = string.Empty;
        RangeFeatureInput = string.Empty;
        RangeStartInput = string.Empty;
    }

    private void DeleteRangeCommand(HogRange range)
    {
        Ranges.Remove(range);
    }

    private void DeleteEntryCommand(Entry entry)
    {
        Entries.Remove(entry);
    }

    private void DeleteLastOrSelectedCommand()
    {
        if (Entries.Count == 0 || Ranges.Count == 0) return;

        if (SelectedRange == null)
            Ranges.RemoveAt(Ranges.Count - 1);
        else
            Ranges.Remove(SelectedRange);

        if (SelectEntry == null)
            Entries.RemoveAt(Entries.Count - 1);
        else
            Entries.Remove(SelectEntry);
    }

    private void DeleteAllCommand()
    {
        Ranges.Clear();
        Entries.Clear();
    }

    private async Task GenerateXmlAsyncCommand()
    {
        if (Ranges.Count != 0 || Entries.Count != 0)
        {
            var groupedRanges = Ranges.DistinctBy(range => (range.Function, range.Feature));
            _channel = new Channel(0, Ranges.ToList());
            ChannelXml = await _xmlManager.SerializeDataAsync(_channel);
            var previews = new List<FunctionPreview>();

            foreach (var range in groupedRanges)
            {
                var func = new FunctionReference(range.FunctionName, range.FeatureName);
                var definition =
                    new Definition(range.FunctionName == null ? string.Empty : range.FunctionName.ToLower(), func);
                var preview = new FunctionPreview(definition,
                    Entries.Where(entry => AreRangesTheSame(entry, range))
                        .ToList());

                previews.Add(preview);
            }

            if (_settingsManager.ApplicationSettings.UseHogV3Xml)
            {
                var package = new Package("stringmaps", previews);
                _previewPackages = new PreviewPackages();

                _previewPackages.Packages.Add(package);

                StringmapXml = await _xmlManager.SerializeDataAsync(_previewPackages);
            }
            else
            {
                _stringmap = new Stringmap(previews);
                StringmapXml = await _xmlManager.SerializeDataAsync(_stringmap);
            }
        }
    }

    private async Task CopyXmlTextAsyncCommand(bool copyChannelXml)
    {
        if (copyChannelXml)
            await _clipboardManager.SetClipboardTextAsync(ChannelXml);
        else
            await _clipboardManager.SetClipboardTextAsync(StringmapXml);
    }

    private async Task AddMultipleRangesAsyncCommand()
    {
        var dialog = new AddMultipleRangesWindowViewModel(_dmxValidator, _sqliteManager, false);
        var newRanges = await AddMultipleRangesInteraction.Handle(dialog);

        if (newRanges != null)
        {
            Ranges.Clear();
            Entries.Clear();

            Ranges = new ObservableCollection<HogRange>(newRanges);

            foreach (var range in newRanges) Entries.Add(CreateEntry(range));
        }
    }

    private bool AreRangesTheSame(Entry entry, HogRange rangeToCompare)
    {
        if (entry.Range.Function == rangeToCompare.Function
            && entry.Range.Feature == rangeToCompare.Feature)
            return true;

        return false;
    }

    private IObservable<bool> IsValidRangeInput()
    {
        return this.WhenAnyValue(thisViewModel => thisViewModel.RangeDmxStartInput,
            thisViewModel => thisViewModel.RangeStartInput,
            thisViewModel => thisViewModel.RangeFunctionInput,
            thisViewModel => thisViewModel.RangeFeatureInput,
            (rangeDmxStart, rangeStart,
                    rangeFunction, rangeFeature) => _dmxValidator.IsValid8BitDmxValue(rangeDmxStart).IsValid
                                                    && _dmxValidator.IsValid8BitDmxValue(rangeStart).IsValid
                                                    && _dmxValidator.IsValidFunction(rangeFunction).Result.IsValid
                                                    && _dmxValidator.IsValidFeature(rangeFeature).Result.IsValid);
    }

    private Entry CreateEntry(HogRange range)
    {
        var filteredEntries = Entries.Where(entry => AreRangesTheSame(entry, range));
        var newEntry = new Entry(range.DmxStart,
            $"function {_conversionManager.ConvertArabNumeralToString(filteredEntries.Count())}", range);

        return newEntry;
    }
}