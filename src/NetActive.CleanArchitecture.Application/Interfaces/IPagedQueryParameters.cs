namespace NetActive.CleanArchitecture.Application.Interfaces
{
    using Domain.Interfaces;

    /// <summary>
    /// Query parameters for filtering, sorting and paging to apply to a query.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TKey">Type of entity key.</typeparam>
    /// <typeparam name="TSortModel">Sorting model.</typeparam>
    /// <typeparam name="TFilterModel">Filtering model.</typeparam>
    public interface IPagedQueryParameters<TEntity, TKey, TSortModel, TFilterModel>
        : IQueryParameters<TEntity, TKey, TSortModel, TFilterModel>
        where TEntity : class, IEntityBase<TKey>
        where TKey : struct
    {
        /// <summary>
        /// Zero based page index to return.
        /// </summary>
        uint? PageIndex { get; set; }

        /// <summary>
        /// Number of items to return.
        /// </summary>
        uint? PageSize { get; set; }
    }
}