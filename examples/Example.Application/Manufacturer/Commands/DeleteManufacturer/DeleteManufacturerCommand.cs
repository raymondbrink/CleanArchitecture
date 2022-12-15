namespace Example.Application.Manufacturer.Commands.DeleteManufacturer
{
    using Domain.Entities;

    using Interfaces.Persistence;

    using NetActive.CleanArchitecture.Application.Exceptions;

    using Repository;

    internal class DeleteManufacturerCommand : IDeleteManufacturerCommand
    {
        private readonly IDeleteManufacturerRepositoryFacade _repositories;
        private readonly IExampleUnitOfWork _unitOfWork;

        public DeleteManufacturerCommand(IDeleteManufacturerRepositoryFacade repositories,
            IExampleUnitOfWork unitOfWork)
        {
            _repositories = repositories;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(Guid manufacturerId)
        {
            var manufacturer = await _repositories.GetAsync(manufacturerId);
            if (manufacturer == null)
            {
                throw new EntityNotFoundException(typeof(Manufacturer), manufacturerId);
            }

            _repositories.Delete(manufacturer);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}