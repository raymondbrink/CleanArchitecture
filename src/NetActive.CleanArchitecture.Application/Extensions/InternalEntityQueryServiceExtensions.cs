namespace NetActive.CleanArchitecture.Application.Extensions;

using System.Linq;

using Domain.Interfaces;

/// <summary>
/// Internal extension methods for IEntityQueryService.
/// </summary>
internal static class InternalEntityQueryServiceExtensions
{
    internal static IQueryable<TEntity> GetPaged<TEntity, TKey>(
        this IOrderedQueryable<TEntity> query,
        uint pageSize,
        uint pageIndex)
        where TEntity : class, IEntityBase<TKey>
        where TKey : struct
    {
        var itemsToSkip = pageIndex * pageSize;
        return query.Skip((int)itemsToSkip).Take((int)pageSize);
    }
}