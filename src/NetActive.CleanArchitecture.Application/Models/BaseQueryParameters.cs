namespace NetActive.CleanArchitecture.Application.Models;

using System;
using System.Linq.Expressions;

using Domain.Interfaces;

using Interfaces;

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
    public TFilterModel Filters { get; }

    /// <inheritdoc />
    public TSortModel SortBy { get; set; }

    /// <inheritdoc />
    public bool SortDescending { get; set; }

    /// <inheritdoc />
    public abstract Expression<Func<TEntity, bool>> GetFilterExpression();

    /// <inheritdoc />
    public abstract Expression<Func<TEntity, object>> GetSortingExpression();
}