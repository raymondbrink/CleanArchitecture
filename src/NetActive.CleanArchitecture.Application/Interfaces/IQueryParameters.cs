﻿namespace NetActive.CleanArchitecture.Application.Interfaces;

using System;
using System.Linq.Expressions;

using Domain.Interfaces;

/// <summary>
/// Query parameters used to apply filtering and sorting to an entity query.
/// </summary>
/// <typeparam name="TEntity">Type of entity.</typeparam>
/// <typeparam name="TKey">Type of entity key.</typeparam>
/// <typeparam name="TSortModel">Sorting model.</typeparam>
/// <typeparam name="TFilterModel">Filtering model.</typeparam>
public interface IQueryParameters<TEntity, TKey, TSortModel, out TFilterModel>
    where TEntity : class, IEntityBase<TKey>
    where TKey : struct
{
    /// <summary>
    /// Filtering to apply.
    /// </summary>
    TFilterModel Filters { get; }

    /// <summary>
    /// Sorting to apply.
    /// </summary>
    TSortModel SortBy { get; set; }

    /// <summary>
    /// Sort descending if true.
    /// </summary>
    bool SortDescending { get; set; }

    /// <summary>
    /// Gets the filtering expression.
    /// </summary>
    /// <returns>IQueryable&lt;TEntity&gt;.</returns>
    Expression<Func<TEntity, bool>> GetFilterExpression();

    /// <summary>
    /// Gets the sorting expression.
    /// </summary>
    /// <returns>IOrderedQueryable&lt;TEntity&gt;.</returns>
    Expression<Func<TEntity, object>> GetSortingExpression();
}