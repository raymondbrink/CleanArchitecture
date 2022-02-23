namespace NetActive.CleanArchitecture.Application.Models;

using System;
using System.Linq.Expressions;

using Domain.Interfaces;

using Interfaces;

/// <inheritdoc cref="IQueryParameters{TEntity, TKey, TSortModel, TFilterModel}" />
public abstract class BaseQueryParameters<TEntity, TSortModel, TFilterModel> 
    : BaseQueryParameters<TEntity, long, TSortModel, TFilterModel>
    where TEntity : class, IEntity
    where TFilterModel : new()
{
}

/// <inheritdoc cref="IQueryParameters{TEntity, TKey, TSortModel, TFilterModel}" />
public abstract class BaseQueryParameters<TEntity, TKey, TSortModel, TFilterModel>
    : IQueryParameters<TEntity, TKey, TSortModel, TFilterModel>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
    where TFilterModel : new()
{
    /// <summary>
    /// Constructor.
    /// </summary>
    protected BaseQueryParameters()
    {
        Filters = new TFilterModel();
    }

    /// <inheritdoc />
    public TFilterModel Filters { get; set; }

    /// <inheritdoc />
    public TSortModel SortBy { get; set; }

    /// <inheritdoc />
    public bool SortDescending { get; set; }

    /// <inheritdoc />
    public TSortModel ThenBy { get; set; }

    /// <inheritdoc />
    public bool ThenDescending { get; set; }

    /// <inheritdoc />
    public string[] Includes { get; set; }

    /// <inheritdoc />
    public virtual Expression<Func<TEntity, bool>> GetFilterExpression()
    {
        return null;
    }

    /// <inheritdoc />
    public virtual Expression<Func<TEntity, object>> GetSortingExpression()
    {
        return null;
    }

    /// <inheritdoc />
    public virtual Expression<Func<TEntity, object>> GetAdditionalSortingExpression()
    {
        return null;
    }
}