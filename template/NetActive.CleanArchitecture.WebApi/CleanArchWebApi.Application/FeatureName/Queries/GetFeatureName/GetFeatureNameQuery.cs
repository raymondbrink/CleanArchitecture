namespace CleanArchWebApi.Application.FeatureName.Queries.GetFeatureName
{
    using Domain.Entities;

    using Models;

    using NetActive.CleanArchitecture.Application.Interfaces;

    internal class GetFeatureNameQuery 
        : IGetFeatureNameQuery
    {
        private readonly IEntityQueryService<FeatureName, FeatureNameDetailModel, KeyType> _query;

        public GetFeatureNameQuery(IEntityQueryService<FeatureName, FeatureNameDetailModel, KeyType> query)
        {
            _query = query;
        }

        public Task<FeatureNameDetailModel> ExecuteAsync(KeyType id)
        {
            return _query.GetAsync(id);
        }
    }
}