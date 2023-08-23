namespace Example.Application.Manufacturer.Commands.AddManufacturer
{
    using Domain.Entities;
    using Factory;
    using Interfaces.Persistence;
    using Models;
    using Repository;

    using NetActive.CleanArchitecture.Domain.Interfaces;

    internal class AddManufacturerCommand 
        : IAddManufacturerCommand
    {
        private readonly IAddManufacturerRepositoryFacade _repositories;
        private readonly IManufacturerFactory _factory;
        private readonly IEntityValidator<Manufacturer> _validator;
        private readonly IExampleUnitOfWork _unitOfWork;

        public AddManufacturerCommand(
            IAddManufacturerRepositoryFacade repositories,
            IManufacturerFactory factory,
            IEntityValidator<Manufacturer> validator,
            IExampleUnitOfWork unitOfWork)
        {
            _repositories = repositories;
            _factory = factory;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> ExecuteAsync(AddManufacturerCommandModel model, CancellationToken? cancellationToken = null)
        {
            if (await _repositories.ManufacturerExistsAsync(model.ManufacturerName))
            {
                throw new InvalidOperationException($"Manufacturer with name '{model.ManufacturerName}' already exists.");
            }

            // Create manufacturer instance.
            var manufacturer = _factory.Create(model.ManufacturerName, model.Contact?.FamilyName, model.Contact?.GivenName);

            // Assert manufacturer is valid.
            await _validator.AssertIsValidAsync(manufacturer);

            // Add manufacturer to repo.
            _repositories.AddManufacturer(manufacturer);

            // Commit changes.            
            await _unitOfWork.SaveChangesAsync(cancellationToken ?? CancellationToken.None);

            return manufacturer.Id;
        }
    }
}
