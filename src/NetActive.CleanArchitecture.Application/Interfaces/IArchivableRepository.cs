namespace NetActive.CleanArchitecture.Application.Interfaces
{
    using Domain.Interfaces;

    /// <summary>
    /// Interface defining an archivable entity based repository of type <see cref="T:TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    public interface IArchivableRepository<TEntity> : IArchivableRepository<TEntity, long>
        where TEntity : IEntity, IArchivableEntity
    {
    }

    /// <summary>
    /// Interface defining an archivable entity based repository of type <see cref="T:TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TKey">Type of entity key (default: long).</typeparam>
    public interface IArchivableRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>, IArchivableEntity
        where TKey : struct
    {
        /// <summary>
        /// Archives the given entity in the repository (without removing it).
        /// </summary>
        /// <param name="entity">Entity to archive.</param>
        /// <param name="by">Reference to the user who archived the entity.</param>
        void Archive(TEntity entity, string by);
    }
}