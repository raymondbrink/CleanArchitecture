namespace CleanArchConsoleApp.Application.FeatureName.Queries.GetPageOfMyEntities
{
    using Domain.Entities;

    using Models;

    using NetActive.CleanArchitecture.Application.Interfaces;
    using NetActive.CleanArchitecture.Application.Models;

    internal class GetPageOfMyEntitiesQuery : IGetPageOfMyEntitiesQuery
    {
        private readonly IEntityQueryService<FeatureName, FeatureNameListModel, Guid> _query;

        public GetPageOfMyEntitiesQuery(IEntityQueryService<FeatureName, FeatureNameListModel, Guid> query)
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