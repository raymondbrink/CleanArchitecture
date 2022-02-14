namespace Example.Domain.Validation;

using Entities;

using FluentValidation;

using NetActive.CleanArchitecture.Domain.FluentValidation;

public class SupplierValidator : FluentEntityValidatorBase<Supplier>
{
    public override void Rules()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage(string.Format(ValidationResources.Required, nameof(Supplier.Name)));
        RuleFor(e => e.Contact)
            .NotEmpty()
            .WithMessage(string.Format(ValidationResources.Required, nameof(Supplier.Contact)));
        RuleFor(e => e.Contact.FamilyName)
            .NotEmpty()
            .WithMessage(string.Format(ValidationResources.Required, nameof(Supplier.Contact.FamilyName)));
    }
}