namespace HogFixtureLibrarianTool.ViewModels;

public class GuidGeneratorViewModel : RoutableViewModelBase
{
    private readonly IClipboardService _clipboardManager;

    private ObservableCollection<FixtureMode> _fixtureModes;

    private string _modeName;

    private FixtureMode? _selectedFixtureMode;

    public GuidGeneratorViewModel()
    {
        _clipboardManager = new ClipboardManager();
        ModeName = string.Empty;
        _modeName = string.Empty;
        SelectedFixtureMode = default;
        _selectedFixtureMode = default;
        FixtureModes = new ObservableCollection<FixtureMode>();
        _fixtureModes = new ObservableCollection<FixtureMode>();
        GenerateGuid = ReactiveCommand.Create(GenerateGuidCommand);
        DeleteSelectedOrLastFixtureMode = ReactiveCommand.Create(DeleteSelectedOrLastFixtureModeCommand);
        DeleteAllModes = ReactiveCommand.Create(DeleteAllFixtureModesCommand);
        DeleteFixtureMode = ReactiveCommand.Create<FixtureMode>(DeleteFixtureModeCommand);
        CopyGuid = ReactiveCommand.CreateFromTask<FixtureMode>(CopyGuidAsyncCommand);
        OpenAddMultipleFixtureModesDialog =
            ReactiveCommand.CreateFromTask(OpenAddMultipleFixtureModesDialogAsyncCommand);
        AddingMultipleFixtureModesInteraction =
            new Interaction<AddMultipleFixtureModesWindowViewModel, FixtureMode[]?>();
    }

    public GuidGeneratorViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        _clipboardManager = new ClipboardManager();
        ModeName = string.Empty;
        _modeName = string.Empty;
        SelectedFixtureMode = default;
        _selectedFixtureMode = default;
        FixtureModes = new ObservableCollection<FixtureMode>();
        _fixtureModes = new ObservableCollection<FixtureMode>();
        GenerateGuid = ReactiveCommand.Create(GenerateGuidCommand);
        DeleteSelectedOrLastFixtureMode = ReactiveCommand.Create(DeleteSelectedOrLastFixtureModeCommand);
        DeleteAllModes = ReactiveCommand.Create(DeleteAllFixtureModesCommand);
        DeleteFixtureMode = ReactiveCommand.Create<FixtureMode>(DeleteFixtureModeCommand);
        CopyGuid = ReactiveCommand.CreateFromTask<FixtureMode>(CopyGuidAsyncCommand);
        OpenAddMultipleFixtureModesDialog =
            ReactiveCommand.CreateFromTask(OpenAddMultipleFixtureModesDialogAsyncCommand);
        AddingMultipleFixtureModesInteraction =
            new Interaction<AddMultipleFixtureModesWindowViewModel, FixtureMode[]?>();
    }

    public GuidGeneratorViewModel(IScreen hostScreen, IClipboardService clipboardManager)
    {
        HostScreen = hostScreen;
        _clipboardManager = clipboardManager;
        ModeName = string.Empty;
        _modeName = string.Empty;
        SelectedFixtureMode = default;
        _selectedFixtureMode = default;
        FixtureModes = new ObservableCollection<FixtureMode>();
        _fixtureModes = new ObservableCollection<FixtureMode>();
        GenerateGuid = ReactiveCommand.Create(GenerateGuidCommand);
        DeleteSelectedOrLastFixtureMode = ReactiveCommand.Create(DeleteSelectedOrLastFixtureModeCommand);
        DeleteAllModes = ReactiveCommand.Create(DeleteAllFixtureModesCommand);
        DeleteFixtureMode = ReactiveCommand.Create<FixtureMode>(DeleteFixtureModeCommand);
        CopyGuid = ReactiveCommand.CreateFromTask<FixtureMode>(CopyGuidAsyncCommand);
        OpenAddMultipleFixtureModesDialog =
            ReactiveCommand.CreateFromTask(OpenAddMultipleFixtureModesDialogAsyncCommand);
        AddingMultipleFixtureModesInteraction =
            new Interaction<AddMultipleFixtureModesWindowViewModel, FixtureMode[]?>();
    }

    public string ModeName
    {
        get => _modeName;
        set => this.RaiseAndSetIfChanged(ref _modeName, value);
    }

    public ObservableCollection<FixtureMode> FixtureModes
    {
        get => _fixtureModes;
        set => this.RaiseAndSetIfChanged(ref _fixtureModes, value);
    }

    public FixtureMode? SelectedFixtureMode
    {
        get => _selectedFixtureMode;
        set => this.RaiseAndSetIfChanged(ref _selectedFixtureMode, value);
    }

    public Interaction<AddMultipleFixtureModesWindowViewModel, FixtureMode[]?> AddingMultipleFixtureModesInteraction
    {
        get;
    }

    public ReactiveCommand<Unit, Unit> GenerateGuid { get; }

    public ReactiveCommand<Unit, Unit> DeleteSelectedOrLastFixtureMode { get; }

    public ReactiveCommand<Unit, Unit> DeleteAllModes { get; }

    public ReactiveCommand<FixtureMode, Unit> DeleteFixtureMode { get; }

    public ReactiveCommand<FixtureMode, Unit> CopyGuid { get; }

    public ReactiveCommand<Unit, Unit> OpenAddMultipleFixtureModesDialog { get; }

    private void GenerateGuidCommand()
    {
        var modeLabel = string.IsNullOrEmpty(ModeName) ? $"Mode {FixtureModes.Count}" : ModeName;
        var modeGuid = Guid.NewGuid();
        var mode = new FixtureMode(modeLabel,
            modeGuid);

        FixtureModes.Add(mode);

        ModeName = string.Empty;
    }

    private void DeleteSelectedOrLastFixtureModeCommand()
    {
        if (FixtureModes.Count == 0) return;

        if (SelectedFixtureMode != null)
            FixtureModes.Remove(SelectedFixtureMode);
        else
            FixtureModes.RemoveAt(FixtureModes.Count - 1);
    }

    private void DeleteAllFixtureModesCommand()
    {
        FixtureModes.Clear();
    }

    private void DeleteFixtureModeCommand(FixtureMode mode)
    {
        FixtureModes.Remove(mode);
    }

    private async Task CopyGuidAsyncCommand(FixtureMode mode)
    {
        await _clipboardManager.SetClipboardTextAsync(mode.Guid.ToString());
    }

    private async Task OpenAddMultipleFixtureModesDialogAsyncCommand()
    {
        var dialog = new AddMultipleFixtureModesWindowViewModel();
        var modes = await AddingMultipleFixtureModesInteraction.Handle(dialog);

        if (modes != null)
        {
            FixtureModes.Clear();

            FixtureModes = new ObservableCollection<FixtureMode>(modes);
        }
    }
}