namespace NetActive.CleanArchitecture.Application.Extensions;

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Domain.Interfaces;

using Interfaces;

using Microsoft.EntityFrameworkCore;

public static class EntityRepositoryExtensions
{
    /// <summary>
    /// Returns a boolean value indicating whether an entity with the given Id exists or not.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <param name="repository">Entity repository.</param>
    /// <param name="entityId">Entity Id to match.</param>
    /// <returns>Boolean value indicating whether an entity with the given Id exists or not.</returns>
    public static Task<bool> ExistsAsync<TEntity>(this IRepository<TEntity> repository, long entityId)
        where TEntity : class, IEntityBase
    {
        return repository.ExistsAsync(e => e.Id.Equals(entityId));
    }

    /// <summary>
    /// Returns a boolean value indicating whether an entity with the given Id exists or not.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TKey">Type of entity key.</typeparam>
    /// <param name="repository">Entity repository.</param>
    /// <param name="entityId">Entity Id to match.</param>
    /// <returns>Boolean value indicating whether an entity with the given Id exists or not.</returns>
    public static Task<bool> ExistsAsync<TEntity, TKey>(this IRepository<TEntity, TKey> repository, TKey entityId)
        where TEntity : class, IEntityBase<TKey>
        where TKey : struct
    {
        return repository.ExistsAsync(e => e.Id.Equals(entityId));
    }

    /// <summary>
    /// Returns a boolean value indicating whether an entity that complies with the given predicate exists or not.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TKey">Type of entity key.</typeparam>
    /// <param name="repository">Entity repository.</param>
    /// <param name="predicate">Predicate to match.</param>
    /// <returns>Boolean value indicating whether an entity that complies with the given predicate exists or not.</returns>
    public static Task<bool> ExistsAsync<TEntity, TKey>(this IRepository<TEntity, TKey> repository,
        Expression<Func<TEntity, bool>> predicate)
        where TEntity : class, IEntityBase<TKey>
        where TKey : struct
    {
        return repository.All().AnyAsync(predicate);
    }
}