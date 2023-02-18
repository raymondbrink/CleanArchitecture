namespace Example.Application.Manufacturer.Commands.DeleteManufacturer
{
    using Domain.Entities;
    using Interfaces.Persistence;

    using MediatR;
    using NetActive.CleanArchitecture.Application.Exceptions;
    using NetActive.CleanArchitecture.Application.MediatR.Abstractions.Commands;

    using Repository;

    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class DeleteManufacturerCommandHandler : BaseDeleteCommandHandler<DeleteManufacturerCommand>
    {
        private readonly IDeleteManufacturerRepositoryFacade _repositories;

        public DeleteManufacturerCommandHandler(
            IDeleteManufacturerRepositoryFacade repositories, 
            IExampleUnitOfWork unitOfWork, 
            IPublisher publisher) 
            : base(unitOfWork, publisher)
        {
            _repositories = repositories;
        }

        public override async Task<Unit> Handle(DeleteManufacturerCommand request, CancellationToken cancellationToken)
        {
            // Find manufacturer instance.
            var manufacturer = await _repositories.GetAsync(request.ManufacturerId);
            if (manufacturer == null)
            {
                throw new EntityNotFoundException(typeof(Manufacturer), request.ManufacturerId);
            }

            // Delete manufacturer from repo.
            _repositories.Delete(manufacturer);

            // Commit changes.
            await SaveChangesAsync(cancellationToken);

            // Send notification.
            await PublishNotificationAsync(manufacturer.Id, cancellationToken);

            return Unit.Value;
        }
    }
}
