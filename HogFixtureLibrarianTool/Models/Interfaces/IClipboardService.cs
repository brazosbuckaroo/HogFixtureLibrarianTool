namespace HogFixtureLibrarianTool.Models.Interfaces;

/// <summary>
///     An interface used to access the operating system's
///     clipboard. This should allow getting what is in the clipboard
///     and set the copied text in the clipboard.
/// </summary>
public interface IClipboardService
{
    /// <summary>
    ///     A function used to set the operating system's clipboard text to the
    ///     provided string.
    /// </summary>
    /// <param name="textToCopy">
    ///     The string to copy to the operating system's clipboard.
    /// </param>
    /// <returns>
    ///     Returns a <see cref="Task" />
    /// </returns>
    Task SetClipboardTextAsync(string? textToCopy);

    /// <summary>
    ///     A function used to get what is currently in the operating system's
    ///     clipboard. Allow for pasting in the application.
    /// </summary>
    /// <returns>
    ///     Returns a <see cref="Task" />
    /// </returns>
    Task GetClipboardTextAsync();
}