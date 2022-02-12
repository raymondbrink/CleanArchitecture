namespace NetActive.CleanArchitecture.Application.Interfaces;

using AutoMapper;

using Domain.Interfaces;

using Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

/// <summary>
/// Base service interface that can be used to query the given model's entity repository. 
/// </summary>
/// <typeparam name="TModel">Type of model to output.</typeparam>
/// <typeparam name="TEntity">Type of entity to query.</typeparam>
/// <typeparam name="TKey">Type of entity key.</typeparam>
public interface IEntityQueryService<TEntity, TModel, TKey>
    where TEntity : class, IEntityBase<TKey>
    where TModel : class, IModel<TKey>
    where TKey : struct
{
    internal IQueryable<TEntity> GetQuery();

    internal IMapper Mapper { get; }

    /// <summary>
    /// Gets the model for the entity with the specified identifier.
    /// </summary>
    /// <param name="entityId">The entity identifier.</param>
    /// <returns>
    /// Model.
    /// </returns>
    Task<TModel> GetAsync(TKey entityId);

    /// <summary>
    /// Gets the single model for the entity that meets the given filter function.
    /// </summary>
    /// <param name="where">Filtering based on a filter function.</param>
    /// <returns>Model.</returns>
    Task<TModel> GetAsync(Expression<Func<TEntity, bool>> where);

    /// <summary>
    /// Gets the model for the entity with the specified identifier. Throws an EntityNotFoundException, if the entity wasn't found.
    /// </summary>
    /// <param name="entityId">The entity identifier.</param>
    /// <returns>
    /// Model.
    /// </returns>
    Task<TModel> ReadAsync(TKey entityId);

    /// <summary>
    /// Gets the single model for the entity that meets the given filter function. Throws an EntityNotFoundException, if the entity wasn't found.
    /// </summary>
    /// <param name="where">Filtering based on a filter function.</param>
    /// <returns>Model.</returns>
    Task<TModel> ReadAsync(Expression<Func<TEntity, bool>> where);

    /// <summary>
    /// Gets a boolean value indicating whether an entity with the given Id exists.
    /// </summary>
    /// <param name="id">Id of the entity to find.</param>
    /// <returns>Boolean.</returns>
    Task<bool> ExistsAsync(TKey id);

    /// <summary>
    /// Gets a boolean value indicating whether at least one entity exists, that complies to the (optional) given filter.
    /// </summary>
    /// <param name="where">Filtering based on a filter function.</param>
    /// <returns>Boolean.</returns>
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> where);

    /// <summary>
    /// Gets one page of results for the given query, sorted by entity Id ascending.
    /// </summary>
    /// <param name="pageSize">Number of items requested per page.</param>
    /// <param name="pageIndex">Zero-based page index.</param>
    /// <param name="where">Optional filtering based on a filter function.</param>
    /// <returns>Paged query result.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    Task<PagedQueryResultModel<TModel>> GetPageOfItemsAsync(
        Expression<Func<TEntity, bool>> where = null,
        uint pageIndex = 0,
        uint pageSize = Constants.DefaultPageSize);

    /// <summary>
    /// Gets one page of results for the given query.
    /// </summary>
    /// <param name="pageSize">Number of items requested per page.</param>
    /// <param name="pageIndex">Zero-based page index.</param>
    /// <param name="orderBy">Optional sorting according to a key (default: by entity Id).</param>
    /// <param name="orderDescending">Sorting direction, default value: false, meaning ASCENDING order)</param>
    /// <returns>Paged query result.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    Task<PagedQueryResultModel<TModel>> GetPageOfItemsAsync(
        Expression<Func<TEntity, object>> orderBy,
        bool orderDescending = false,
        uint pageIndex = 0,
        uint pageSize = Constants.DefaultPageSize);

    /// <summary>
    /// Gets one page of results for the given query.
    /// </summary>
    /// <param name="pageSize">Number of items requested per page.</param>
    /// <param name="pageIndex">Zero-based page index.</param>
    /// <param name="where">Optional filtering based on a filter function.</param>
    /// <param name="orderBy">Optional sorting according to a key (default: by entity Id).</param>
    /// <param name="orderDescending">Sorting direction, default value: false, meaning ASCENDING order)</param>
    /// <returns>Paged query result.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    Task<PagedQueryResultModel<TModel>> GetPageOfItemsAsync(
        Expression<Func<TEntity, bool>> where,
        Expression<Func<TEntity, object>> orderBy,
        bool orderDescending = false,
        uint pageIndex = 0,
        uint pageSize = Constants.DefaultPageSize);

    /// <summary>
    /// Gets results for the given query, sorted by entity Id ascending.
    /// </summary>
    /// <param name="where">Optional filtering based on a filter function.</param>
    /// <returns>List of items.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    Task<List<TModel>> GetItemsAsync(Expression<Func<TEntity, bool>> where = null);

    /// <summary>
    /// Gets results for the given query.
    /// </summary>
    /// <param name="orderBy">Optional sorting according to a key (default: by entity Id).</param>
    /// <param name="orderDescending">Sorting direction, default value: false, meaning ASCENDING order)</param>
    /// <returns>List of items.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    Task<List<TModel>> GetItemsAsync(Expression<Func<TEntity, TKey>> orderBy,
        bool orderDescending = false);

    /// <summary>
    /// Gets results for the given query.
    /// </summary>
    /// <param name="where">Filtering based on a filter function.</param>
    /// <param name="orderBy">Optional sorting according to a key (default: by entity Id).</param>
    /// <param name="orderDescending">Sorting direction, default value: false, meaning ASCENDING order)</param>
    /// <returns>List of items.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    Task<List<TModel>> GetItemsAsync(Expression<Func<TEntity, bool>> where,
        Expression<Func<TEntity, object>> orderBy,
        bool orderDescending = false);
}