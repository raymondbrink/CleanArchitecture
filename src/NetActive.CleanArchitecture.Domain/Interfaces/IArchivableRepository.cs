namespace NetActive.CleanArchitecture.Domain.Interfaces
{
    using System.Linq;

    /// <summary>
    /// Interface defining an archivable entity based repository of type <see cref="T:TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    public interface IArchivableRepository<TEntity> : IArchivableRepository<TEntity, long>
        where TEntity : IEntity, IArchivableEntity, IAggregateRoot
    {
    }

    /// <summary>
    /// Interface defining an archivable entity based repository of type <see cref="T:TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TKey">Type of entity key (default: long).</typeparam>
    public interface IArchivableRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>, IArchivableEntity, IAggregateRoot
        where TKey : struct
    {
        /// <summary>
        /// Gets a queryable of <see cref="T:TEntity"/>, optionally including archived entities.
        /// </summary>
        /// <param name="includeArchived">Boolean value indicating whether archived entities should be included.</param>
        /// <param name="includes">An array of strings of '.' separated navigation property names to be included.</param>
        /// <returns>A queryable of Type <see cref="T:TEntity"/>.</returns>
        IQueryable<TEntity> All(bool includeArchived, string[] includes = null);

        /// <summary>
        /// Archives the given entity in the repository (without removing it).
        /// </summary>
        /// <param name="entity">Entity to archive.</param>
        /// <param name="by">Reference to the user who archived the entity.</param>
        void Archive(TEntity entity, string by);
    }
}