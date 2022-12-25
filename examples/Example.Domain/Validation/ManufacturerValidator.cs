namespace Example.Domain.Validation
{
    using Entities;

    using FluentValidation;

    using NetActive.CleanArchitecture.Domain.FluentValidation;

    public class ManufacturerValidator : BaseFluentEntityValidator<Manufacturer>
    {
        public override void Rules()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage(string.Format(ValidationResources.Required, nameof(Manufacturer.Name)));
            RuleFor(e => e.Contact)
                .NotEmpty()
                .WithMessage(string.Format(ValidationResources.Required, nameof(Manufacturer.Contact)));
            RuleFor(e => e.Contact.FamilyName)
                .NotEmpty()
                .WithMessage(string.Format(ValidationResources.Required, nameof(Manufacturer.Contact.FamilyName)));
        }
    }
}