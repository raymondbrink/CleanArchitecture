namespace NetActive.CleanArchitecture.UseCase.FeatureName.Commands.AddFeatureName.Repository
{
    using Domain.Entities;

    using NetActive.CleanArchitecture.Application.EntityFrameworkCore.Extensions;
    using NetActive.CleanArchitecture.Domain.Interfaces;

    internal class AddFeatureNameRepositoryFacade 
        : IAddFeatureNameRepositoryFacade
    {
        private readonly IRepository<FeatureName, KeyType> _repo;

        public AddFeatureNameRepositoryFacade(IRepository<FeatureName, KeyType> repo)
        {
            _repo = repo;
        }

        /// <inheritdoc />
        public Task<bool> FeatureNameExistsAsync(string name) => _repo.ExistsAsync(s => s.Name.Equals(name));

        /// <inheritdoc />
        public void AddFeatureName(FeatureName featureName) => _repo.Add(featureName);
    }
}