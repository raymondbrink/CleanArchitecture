namespace Example.Application.Manufacturer.Commands.AddManufacturer
{
    using Domain.Entities;
    using Factory;
    using Interfaces.Persistence;

    using MediatR;
    using NetActive.CleanArchitecture.Application.MediatR.Abstractions.Commands;
    using NetActive.CleanArchitecture.Domain.Interfaces;
    
    using Repository;
    
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class AddManufacturerCommandHandler : BaseCreateCommandHandler<AddManufacturerCommand, Guid>
    {
        private readonly IAddManufacturerRepositoryFacade _repositories;
        private readonly IEntityValidator<Manufacturer> _validator;
        private readonly IManufacturerFactory _factory;

        public AddManufacturerCommandHandler(IAddManufacturerRepositoryFacade repositories,
            IEntityValidator<Manufacturer> validator,
            IManufacturerFactory factory,
            IExampleUnitOfWork unitOfWork,
            IPublisher publisher) 
            : base(unitOfWork, publisher)
        {
            _repositories = repositories;
            _validator = validator;
            _factory = factory;
        }

        /// <inheritdoc/>
        public override async Task<Guid> Handle(AddManufacturerCommand request, CancellationToken cancellationToken)
        {
            if (await _repositories.ManufacturerExistsAsync(request.model.ManufacturerName))
            {
                throw new InvalidOperationException($"Manufacturer with name '{request.model.ManufacturerName}' already exists.");
            }

            // Create manufacturer instance.
            var manufacturer = _factory.Create(request.model.ManufacturerName, request.model.Contact?.FamilyName, request.model.Contact?.GivenName);

            // Assert manufacturer is valid.
            await _validator.AssertIsValidAsync(manufacturer);

            // Add manufacturer to repo.
            _repositories.AddManufacturer(manufacturer);

            // Commit changes.
            await SaveChangesAsync(cancellationToken);

            // Send notification.
            await PublishNotificationAsync(manufacturer.Id, cancellationToken);

            return manufacturer.Id;
        }
    }
}
