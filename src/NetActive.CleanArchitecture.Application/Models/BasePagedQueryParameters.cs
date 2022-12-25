namespace NetActive.CleanArchitecture.Application.Models
{
    using Domain.Interfaces;

    using Interfaces;

    /// <summary>
    /// Abstract base for generic query parameters used to apply filtering, sorting and paging to an entity query.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TSortModel">Type of sorting model.</typeparam>
    /// <typeparam name="TFilterModel">Type of filtering model.</typeparam>
    public abstract class BasePagedQueryParameters<TEntity, TSortModel, TFilterModel>
        : BasePagedQueryParameters<TEntity, long, TSortModel, TFilterModel>
        where TEntity : class, IEntity
        where TFilterModel : new()
    {
    }

    /// <summary>
    /// Abstract base for generic query parameters used to apply filtering, sorting and paging to an entity query.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TKey">Type of entity key.</typeparam>
    /// <typeparam name="TSortModel">Type of sorting model.</typeparam>
    /// <typeparam name="TFilterModel">Type of filtering model.</typeparam>
    public abstract class BasePagedQueryParameters<TEntity, TKey, TSortModel, TFilterModel>
        : BaseQueryParameters<TEntity, TKey, TSortModel, TFilterModel>,
            IPagedQueryParameters<TEntity, TKey, TSortModel, TFilterModel>
        where TEntity : class, IEntity<TKey>
        where TKey : struct
        where TFilterModel : new()
    {
        /// <inheritdoc />
        public uint PageIndex { get; set; }

        /// <inheritdoc />
        public uint? PageSize { get; set; }
    }
}