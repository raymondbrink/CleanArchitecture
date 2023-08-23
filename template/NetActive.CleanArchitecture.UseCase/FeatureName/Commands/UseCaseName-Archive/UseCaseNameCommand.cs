namespace NetActive.CleanArchitecture.UseCase.FeatureName.Commands.UseCaseName
{
    using Domain.Entities;
    using Interfaces.Persistence;
    using Repository;
    
    using NetActive.CleanArchitecture.Application.Exceptions;
    using NetActive.CleanArchitecture.Application.Persistence.Interfaces;

    using System;
    using System.Threading.Tasks;

    internal class ArchiveFeatureNameCommand
        : IArchiveFeatureNameCommand
    {
        private readonly IArchiveFeatureNameRepositoryFacade _repositories;
        private readonly IUnitOfWork _unitOfWork;

        public ArchiveFeatureNameCommand(
            IArchiveFeatureNameRepositoryFacade repositories,
            IUnitOfWork unitOfWork)
        {
            _repositories = repositories;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(KeyType id, string archivedBy)
        {
            if (string.IsNullOrWhiteSpace(archivedBy))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(archivedBy));
            }

            var entity = await _repositories.GetByIdAsync(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(FeatureName), id);
            }

            _repositories.Archive(entity, archivedBy);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
