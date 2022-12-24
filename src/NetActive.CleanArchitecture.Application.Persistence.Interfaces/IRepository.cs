namespace NetActive.CleanArchitecture.Application.Persistence.Interfaces
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Domain.Interfaces;

    /// <summary>
    /// Interface defining an entity based repository of type <see cref="T:TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, long>
        where TEntity : IEntity
    {
    }

    /// <summary>
    /// Interface defining an entity based repository of type <see cref="T:TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TKey">Type of entity key (default: long).</typeparam>
    public interface IRepository<TEntity, in TKey>
        where TEntity : IEntity<TKey>
        where TKey : struct
    {
        /// <summary>
        /// Gets the specified entity of <see cref="T:TEntity"/>.
        /// </summary>
        /// <param name="entityId">Id of the entity to return.</param>
        /// <param name="includes"></param>
        /// <returns>An instance of Type <see cref="T:TEntity"/>.</returns>
        Task<TEntity> GetAsync(TKey entityId, string[]? includes = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new entity in this repository, returning a new instance of <see cref="T:TEntity"/>.
        /// </summary>
        /// <returns>A new instance of Type <see cref="T:TEntity"/>.</returns>
        TEntity Create();

        /// <summary>
        /// Gets a queryable of <see cref="T:TEntity"/>.
        /// </summary>
        /// <param name="includes">An array of strings of '.' separated navigation property names to be included.</param>
        /// <returns>A queryable of Type <see cref="T:TEntity"/>.</returns>
        IQueryable<TEntity> All(string[]? includes = null);

        /// <summary>
        /// Adds the given entity to the repository.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Removes the given entity from the repository.
        /// </summary>
        /// <param name="entity">Entity to remove.</param>
        void Remove(TEntity entity);
    }
}