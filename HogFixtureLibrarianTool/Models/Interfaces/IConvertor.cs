namespace HogFixtureLibrarianTool.Models.Interfaces;

/// <summary>
///     An interface used for any special conversions the application
///     needs to handle.
/// </summary>
public interface IConvertor
{
    /// <summary>
    ///     A function used to convert arabic numerals to their
    ///     string equivalents
    /// </summary>
    /// <param name="number">
    ///     An <see cref="int" /> to convert from its arabic numeral to a string.
    /// </param>
    /// <returns>
    ///     A string representation of the provided arabic numeral.
    /// </returns>
    string ConvertArabNumeralToString(int number);
}