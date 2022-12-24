namespace NetActive.CleanArchitecture.Persistence.EntityFrameworkCore.Extensions
{
    using System;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    using Application.Persistence.Interfaces;

    using Domain.Interfaces;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Extensions for <see cref="IRepository{TEntity}"/> or  <see cref="IRepository{TEntity, TKey}"/>
    /// </summary>
    public static class EntityRepositoryExtensions
    {
        /// <summary>
        /// Returns a boolean value indicating whether an entity with the given Id exists or not.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="repository">Entity repository.</param>
        /// <param name="entityId">Entity Id to match.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Boolean value indicating whether an entity with the given Id exists or not.</returns>
        public static Task<bool> ExistsAsync<TEntity>(this IRepository<TEntity> repository,
            long entityId,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity
        {
            return repository.ExistsAsync(e => e.Id.Equals(entityId), cancellationToken);
        }

        /// <summary>
        /// Returns a boolean value indicating whether an entity with the given Id exists or not.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <typeparam name="TKey">Type of entity key.</typeparam>
        /// <param name="repository">Entity repository.</param>
        /// <param name="entityId">Entity Id to match.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Boolean value indicating whether an entity with the given Id exists or not.</returns>
        public static Task<bool> ExistsAsync<TEntity, TKey>(this IRepository<TEntity, TKey> repository, 
            TKey entityId,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>
            where TKey : struct
        {
            return repository.ExistsAsync(e => e.Id.Equals(entityId), cancellationToken);
        }

        /// <summary>
        /// Returns a boolean value indicating whether an entity that complies with the given predicate exists or not.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <typeparam name="TKey">Type of entity key.</typeparam>
        /// <param name="repository">Entity repository.</param>
        /// <param name="predicate">Predicate to match.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Boolean value indicating whether an entity that complies with the given predicate exists or not.</returns>
        public static Task<bool> ExistsAsync<TEntity, TKey>(this IRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>
            where TKey : struct
        {
            return repository.All().AnyAsync(predicate, cancellationToken);
        }
    }
}