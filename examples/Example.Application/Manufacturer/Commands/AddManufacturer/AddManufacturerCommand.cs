namespace Example.Application.Manufacturer.Commands.AddManufacturer
{
    using Domain.Entities;

    using Factory;

    using Interfaces.Persistence;

    using Models;

    using NetActive.CleanArchitecture.Domain.Interfaces;

    using Repository;

    internal class AddManufacturerCommand : IAddManufacturerCommand
    {
        private readonly IAddManufacturerRepositoryFacade _repositories;
        private readonly IEntityValidator<Manufacturer> _validator;
        private readonly IManufacturerFactory _factory;
        private readonly IExampleUnitOfWork _unitOfWork;

        public AddManufacturerCommand(IAddManufacturerRepositoryFacade repositories,
            IEntityValidator<Manufacturer> validator,
            IManufacturerFactory factory,
            IExampleUnitOfWork unitOfWork)
        {
            _repositories = repositories;
            _validator = validator;
            _factory = factory;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> ExecuteAsync(AddManufacturerCommandModel model)
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
            await _unitOfWork.SaveChangesAsync();

            return manufacturer.Id;
        }
    }
}