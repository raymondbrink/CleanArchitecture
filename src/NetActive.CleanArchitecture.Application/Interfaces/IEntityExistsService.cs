namespace NetActive.CleanArchitecture.Application.Interfaces
{
    using System;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    using Domain.Interfaces;

    public interface IEntityExistsService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : struct
    {

        /// <summary>
        /// Gets a boolean value indicating whether an entity with the given Id exists.
        /// </summary>
        /// <param name="id">Id of the entity to find.</param>
        /// <returns>Boolean.</returns>
        Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a boolean value indicating whether at least one entity exists, that complies to the (optional) given filter.
        /// </summary>
        /// <param name="where">Filtering based on a filter function.</param>
        /// <returns>Boolean.</returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);
    }
}
