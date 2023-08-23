namespace NetActive.CleanArchitecture.UseCase.FeatureName.Queries.UseCaseName
{
    using Domain.Entities;
    using Models;

    using NetActive.CleanArchitecture.Application.Interfaces;
    using System.Threading.Tasks;

    internal class UseCaseNameQuery
        : IUseCaseNameQuery
    {
        private readonly IEntityQueryService<FeatureName, FeatureNameDetailModel, KeyType> _query;

        public UseCaseNameQuery(IEntityQueryService<FeatureName, FeatureNameDetailModel, KeyType> query)
        {
            _query = query;
        }

        public Task<FeatureNameDetailModel> ExecuteAsync(KeyType id)
        {
            return _query.GetAsync(id);
        }
    }
}