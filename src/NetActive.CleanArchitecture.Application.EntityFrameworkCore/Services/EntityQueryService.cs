namespace NetActive.CleanArchitecture.Application.EntityFrameworkCore.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Domain.Interfaces;

using Exceptions;

using Extensions;

using Interfaces;

using Microsoft.EntityFrameworkCore;

using Models;

/// <summary>
/// Service class that can be used to query the given model's entity repository. 
/// </summary>
/// <typeparam name="TEntity">Type of entity.</typeparam>
/// <typeparam name="TModel">Type of model returned.</typeparam>
public class EntityQueryService<TEntity, TModel> 
    : EntityQueryService<TEntity, TModel, long>, IEntityQueryService<TEntity, TModel>
    where TEntity : class, IEntity<long>
    where TModel : class, IModel<long>
{
    public EntityQueryService(IRepository<TEntity, long> repo, IMapper mapper) 
        : base(repo, mapper)
    {
    }
}

/// <summary>
/// Service class that can be used to query the given model's entity repository. 
/// </summary>
/// <typeparam name="TEntity">Type of entity.</typeparam>
/// <typeparam name="TModel">Type of model returned.</typeparam>
/// <typeparam name="TKey">Type of entity key.</typeparam>
public class EntityQueryService<TEntity, TModel, TKey> 
    : IEntityQueryService<TEntity, TModel, TKey>
    where TEntity : class, IEntity<TKey>
    where TModel : class, IModel<TKey>
    where TKey : struct
{
    private readonly IMapper _mapper;

    private readonly IRepository<TEntity, TKey> _repo;

    /// <summary>
    /// Constructor used to instantiate an <see cref="EntityQueryService{TEntity,TModel,TKey}"/>.
    /// </summary>
    /// <param name="repo"></param>
    /// <param name="mapper"></param>
    public EntityQueryService(IRepository<TEntity, TKey> repo, IMapper mapper)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    IMapper IEntityQueryService<TEntity, TModel, TKey>.Mapper => _mapper;

    /// <inheritdoc />
    public Task<TModel> GetAsync(TKey entityId, string[] includes = null) => GetAsync(e => e.Id.Equals(entityId));

    /// <inheritdoc />
    public async Task<TModel> GetAsync(Expression<Func<TEntity, bool>> where, string[] includes = null)
    {
        if (where == null)
        {
            throw new ArgumentNullException(nameof(where));
        }

        var query = getQuery(where, includes);
        var result = await query.SingleOrDefaultAsync();

        return _mapper.Map<TModel>(result);
    }

    /// <inheritdoc />
    public Task<TModel> ReadAsync(TKey entityId, string[] includes = null) => ReadAsync(e => e.Id.Equals(entityId));

    /// <inheritdoc />
    public async Task<TModel> ReadAsync(Expression<Func<TEntity, bool>> where, string[] includes = null)
    {
        var model = await GetAsync(where, includes);
        return model ?? throw new EntityNotFoundException(typeof(TEntity));
    }

    /// <inheritdoc />
    public Task<bool> ExistsAsync(TKey id) => ExistsAsync(e => e.Id.Equals(id));

    /// <inheritdoc />
    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> where = null)
    {
        var query = _repo.All();
        return where == null ? query.AnyAsync() : query.Where(where).AnyAsync();
    }

    public Task<PagedQueryResultModel<TModel>> GetPageOfItemsAsync(
        string[] includes = null, 
        uint pageIndex = 0,
        uint pageSize = Constants.DefaultPageSize)
    {
        return GetPageOfItemsAsync(null, includes, pageIndex, pageSize);
    }

    /// <inheritdoc />
    public Task<PagedQueryResultModel<TModel>> GetPageOfItemsAsync(
        Expression<Func<TEntity, bool>> where,
        string[] includes = null,
        uint pageIndex = 0,
        uint pageSize = Constants.DefaultPageSize)
    {
        return GetPageOfItemsAsync(
            where,
            null,
            includes: includes,
            pageIndex: pageIndex,
            pageSize: pageSize);
    }

    /// <inheritdoc />
    public Task<PagedQueryResultModel<TModel>> GetPageOfItemsAsync(
        Expression<Func<TEntity, object>> orderBy,
        bool orderDescending = false,
        Expression<Func<TEntity, object>> thenBy = null,
        bool thenDescending = false,
        string[] includes = null,
        uint pageIndex = 0,
        uint pageSize = Constants.DefaultPageSize)
    {
        return GetPageOfItemsAsync(
            null,
            orderBy,
            orderDescending,
            thenBy,
            thenDescending,
            includes,
            pageIndex,
            pageSize
        );
    }

    /// <inheritdoc />
    public Task<PagedQueryResultModel<TModel>> GetPageOfItemsAsync<TSortModel, TFilterModel>(
        BasePagedQueryParameters<TEntity, TKey, TSortModel, TFilterModel> parameters) where TFilterModel : new()
    {
        return parameters != null
            ? GetPageOfItemsAsync(
                parameters.GetFilterExpression(),
                parameters.GetSortingExpression(),
                parameters.SortDescending,
                parameters.GetAdditionalSortingExpression(),
                parameters.ThenDescending,
                parameters.GetIncludes(),
                parameters.PageIndex,
                parameters.PageSize ?? Constants.DefaultPageSize)
            : GetPageOfItemsAsync();
    }

    /// <inheritdoc />
    public async Task<PagedQueryResultModel<TModel>> GetPageOfItemsAsync(
        Expression<Func<TEntity, bool>> where,
        Expression<Func<TEntity, object>> orderBy,
        bool orderDescending = false,
        Expression<Func<TEntity, object>> thenBy = null,
        bool thenDescending = false, 
        string[] includes = null,
        uint pageIndex = 0,
        uint pageSize = Constants.DefaultPageSize)
    {
        // Handle input exceptions.
        if (pageSize <= 0)
        {
            throw new ArgumentException("Page size should be larger than 0", nameof(pageSize));
        }

        var query = getQuery(where, includes);

        var itemCount = query.LongCount();

        var ordered = applySorting(query, orderBy, orderDescending, thenBy, thenDescending);

        var paged = ordered.GetPaged<TEntity, TKey>(pageSize, pageIndex);
        var pageOfItems = await paged.ProjectTo<TModel>(_mapper.ConfigurationProvider).ToListAsync();

        return new PagedQueryResultModel<TModel>(pageIndex, pageSize, (ulong)itemCount, pageOfItems);
    }

    /// <inheritdoc />
    public Task<List<TModel>> GetItemsAsync(Expression<Func<TEntity, bool>> where = null,
        string[] includes = null)
    {
        // By default sort results by id, ascending.
        return GetItemsAsync(where, e => e.Id, includes: includes);
    }

    /// <inheritdoc />
    public Task<List<TModel>> GetItemsAsync(Expression<Func<TEntity, object>> orderBy,
        bool orderDescending = false,
        Expression<Func<TEntity, object>> thenBy = null,
        bool thenDescending = false,
        string[] includes = null)
    {
        // By default use no filtering.
        return GetItemsAsync(null, orderBy, orderDescending, thenBy, thenDescending, includes);
    }

    /// <inheritdoc />
    public Task<List<TModel>> GetItemsAsync<TSortModel, TFilterModel>(
        BaseQueryParameters<TEntity, TKey, TSortModel, TFilterModel> parameters) where TFilterModel : new()
    {
        return parameters != null ? GetItemsAsync(
            parameters.GetFilterExpression(),
            parameters.GetSortingExpression(),
            parameters.SortDescending,
            parameters.GetAdditionalSortingExpression(),
            parameters.ThenDescending,
            parameters.GetIncludes()) : GetItemsAsync();
    }

    /// <inheritdoc />
    public Task<List<TModel>> GetItemsAsync(Expression<Func<TEntity, bool>> where,
        Expression<Func<TEntity, object>> orderBy,
        bool orderDescending = false,
        Expression<Func<TEntity, object>> thenBy = null,
        bool thenDescending = false, 
        string[] includes = null)
    {
        var query = getQuery(where, includes);

        var ordered = applySorting(query, orderBy, orderDescending, thenBy, thenDescending);

        return ordered.ProjectTo<TModel>(_mapper.ConfigurationProvider).ToListAsync();
    }

    private IQueryable<TEntity> getQuery(Expression<Func<TEntity, bool>> where, string[] includes)
    {
        var query = _repo.All(includes);

        if (where != null)
        {
            query = query.Where(where);
        }

        return query;
    }

    private static IOrderedQueryable<TEntity> applySorting(IQueryable<TEntity> query,
        Expression<Func<TEntity, object>> orderBy, bool orderDescending, Expression<Func<TEntity, object>> thenBy,
        bool thenDescending)
    {
        var ordered = query.OrderBy(orderBy ?? (e => e.Id), orderDescending);
        if (thenBy != null)
        {
            ordered = ordered.ThenBy(thenBy, thenDescending);
        }

        return ordered;
    }
}