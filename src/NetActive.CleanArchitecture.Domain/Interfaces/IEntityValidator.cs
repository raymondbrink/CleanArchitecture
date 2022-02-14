namespace NetActive.CleanArchitecture.Domain.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Generic interface used to validate the given entity against a set of validation rules.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
public interface IEntityValidator<in TEntity>
{
    /// <summary>
    /// Validates the specified entity instance asynchronously.
    /// Throws an <see cref="Exception"/> if the entity is invalid.
    /// </summary>
    /// <param name="model">Entity to validate.</param>
    /// <param name="data">Additional data associated with the validation request.</param>
    Task AssertIsValidAsync(TEntity model, IDictionary<string, object> data = null);
}