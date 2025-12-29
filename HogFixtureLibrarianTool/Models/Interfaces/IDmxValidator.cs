namespace HogFixtureLibrarianTool.Models.Interfaces;

/// <summary>
///     An interface used to
/// </summary>
public interface IDmxValidator
{
    IValidationState IsValid8BitDmxValue(string? input);

    IValidationState IsValid16BitDmxValue(string? input);

    bool TryConvertDmxStringToIntegers(HogRange range, out int? dmxStart, out int? dmxEnd, bool is16Bit = false);

    Task<IValidationState> IsValidFunction(string? input);

    Task<IValidationState> IsValidFeature(string? input);
}