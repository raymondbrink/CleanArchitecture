namespace NetActive.CleanArchitecture.UseCase.FeatureName.Queries.UseCaseName
{
    using Domain.Entities;

    using Models;

    using NetActive.CleanArchitecture.Application.Interfaces;

    internal class UseCaseNameQuery 
        : IUseCaseNameQuery
    {
        private readonly IEntityQueryService<FeatureName, FeatureNameExistsModel, KeyType> _query;

        public UseCaseNameQuery(IEntityQueryService<FeatureName, FeatureNameExistsModel, KeyType> query)
        {
            _query = query;
        }

        public Task<bool> ExecuteAsync(string name)
        {
            return _query.ExistsAsync(c => c.Name.Equals(name));
        }
    }
}