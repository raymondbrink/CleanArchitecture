namespace NetActive.CleanArchitecture.Domain.Validation;

using System.Collections.Generic;
using System.Threading.Tasks;

using FluentValidation;

using Interfaces;

/// <summary>
/// Base for entity validators.
/// Override <see cref="Rules"/> to add FluentValidation rules for <see cref="T:TEntity"/>.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public abstract class EntityValidatorBase<TEntity> : AbstractValidator<TEntity>, IEntityValidator<TEntity>
{
    protected EntityValidatorBase()
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        Rules();
    }

    /// <inheritdoc />
    public async Task AssertIsValidAsync(TEntity model, IDictionary<string, object> data = null)
    {
        var validationResult = await base.ValidateAsync(getValidationContext(model, data));
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    }

    /// <inheritdoc />
    public void AssertIsValid(TEntity model, IDictionary<string, object> data = null)
    {
        var validationResult = base.Validate(getValidationContext(model, data));
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    }

    /// <inheritdoc />
    public abstract void Rules();

    private static ValidationContext<TEntity> getValidationContext(TEntity model, IDictionary<string, object> data)
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