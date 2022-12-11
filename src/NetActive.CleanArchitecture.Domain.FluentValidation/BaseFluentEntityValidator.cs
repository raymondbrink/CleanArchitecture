namespace NetActive.CleanArchitecture.Domain.FluentValidation;

using System.Collections.Generic;
using System.Threading.Tasks;

using global::FluentValidation;

using Interfaces;

/// <summary>
/// Generic abstract base used to validate entities against a set of FluentValidation rules.
/// Inherit your validator from this class and override <see cref="Rules"/> to add FluentValidation rules for <see cref="T:TEntity"/>.
/// </summary>
/// <typeparam name="TEntity">Type of entity.</typeparam>
public abstract class BaseFluentEntityValidator<TEntity> : AbstractValidator<TEntity>, IEntityValidator<TEntity>
{
    protected BaseFluentEntityValidator()
    {
        Rules();
    }

    /// <summary>
    /// Validates the specified entity instance asynchronously.
    /// Throws a <see cref="ValidationException"/> if the entity is invalid.
    /// </summary>
    /// <param name="model">Entity to validate.</param>
    /// <param name="data">Additional data associated with the validation request.</param>
    public async Task AssertIsValidAsync(TEntity model, IDictionary<string, object>? data = null)
    {
        var validationResult = await base.ValidateAsync(getValidationContext(model, data));
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    }

    /// <summary>
    /// Implement this method to define the validation rules the entity will be validated against.
    /// </summary>
    public abstract void Rules();

    private static ValidationContext<TEntity> getValidationContext(TEntity model, IDictionary<string, object>? data)
    {
        var validationContext = new ValidationContext<TEntity>(model);
        if (data != null)
        {
            foreach (var entry in data)
            {
                validationContext.RootContextData.Add(entry);
            }
        }

        return validationContext;
    }
}