namespace NetActive.CleanArchitecture.UseCase.FeatureName.Commands.UseCaseName.Repository
{
	using Domain.Entities;

	using Microsoft.EntityFrameworkCore;
	
	using NetActive.CleanArchitecture.Application.Persistence.Interfaces;

	using System.Threading.Tasks;

	internal class UpdateFeatureNameRepositoryFacade
        : IUpdateFeatureNameRepositoryFacade
    {
		private readonly IRepository<FeatureName, KeyType> _repo;

		public UpdateFeatureNameRepositoryFacade(
			IRepository<FeatureName, KeyType> repo)
		{
			_repo = repo;
		}

		public Task<FeatureName?> GetFeatureNameByIdAsync(KeyType id)
		{
			return _repo.GetAsync(v => v.Id == id);
		}
	}
}