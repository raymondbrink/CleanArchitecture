namespace NetActive.CleanArchitecture.Domain.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

using FluentValidation;

/// <summary>
/// Class used to validate entities against a set of Fluent Validation rules.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
public interface IEntityValidator<in TEntity>
{
    /// <summary>
    /// Validates the specified instance asynchronously.
    /// Throws a <see cref="ValidationException"/> if the entity is invalid.
    /// </summary>
    /// <param name="model">Entity to validate.</param>
    /// <param name="data">Additional data associated with the validation request.</param>
    Task AssertIsValidAsync(TEntity model, IDictionary<string, object> data = null);

    /// <summary>
    /// Validates the specified instance.
    /// Throws a <see cref="ValidationException"/> if the entity is invalid.
    /// </summary>
    /// <param name="model">Entity to validate.</param>
    /// <param name="data">Additional data associated with the validation request.</param>
    void AssertIsValid(TEntity model, IDictionary<string, object> data = null);

    /// <summary>
    /// Implement this method to define the validation rules the entity will be validated against.
    /// </summary>
    void Rules();
}