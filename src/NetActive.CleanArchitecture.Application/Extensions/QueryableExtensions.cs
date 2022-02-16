namespace NetActive.CleanArchitecture.Application.Extensions;

using System;
using System.Linq;
using System.Linq.Expressions;

using Domain.Interfaces;

/// <summary>
/// Queryable Extensions
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Sorts the elements of a sequence according to a key.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <typeparam name="TKey">The type of the key returned by the function that is represented by keySelector.</typeparam>
    /// <param name="source">A sequence of values to order.</param>
    /// <param name="keySelector">A function to extract a key from an element.</param>
    /// <param name="descending">If set to <c>true</c>, order the elements in descending order. If set to <c>false</c>, order the elements in ascending order.</param>
    /// <returns>An System.Linq.IOrderedQueryable`1 whose elements are sorted according to a key.</returns>
    public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(
        this IQueryable<TSource> source,
        Expression<Func<TSource, TKey>> keySelector,
        bool descending)
    {
        return descending ? source.OrderByDescending(keySelector) : source.OrderBy(keySelector);
    }

    /// <summary>
    /// Performs a subsequent ordering of the elements in a sequence according to a key.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <typeparam name="TKey">The type of the key returned by the function represented by keySelector.</typeparam>
    /// <param name="source">An System.Linq.IOrderedQueryable`1 that contains elements to sort.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <param name="descending">If set to <c>true</c>, order the elements in descending order. If set to <c>false</c>, order the elements in ascending order.</param>
    /// <returns>An System.Linq.IOrderedQueryable`1 whose elements are sorted according to a key.</returns>
    public static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(
        this IOrderedQueryable<TSource> source,
        Expression<Func<TSource, TKey>> keySelector,
        bool descending)
    {
        return descending ? source.ThenByDescending(keySelector) : source.ThenBy(keySelector);
    }

    /// <summary>
    /// Applies paging to the given query using Skip and Take.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <typeparam name="TKey">Type of entity key.</typeparam>
    /// <param name="query">Query to apply paging to.</param>
    /// <param name="pageSize">Page size.</param>
    /// <param name="pageIndex">Page index.</param>
    /// <returns>Paged query.</returns>
    public static IQueryable<TEntity> GetPaged<TEntity, TKey>(
        this IOrderedQueryable<TEntity> query,
        uint pageSize,
        uint pageIndex)
        where TEntity : class, IEntity<TKey>
        where TKey : struct
    {
        var itemsToSkip = pageIndex * pageSize;
        return query.Skip((int)itemsToSkip).Take((int)pageSize);
    }
}