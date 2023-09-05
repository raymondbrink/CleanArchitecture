namespace CleanArchFeature.Application.FeatureName.Queries.GetFeatureNameList
{
    using Domain.Entities;

    using Models;

    using NetActive.CleanArchitecture.Application.Interfaces;

    /// <inheritdoc cref="IGetFeatureNameListQuery" />
    internal class GetFeatureNameListQuery 
        : IGetFeatureNameListQuery
    {
        private readonly IEntityQueryService<FeatureName, FeatureNameListModel, KeyType> _query;

        public GetFeatureNameListQuery(IEntityQueryService<FeatureName, FeatureNameListModel, KeyType> query)
        {
            _query = query;
        }

        /// <inheritdoc />
        public Task<List<FeatureNameListModel>> ExecuteAsync(FeatureNameQueryParams? parameters = null)
        {
            return _query.GetItemsAsync(parameters);
        }
    }
}