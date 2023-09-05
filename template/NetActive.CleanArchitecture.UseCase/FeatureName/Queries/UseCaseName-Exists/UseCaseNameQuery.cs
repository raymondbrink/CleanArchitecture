namespace NetActive.CleanArchitecture.UseCase.FeatureName.Queries.UseCaseName
{
    using Domain.Entities;

    using Models;

    using NetActive.CleanArchitecture.Application.Interfaces;

    internal class UseCaseNameQuery 
        : IUseCaseNameQuery
    {
        private readonly IEntityExistsService<FeatureName, KeyType> _query;

        public UseCaseNameQuery(IEntityExistsService<FeatureName, KeyType> query)
        {
            _query = query;
        }

        public Task<bool> ExecuteAsync(string name)
        {
            return _query.ExistsAsync(c => c.Name.Equals(name));
        }
    }
}