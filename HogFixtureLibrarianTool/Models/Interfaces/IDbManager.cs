namespace HogFixtureLibrarianTool.Models.Interfaces;

/// <summary>
///     An interface used to access a database. Can be used as basis for any
///     database software.
/// </summary>
public interface IDbManager
{
    /// <summary>
    ///     A function used to get an entire table from a connected database
    /// </summary>
    /// <param name="tableName">
    ///     The table name
    /// </param>
    /// <param name="cancellation">
    ///     A <see cref="CancellationToken" /> used to cancel the task after it has been started
    ///     in another thread.
    /// </param>
    /// <returns>
    ///     Returns a <see cref="List<see cref="String" />"/> containing all the information from the table
    ///     in their string form.
    /// </returns>
    Task<List<string>> GetTableAsync(string tableName, CancellationToken cancellation = default);
}