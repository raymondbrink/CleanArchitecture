namespace NetActive.CleanArchitecture.Application.Models;

/// <summary>
/// Contains one page of results from an entity query.
/// </summary>
public interface IPagedQueryResultModel
{
    /// <summary>
    /// Gets the page index.
    /// </summary>
    public uint PageIndex { get; }

    /// <summary>
    /// Gets the page number.
    /// </summary>
    public uint PageNumber { get; }

    /// <summary>
    /// Gets the page size.
    /// </summary>
    public uint PageSize { get; }

    /// <summary>
    /// Gets the page count.
    /// </summary>
    public uint PageCount { get; }

    /// <summary>
    /// Gets the total number of matching entities found.
    /// </summary>
    public ulong ItemCount { get; }

}