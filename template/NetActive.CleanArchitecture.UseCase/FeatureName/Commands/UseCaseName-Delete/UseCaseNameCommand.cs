namespace NetActive.CleanArchitecture.UseCase.FeatureName.Commands.UseCaseName
{
    using Domain.Entities;
    using Repository;
    
    using NetActive.CleanArchitecture.Application.Exceptions;
    using NetActive.CleanArchitecture.Application.Persistence.Interfaces;

    using System;
    using System.Threading.Tasks;

    internal class DeleteFeatureNameCommand
        : IDeleteFeatureNameCommand
    {
        private readonly IDeleteFeatureNameRepositoryFacade _repositories;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFeatureNameCommand(
            IDeleteFeatureNameRepositoryFacade repositories,
            IUnitOfWork unitOfWork)
        {
            _repositories = repositories;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(KeyType id)
        {
            var entity = await _repositories.GetByIdAsync(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(FeatureName), id);
            }

            _repositories.Delete(entity);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
