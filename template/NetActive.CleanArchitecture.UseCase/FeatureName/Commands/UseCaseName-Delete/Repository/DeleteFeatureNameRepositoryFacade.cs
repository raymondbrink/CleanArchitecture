namespace NetActive.CleanArchitecture.UseCase.FeatureName.Commands.UseCaseName.Repository
{
    using Domain.Entities;

    using NetActive.CleanArchitecture.Domain.Interfaces;

    using System;
    using System.Threading.Tasks;

    internal class DeleteFeatureNameRepositoryFacade
        : IDeleteFeatureNameRepositoryFacade
    {
        private readonly IRepository<FeatureName, KeyType> _repo;

        public DeleteFeatureNameRepositoryFacade(IRepository<FeatureName, KeyType> repo)
        {
            _repo = repo;
        }

        public void Delete(FeatureName entity)
            => _repo.Remove(entity);

        public Task<FeatureName?> GetByIdAsync(KeyType id) 
            => _repo.GetAsync(id);
    }
}
