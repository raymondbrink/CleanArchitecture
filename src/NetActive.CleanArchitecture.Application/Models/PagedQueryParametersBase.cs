namespace NetActive.CleanArchitecture.Application.Models;

using Domain.Interfaces;

using Interfaces;

/// <summary>
/// Query parameters used to apply filtering, sorting and paging to an entity query.
/// </summary>
/// <typeparam name="TEntity">Type of entity.</typeparam>
/// <typeparam name="TKey">Type of entity key.</typeparam>
/// <typeparam name="TSortModel">Type of sorting model.</typeparam>
/// <typeparam name="TFilterModel">Type of filtering model.</typeparam>
public abstract class PagedQueryParametersBase<TEntity, TKey, TSortModel, TFilterModel>
    : QueryParametersBase<TEntity, TKey, TSortModel, TFilterModel>,
        IPagedQueryParameters<TEntity, TKey, TSortModel, TFilterModel>
    where TEntity : class, IEntityBase<TKey>
    where TKey : struct
    where TFilterModel : new()
{
    /// <inheritdoc />
    public uint? PageIndex { get; set; }

    /// <inheritdoc />
    public uint? PageSize { get; set; }
}