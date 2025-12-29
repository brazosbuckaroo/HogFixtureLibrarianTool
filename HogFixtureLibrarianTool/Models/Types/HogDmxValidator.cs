namespace HogFixtureLibrarianTool.Models.Types;

public class HogDmxValidator : IDmxValidator
{
    private readonly IDbManager _sqliteManager = new SqlLiteManager();

    public static int Max8BitValue => 255;

    public static int Max16BitValue => 65535;

    public static int MinValue => 0;

    public static string NoValue => "NOVALUE";

    public static string Disallowed => "DISALLOWED";

    public IValidationState IsValid8BitDmxValue(string? userInput)
    {
        if (userInput == null) return new ValidationState(false, "Must provide a real value");

        // special cases for HOG
        if (userInput == Disallowed || userInput == NoValue) return new ValidationState(true, "Valid");

        if (!int.TryParse(userInput, out var number)) return new ValidationState(false, "Must be a number");

        if (number > Max8BitValue || number < MinValue)
            return new ValidationState(false, $"Must be a number between {MinValue} thru {Max8BitValue}");

        return new ValidationState(true, "Valid");
    }

    public IValidationState IsValid16BitDmxValue(string? userInput)
    {
        if (userInput == null) return new ValidationState(false, "Must provide a real value");

        // special case for HOG
        if (userInput == NoValue || userInput == Disallowed) return new ValidationState(true, "Valid");

        if (!int.TryParse(userInput, out var number)) return new ValidationState(false, "Must be a number");

        if (number > Max16BitValue || number < MinValue)
            return new ValidationState(false, $"Must be a number between {MinValue} thru {Max16BitValue}");

        return new ValidationState(true, "Valid");
    }

    public bool TryConvertDmxStringToIntegers(Range range, out int? dmxStart, out int? dmxEnd,
        bool is16Bit = false)
    {
        dmxStart = null;
        dmxEnd = null;

        // special case for HOG
        if (range.DmxStart == NoValue || range.DmxStart == Disallowed || range.DmxEnd == NoValue ||
            range.DmxEnd == Disallowed)
            return true;

        if (!int.TryParse(range.DmxStart, out var parsedDmxStart)) return false;

        // special case to allow no value for DmxEnd
        if (!int.TryParse(range.DmxEnd, out var parsedDmxEnd) && dmxEnd != null) return false;

        if (is16Bit)
        {
            if (parsedDmxStart < MinValue || parsedDmxStart > Max16BitValue) return false;

            // special case to allow for null for DmxEnd
            if (range.DmxEnd != null && (parsedDmxEnd < MinValue || parsedDmxEnd > Max16BitValue)) return false;
        }
        else
        {
            if (parsedDmxStart < MinValue || parsedDmxStart > Max8BitValue) return false;

            // special case to allow for null for DmxEnd
            if (range.DmxEnd != null && (parsedDmxEnd < MinValue || parsedDmxEnd > Max8BitValue)) return false;
        }

        dmxStart = parsedDmxStart;
        dmxEnd = parsedDmxEnd;

        return true;
    }

    public async Task<IValidationState> IsValidFunction(string? input)
    {
        if (string.IsNullOrEmpty(input)) return new ValidationState(false, "Must have a value");

        var functions = await _sqliteManager.GetTableAsync("FUNCTIONS");

        if (!functions.Contains(input)) return new ValidationState(false, "Must be a valid function");

        return new ValidationState(true, "Valid Function");
    }

    public async Task<IValidationState> IsValidFeature(string? input)
    {
        if (string.IsNullOrEmpty(input)) return new ValidationState(false, "Must have a value");

        var features = await _sqliteManager.GetTableAsync("FEATURES");

        if (!features.Contains(input)) return new ValidationState(false, "Must be a valid feature");

        return new ValidationState(true, "Valid Feature");
    }
}