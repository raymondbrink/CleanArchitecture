namespace NetActive.CleanArchitecture.Application.EntityFrameworkCore.Extensions
{
    using System;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    using Application.Persistence.Interfaces;

    using Domain.Interfaces;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Extensions for <see cref="IArchivableRepository{TEntity}"/> or  <see cref="IArchivableRepository{TEntity, TKey}"/>
    /// </summary>
    public static class ArchivableEntityRepositoryExtensions
    {
        /// <summary>
        /// Returns a boolean value indicating whether an entity with the given Id exists or not.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="repository">Entity repository.</param>
        /// <param name="entityId">Entity Id to match.</param>
        /// <param name="includeArchived">Boolean value indicating whether archived entities should be included.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Boolean value indicating whether an entity with the given Id exists or not.</returns>
        public static Task<bool> ExistsAsync<TEntity>(this IArchivableRepository<TEntity> repository,
            long entityId,
            bool includeArchived = false,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity, IArchivableEntity, IAggregateRoot
        {
            return repository.ExistsAsync(e => e.Id.Equals(entityId), includeArchived, cancellationToken);
        }

        /// <summary>
        /// Returns a boolean value indicating whether an entity with the given Id exists or not.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <typeparam name="TKey">Type of entity key.</typeparam>
        /// <param name="repository">Entity repository.</param>
        /// <param name="entityId">Entity Id to match.</param>
        /// <param name="includeArchived">Boolean value indicating whether archived entities should be included.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Boolean value indicating whether an entity with the given Id exists or not.</returns>
        public static Task<bool> ExistsAsync<TEntity, TKey>(this IArchivableRepository<TEntity, TKey> repository, 
            TKey entityId,
            bool includeArchived = false,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>, IArchivableEntity, IAggregateRoot
            where TKey : struct
        {
            return repository.ExistsAsync(e => e.Id.Equals(entityId), includeArchived, cancellationToken);
        }

        /// <summary>
        /// Returns a boolean value indicating whether an entity that complies with the given predicate exists or not.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <typeparam name="TKey">Type of entity key.</typeparam>
        /// <param name="repository">Entity repository.</param>
        /// <param name="predicate">Predicate to match.</param>
        /// <param name="includeArchived">Boolean value indicating whether archived entities should be included.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Boolean value indicating whether an entity that complies with the given predicate exists or not.</returns>
        public static Task<bool> ExistsAsync<TEntity, TKey>(this IArchivableRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> predicate,
            bool includeArchived = false,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>, IArchivableEntity, IAggregateRoot
            where TKey : struct
        {
            return repository.All(includeArchived).AnyAsync(predicate, cancellationToken);
        }
    }
}