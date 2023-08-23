namespace NetActive.CleanArchitecture.UseCase.FeatureName.Queries.UseCaseName
{
    using Domain.Entities;

    using Models;

    using NetActive.CleanArchitecture.Application.Interfaces;
    using NetActive.CleanArchitecture.Application.Models;

    internal class UseCaseNameQuery 
        : IUseCaseNameQuery
    {
        private readonly IEntityQueryService<FeatureName, FeatureNameListModel, KeyType> _query;

        public UseCaseNameQuery(IEntityQueryService<FeatureName, FeatureNameListModel, KeyType> query)
        {
            _query = query;
        }

        /// <inheritdoc />
        public Task<PagedQueryResultModel<FeatureNameListModel>> ExecuteAsync(FeatureNameQueryParams? parameters = null)
        {
            return _query.GetPageOfItemsAsync(parameters);
        }
    }
}