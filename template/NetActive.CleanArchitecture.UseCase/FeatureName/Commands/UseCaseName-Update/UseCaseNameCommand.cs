namespace NetActive.CleanArchitecture.UseCase.FeatureName.Commands.UseCaseName
{
	using Domain.Entities;
	using Interfaces.Persistence;
	using Models;
	using Repository;

	using NetActive.CleanArchitecture.Application.Exceptions;

	using System.Threading.Tasks;

	internal class UseCaseNameCommand
        : IUseCaseNameCommand
    {
		private readonly IUpdateFeatureNameRepositoryFacade _repositories;
		private readonly IUnitOfWork _unitOfWork;

		public UseCaseNameCommand(
			IUpdateFeatureNameRepositoryFacade repositories,
			IUnitOfWork unitOfWork)
		{
			_repositories = repositories;
			_unitOfWork = unitOfWork;
		}

		public async Task ExecuteAsync(UpdateFeatureNameCommandModel model)
		{
			// Get FeatureName from cache.
			var entity = await _repositories.GetFeatureNameByIdAsync(model.Id);
			if (entity == null)
			{
				throw new EntityNotFoundException(typeof(FeatureName), model.Id);
			}

			// Map properties to FeatureName.
			entity.name = model.Name;

            // TODO: Validate FeatureName.

            // Store changes.
            await _unitOfWork.SaveChangesAsync();
		}
	}
}
