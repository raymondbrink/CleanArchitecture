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
/// <typeparam name="TKey">Type of entity key.</typeparam>
public class EntityQueryService<TEntity, TModel, TKey> : IEntityQueryService<TEntity, TModel, TKey>
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

    IQueryable<TEntity> IEntityQueryService<TEntity, TModel, TKey>.GetQuery() =>
        _repo.All();

    IMapper IEntityQueryService<TEntity, TModel, TKey>.Mapper => _mapper;

    /// <inheritdoc />
    public Task<TModel> GetAsync(TKey entityId) => GetAsync(e => e.Id.Equals(entityId));

    /// <inheritdoc />
    public async Task<TModel> GetAsync(Expression<Func<TEntity, bool>> where)
    {
        if (where == null)
        {
            throw new ArgumentNullException(nameof(where));
        }

        var query = _repo.All().Where(where);
        var result = await query.SingleOrDefaultAsync();

        return _mapper.Map<TModel>(result);
    }

    /// <inheritdoc />
    public Task<TModel> ReadAsync(TKey entityId) => ReadAsync(e => e.Id.Equals(entityId));

    /// <inheritdoc />
    public async Task<TModel> ReadAsync(Expression<Func<TEntity, bool>> where)
    {
        var model = await GetAsync(where);
        if (model == null)
        {
            throw new EntityNotFoundException(typeof(TEntity));
        }

        return model;
    }

    /// <inheritdoc />
    public Task<bool> ExistsAsync(TKey id) => ExistsAsync(e => e.Id.Equals(id));

    /// <inheritdoc />
    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> where = null)
    {
        var query = _repo.All();
        return where == null ? query.AnyAsync() : query.Where(where).AnyAsync();
    }

    /// <inheritdoc />
    public Task<PagedQueryResultModel<TModel>> GetPageOfItemsAsync(
        Expression<Func<TEntity, bool>> where = null,
        uint pageIndex = 0,
        uint pageSize = Constants.DefaultPageSize)
    {
        return GetPageOfItemsAsync(
            where,
            null,
            pageIndex: pageIndex,
            pageSize: pageSize);
    }

    /// <inheritdoc />
    public Task<PagedQueryResultModel<TModel>> GetPageOfItemsAsync(
        Expression<Func<TEntity, object>> orderBy,
        bool orderDescending = false,
        uint pageIndex = 0,
        uint pageSize = Constants.DefaultPageSize)
    {
        return GetPageOfItemsAsync(
            null,
            orderBy,
            orderDescending,
            pageIndex,
            pageSize
        );
    }

    /// <inheritdoc />
    public async Task<PagedQueryResultModel<TModel>> GetPageOfItemsAsync(
        Expression<Func<TEntity, bool>> where,
        Expression<Func<TEntity, object>> orderBy,
        bool orderDescending = false,
        uint pageIndex = 0,
        uint pageSize = Constants.DefaultPageSize)
    {
        // Handle input exceptions.
        if (pageSize <= 0)
        {
            throw new ArgumentException("Page size should be larger than 0", nameof(pageSize));
        }

        var query = getQuery(where);

        var itemCount = query.LongCount();

        var ordered = query.OrderBy(orderBy ?? (e => e.Id), orderDescending);

        var paged = ordered.GetPaged<TEntity, TKey>(pageSize, pageIndex);

        var pageOfItems = await paged.ProjectTo<TModel>(_mapper.ConfigurationProvider).ToListAsync();

        return new PagedQueryResultModel<TModel>(pageIndex, pageSize, (ulong)itemCount, pageOfItems);
    }

    /// <inheritdoc />
    public Task<List<TModel>> GetItemsAsync(Expression<Func<TEntity, bool>> where = null)
    {
        // By default sort results by id, ascending.
        return GetItemsAsync(where, e => e.Id);
    }

    /// <inheritdoc />
    public Task<List<TModel>> GetItemsAsync(Expression<Func<TEntity, TKey>> orderBy = null,
        bool orderDescending = false)
    {
        // By default use no filtering.
        return GetItemsAsync(null, e => e.Id);
    }

    /// <inheritdoc />
    public Task<List<TModel>> GetItemsAsync(Expression<Func<TEntity, bool>> where,
        Expression<Func<TEntity, object>> orderBy,
        bool orderDescending = false)
    {
        var query = getQuery(where);

        query = orderBy != null
            ? query.OrderBy(orderBy, orderDescending)
            : query.OrderBy(e => e.Id);

        return query.ProjectTo<TModel>(_mapper.ConfigurationProvider).ToListAsync();
    }

    private IQueryable<TEntity> getQuery(Expression<Func<TEntity, bool>> where = null)
    {
        var query = _repo.All();
        return where != null ? query.Where(where) : query;
    }
}