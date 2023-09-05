namespace CleanArchConsoleApp.Application.FeatureName.Queries.GetFeatureName
{
    using Domain.Entities;

    using Models;

    using NetActive.CleanArchitecture.Application.Interfaces;

    internal class GetFeatureNameQuery : IGetFeatureNameQuery
    {
        private readonly IEntityQueryService<FeatureName, FeatureNameDetailModel, Guid> _query;

        public GetFeatureNameQuery(IEntityQueryService<FeatureName, FeatureNameDetailModel, Guid> query)
        {
            _query = query;
        }

        public Task<FeatureNameDetailModel> ExecuteAsync(Guid id)
        {
            return _query.GetAsync(id);
        }
    }
}